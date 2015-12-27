using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NetOffice;
using Word = NetOffice.WordApi;
using NetOffice.WordApi.Enums;
using System.Text;
using System.Drawing;

using System.Text.RegularExpressions;
using OnlineExam.Models;
using System.Windows.Media.Imaging;
using System.IO;

namespace OnlineExam
{
    public class PaperImporter
    {

        //bool isNum(char c)
        //{

        //    return c >= '0' && c <= '9';
        //}

        bool isBigNum(char c)
        {
            return bigNum.Contains(c);
        }

        //^[\(（]?[1234567890]{1,3}[\)）、\.．，,](.*((\r\n)|\s))+
        static string rexNewContent = @"^[\[（【]?(([0-9一二三四五六七八九十]{1,3})|([a-fA-F])|(参考答案))[\)\]）】:：\.．、]";
        static string rexEnd = "(?!(" + rexNewContent + "))";  //
        static string rexBigNum = @"^(?<=[\(（])?[一二三四五六七八九十]{1,2}(?=[\)）、\.．，, ].+)";//匹配大题题号
        //static string rexBigNum = @"^[\(（]?[一二三四五六七八九十]{1,2}[\)）、\.．，, ]";
        static string rexSmallNum = @"^[\(（]?[1234567890]{1,3}[\)）、\.．，, ]";
        static string rexBigTitle = rexBigNum + @"[\S\s]*?(题|分).*";  //大题标题t
        // rexQuestion: ^[\(（]?[1234567890]{1,3}[\)）、\.．，, ](.*((\r\n)|\s)(?!([\[（【]?(([0-9一二三四五六七八九十]{1,3})|([a-fA-F])|(参考答案))[\)\]）】:：\.．、])))+
        //static string rexQuestion = string.Format("{0}(.*{1}{2})+", rexSmallNum, rexEntry, rexEnd);
        static string rexQuestionNum = @"^(?<=[\(（])?[1234567890]{1,3}(?=[\)）、\.．，, ].*)";



        static string rexOption = @"^[\(（]?[A-Fa-f][\)）、\.．，, ].*"; //匹配选项 
        static string rexSingleOption = @"(\s|^)([\(（]?)[A-Fa-f]([\)）、\.．，, ]).*?((?=\s\2[a-fA-F]\3)|(\r\n)|\n|$)"; //匹配选项 
        //static string rexOptionList = rexOption + "{1,6}";
        static string rexAnswer = @"^[【\[\(（]?.{0,3}((答案)|(答))[】）\):：\s]";
        static string rexRemark = @"^[【\[\(（]?(.{0,4}((提示)|(解析)|(解释)|(分析)|(评分标准)))[】）\):：\s]";
        static string rexAnswerInQuestion = @"^[【\[\(（]?.{0,3}((答案)|(答))[】）\):：\s](.*)";//匹配问题附属的参考答案
        static string rexQuestionRemark = @"^[【\[\(（]?(.{0,4}((提示)|(解析)|(解释)|(分析)|(评分标准)))[】）\):：\s]{1,2}.*";//匹配问题附属的参考答案

        static string rexAnswerList = @"(\s+|^).{0,12}((答案)|(答)).{0,5}\s?";  //用于匹配试卷最后的参考答案
        static string rexAnswer2 = @"(\s|^)([\(（]?)[0-9]{1,3}([\)）、\.．，,]).*?((?=\s\2[0-9]\3)|(\r\n)|\n|$)";  //用于匹配答案列表区域中的答案
        //static string 

        static string[] singleChoiceTitle = new string[] { "单选题", "单项选择题", "选择题" };
        static string[] ChoiceTitle = new string[] { "多项选择题", "多选题", "选择题" };
        static string[] shortAnswerTitle = new string[] { "简答题", "回答", "计算", "小题" };
        static string[] completionTitle = new string[] { "填空题", "填空" };
        static string[] discTitle = new string[] { "议论题", "应用题", "大题" };
        static Dictionary<QuestionType, string[]> dictQtypeTitle = new Dictionary<QuestionType, string[]>();


        #region 暂时未用
        static char[] mark = new char[] { '.', '、', ')', '）' };
        static char[] bigNum = new char[] { '一', '二', '三', '四', '五', '六', '七', '八', '九', '十' };
        static char[] bracket = new char[] { '【', '】' };
        static char[] optionType1 = new char[] { 'A', 'B', 'C', 'D', 'E', 'F' };
        static char[] optionType2 = new char[] { 'a', 'b', 'c', 'd', 'e', 'f' };
        static char[] optionType3 = new char[] { '①', '②', '③', '④', '⑤', '⑥' };


        static string[] answer = new string[] { "答案", "参考答案", "回答", "解答" };
        static string[] answerEnd = new string[] { " ", "]", "】", ":", "、" };
        static string[] remark = new string[] { "解释", "解析", "解题思路", "提示", "备注", "解题方法", "方法" };
        static string[] examType = new string[] { "模拟", "测试", "自测", "期末考试", "期中考试" };
        #endregion



        ImageHandler handler = new ImageHandler();
        static List<string> subjectList = new List<string>();
        static List<string> subjectDetailList = new List<string>();
        bool IsAnswerInQuestion;//判断问题是否在
        char[] CurrentOptionType;//选择题的符号类型；是abcd还是ABCDEF还是①②③④⑤
        QuestionType tempQtype;
        QuestionType CurrentQtype;
        int tempBigNum;//用于临时保存大题号，未判断是否正确
        int CurrentBigNum;//当前的大题号
        string CurrentBigNumStr;
        int tempNum;//用于临时保存小题号，未判断是否正确
        int CurrentQNum;//游标指向的当前的题目编号
        List<Option> tempOps;
        ImportQuestion CurrentQuestion;//游标只想的当前题目
        ImportPaper Paper;
        Word.Paragraphs paras;//包含word文档的所有段落
        NetOffice.WordApi.Paragraphs currentPara;//当前段落
        int CurrentIndex;//当前段落游标位置；
        int ContentStartIndex;//段落的问题、答案等内容开始的位置
        //在导入试题时需要初始化类的参数；
        void InitImporter()
        {
            tempNum = CurrentQNum = 0;
            CurrentIndex = 0;
            CurrentOptionType = null;
            tempOps = null;
            CurrentQuestion = null;
            currentPara = null;
            Paper = new ImportPaper();
            ContentStartIndex = 0;
            CurrentBigNumStr = "";
        }

        static PaperImporter()
        {
            //初始化科目名称列表
            subjectList.Add("语文");
            subjectList.Add("数学");
            subjectList.Add("英语");
            subjectList.Add("政治");
            subjectList.Add("物理");
            subjectList.Add("体育");
            subjectList.Add("生物");
            subjectList.Add("历史");
            subjectList.Add("地理");

            //初始化大题名称（模糊）表
            dictQtypeTitle.Add(QuestionType.SingleChoice, singleChoiceTitle);
            dictQtypeTitle.Add(QuestionType.MultipleChoice, ChoiceTitle);
            dictQtypeTitle.Add(QuestionType.Completion, completionTitle);
            dictQtypeTitle.Add(QuestionType.ShortAnswer, shortAnswerTitle);
            dictQtypeTitle.Add(QuestionType.Discussion, discTitle);
        }

        //小题的开始形式为
        //数字+"、"，如  1、下列题目正确的是
        //数字+空格 如   1 下列题目正确的是
        /// <summary>
        /// 判读段落是否已经开始
        /// </summary>
        /// <param name="s"></param>
        /// <param name="num"></param>
        /// <param name="contentStartIndex"></param>
        /// <returns></returns>
        public bool IsQuestionNum()
        {
            var s = GetCurrentParaText();
            string snum = "";
            var temp = s.Trim();
            Match m = Regex.Match(temp, rexQuestionNum);

            if (m.Success)
            {
                snum = m.Value;
                if (int.Parse(snum) >= 23)
                {

                }
                tempNum = int.Parse(snum);
                if (20 > (tempNum - CurrentQNum) && (tempNum - CurrentQNum) > 0)//新的题号必须必旧题号大，并且不能与上一个题号相差太大，否则该行不是题目
                {
                    CurrentQNum = tempNum;//指向下一题
                    return true;
                }
                else if (CurrentQNum != 1 && tempNum == 1)//新的大题、 重新编号
                {
                    CurrentQNum = tempNum;//指向下一题
                    return true;
                }
            }
            return false;
        }
        Option GetOption(string s)
        {
            Option op = new Option();
            int i = 0;
            var c0 = s[0];
            if ((c0 >= 'A' && c0 <= 'F') | (c0 >= 'a' && c0 <= 'f'))
            {
                op.Op = c0.ToString();
            }
            else
            {
                i = 1;
                op.Op = s[1].ToString();
            }
            op.Content = s.Substring(i + 2);
            return op;
        }
        //从段落中获取选项信息
        bool IsOption()   //选项格式 A)xxxxx   A、xxxxxx  A xxxxxxxxx  A.xxxxxx a.xxxxxxx 等  
        {
            var s = GetCurrentParaText();
            int i = 0;

            var temp = s.Trim();
            var c = temp[0];

            Match m = Regex.Match(temp, rexOption);
            if (m.Success)
            {
                Match singleOp = Regex.Match(temp, rexSingleOption);
                while (singleOp.Success)
                {
                    CurrentQuestion.Options.Add(GetOption(singleOp.Value.Trim()));
                    singleOp = singleOp.NextMatch();
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        //从第一行读取 科目、年级等数据
        public void GetInfoFromTitle()
        {
        }
        public bool IsBigQuestion()
        {
            var s = GetCurrentParaText();
            var temp = s.Trim();
            Match m = Regex.Match(temp, rexBigNum);
            if (m.Success)
            {
                tempBigNum = Convert.ToInt32(ParseCNNumber.ParseCnToInt(m.Value));
                if (tempBigNum - CurrentBigNum > 0 && tempBigNum - CurrentBigNum < 4)
                {
                    //判断是否包含大题；
                    foreach (var t in dictQtypeTitle.ToList())
                    {
                        foreach (string s1 in t.Value)
                        {
                            if (s.IndexOf(s1) > 0)
                            {
                                CurrentQtype = t.Key;
                                CurrentBigNum = tempBigNum;
                                CurrentBigNumStr = m.Value;
                                return true;
                            }
                        }
                    }
                    if (s.IndexOf("题") > 0)
                    {
                        CurrentQtype = QuestionType.UnKnown;//未知题型
                        CurrentBigNum = tempBigNum;
                        CurrentBigNumStr = m.Value;
                        return true;
                    }


                }
            }
            return false;
        }

        public bool IsNewCtype()
        {
            var s = GetCurrentParaText();
            var temp = s.Trim();
            Match m1 = Regex.Match(temp, rexNewContent);
            Match m2 = Regex.Match(temp, rexAnswerInQuestion);
            Match m3 = Regex.Match(temp, rexQuestionRemark);
            Match m4 = Regex.Match(temp, rexAnswerList);
            return m1.Success || m2.Success || m3.Success || m4.Success;
        }
        public bool MoveNext()
        {
            if (HasNextPara()) { currentContent = ""; CurrentIndex++; return true; }
            return false;
        }
        public void MoveBack() { currentContent = ""; CurrentIndex--; }
        public void ReadQuestion()
        {
            CurrentQuestion = new ImportQuestion();//新建题目
            CurrentQuestion.ID = Paper.Questions.Count() + 1;
            Match m = Regex.Match(GetCurrentParaText().TrimStart(), rexQuestionNum);
            int i = m.Value.Length + GetCurrentParaText().TrimStart().IndexOf(m.Value);
            CurrentQuestion.Question = GetCurrentParaText().TrimStart().Substring(i + 1);
            CurrentQuestion.QuestionBigNum = CurrentBigNumStr;
            CurrentQuestion.BigNum = CurrentBigNum;
            CurrentQuestion.QuestionNum = CurrentQNum;
            CurrentQuestion.QType = CurrentQtype;
            bool end = false;
            do
            {
                if (MoveNext())
                {
                    var temp = GetCurrentParaText();
                    if (!IsNewCtype())
                    {
                        CurrentQuestion.Question += temp;

                    }
                    else
                    {
                        if (!IsOption() && !IsQuestionAnswer() && !IsQuestionRemark())
                        {
                            end = true;
                            MoveBack();
                        }
                    }
                }

                else
                {
                    end = true;

                }
            } while (!end);
            //CurrentIndex = CurrentIndex - 1;
            Paper.Questions.Add(CurrentQuestion);
        }
        //处理文字中的上标和下标格式
        public void SetSubAndSup()
        {
            //ran.Select();
            paras[1].Range.Select();
            var selection = wordApplication.Selection;
            selection.Find.ClearFormatting();
            selection.Find.Font.Subscript = 1;
            selection.Find.Font.Superscript = 0;
            selection.Find.Replacement.ClearFormatting();
            selection.Find.Replacement.Font.Subscript = 0;
            while (selection.Find.Execute())
            {

                selection.Range.Select();

                var temp = selection.Text;
                temp = temp.Replace("\r\n", "").Replace("\r", "");
                selection.Font.Subscript = 0;
                selection.Font.Superscript = 0;
                selection.TypeText("<sub>" + temp + "</sub>");
              

                selection.Find.ClearFormatting();
                selection.Find.Font.Subscript = 1;
                selection.Find.Font.Superscript = 0;
                selection.Find.Replacement.ClearFormatting();
                selection.Find.Replacement.Font.Subscript = 0;


            }
            //ran = paras[CurrentIndex].Range;
            paras[1].Range.Select();
            selection = wordApplication.Selection;
            selection.Find.ClearFormatting();
            selection.Find.Font.Subscript = 0;
            selection.Find.Font.Superscript = 1;
            selection.Find.Replacement.ClearFormatting();
            selection.Find.Replacement.Font.Superscript = 0;
            while (selection.Find.Execute())
            {

                selection.Range.Select();
                var temp = selection.Text;
                temp = temp.Replace("\r\n", "").Replace("\r", "");
                selection.TypeText("<sup>" + temp + "</sup>");
                selection.Font.Superscript = 0;
                selection.Font.Subscript = 1; 
                selection.Find.ClearFormatting();
                selection.Find.Font.Subscript = 0;
                selection.Find.Font.Superscript = 1;
                selection.Find.Replacement.ClearFormatting();
                selection.Find.Replacement.Font.Superscript = 0;
             
            }
        }
        string currentContent;//用于临时保存当前段落的内容
        public string GetCurrentParaText()
        {

            if (!string.IsNullOrEmpty(currentContent)) return currentContent;


            var ran = paras[CurrentIndex].Range;




            var s = paras[CurrentIndex].Range.Text;
            if (s.IndexOf('\u0001') >= 0)
            {

                handler.Setting(paras[CurrentIndex].Range.InlineShapes);

                //handler.ImagePath =HttpContext.Current.Server.MapPath( "~\");

                Thread.CurrentThread.TrySetApartmentState(ApartmentState.STA);
                handler.ImagePath = CUrl.ImportPaperImage;

                //handler.GetImages();
                Thread t = new Thread(handler.GetImagesFromShapes);
                t.TrySetApartmentState(ApartmentState.STA);
                t.Start();
                //Thread.Sleep(1000);
                //while (t.ThreadState != ThreadState.Stopped)
                //{
                //    Thread.CurrentThread.Join(;
                //}
                t.Join();
                foreach (string file in handler.ImageFiles)
                {
                    int i = s.IndexOf('\u0001');
                    if (i < 0)
                    {
                        i = s.Length - 1;
                    }
                    s = s.Remove(i, 1);
                    string img = string.Format("<img src='{0}'></img>", file);
                  
                    s = s.Insert(i, img);
                }
            }
            currentContent = s;
            return s;
        }
        //判断是否为问题的附属答案%（答案位于每题问题的下方）
        public bool IsQuestionAnswer()
        {
            var s = GetCurrentParaText();
            var temp = s.TrimStart();
            Match m = Regex.Match(temp, rexAnswer);
            if (!m.Success) return false;
            CurrentQuestion.Answer += temp.Substring(temp.IndexOf(m.Value) + m.Value.Length);
            while (true)
            {
                if (!MoveNext())
                {
                    break;
                }
                else
                {
                    if (!IsNewCtype())
                        CurrentQuestion.Answer += GetCurrentParaText();
                    else
                    {
                        MoveBack();
                        break;
                    }
                }
            }
            return true;
        }
        public bool IsQuestionRemark()
        {
            var s = GetCurrentParaText();
            var temp = s.TrimStart();

            Match m = Regex.Match(temp, rexRemark);

            if (!m.Success) return false;
            CurrentQuestion.Info += temp.Substring(temp.IndexOf(m.Value) + m.Value.Length);
            while (true)
            {
                //;
                //s = GetCurrentParaText();
                if (!MoveNext())
                {
                    break;
                }
                else
                {
                    if (!IsNewCtype())
                        CurrentQuestion.Info += GetCurrentParaText();
                    else
                    {
                        MoveBack();
                        break;
                    }
                }
            }
            return true;
        }

        public bool HasNextPara()
        {
            if (CurrentIndex + 1 < paras.Count) return true;
            return false;
        }

        public string GetNextParaText()
        {
            if (MoveNext()) { return GetCurrentParaText(); }
            return null;
        }
        public void ToBegin()
        {
            CurrentIndex = 0;
            currentContent = "";
        }
        public void ReadTitle()
        {
            string title = "";
            ToBegin();
            while (MoveNext() && !IsNewCtype())
            {
                title += GetCurrentParaText() + "\r\n";
            }
            Paper.ExamTitle = title;
        }





        public bool IsAnswerList()//根据题目列表中是否已经包含答案以及当前游标指向的小题号和大题号判断参考答案是位于每题内部还是位于试卷最后的答案列表区域中
        {
            var s = GetCurrentParaText();
            var temp = s.TrimStart();
            Match m = Regex.Match(temp, rexAnswerList);
            if (m.Success)
            {
                if (temp.Trim().Length < 20)//xxx参考答案  行字数不能太长
                {
                    if (CurrentQNum > 1||CurrentBigNum>1)
                    {
                        //如果之前导入的题目中多数的题目为空
                        var c = (from q in Paper.Questions
                                 where string.IsNullOrEmpty(q.Answer)
                                 select q.QuestionNum).Count();
                        if (c == 0 || Paper.Questions.Count / 1.0 / c < 2)
                        {
                            return true;

                        }
                    }
                }
            }
            return false;

        }


        int CurrentAnswerBigNum;
        int CurrentAnswerNum;
        ImportAnswer CurrentAnswer;
        public void InitAnswerImport()
        {
            CurrentAnswerBigNum = 0;
            CurrentAnswerNum = 0;
            CurrentAnswer = new ImportAnswer();
            //CurrentAnswer.BigNum = "";
            //CurrentAnswer.Num = 0;
        }

        public bool IsAnswerBigNum()
        {
            var s = GetCurrentParaText();
            var temp = s.Trim();
            Match m = Regex.Match(temp, rexBigNum);
            if (m.Success)
            {
                tempBigNum = Convert.ToInt32(ParseCNNumber.ParseCnToInt(m.Value));
                //if (tempBigNum - CurrentBigNum > 0 && tempBigNum - CurrentBigNum < 4)
                //{
                //if (s.IndexOf("题") > 0 || s.Length < 5)
                //{
                CurrentAnswerBigNum = tempBigNum;
                return true;
                //}
                //}
            }
            return false;
        }
        public bool IsAnswer()
        {
            var s = GetCurrentParaText();
            var temp = s.Trim();
            Match m = Regex.Match(temp, rexBigNum);
            return false;
        }
        void GetAnswer(string content)
        {

            int i = 0;
            string snum = "";
            Match m = Regex.Match(content.Trim(), rexQuestionNum);
            if (m.Success)
            {
                snum = m.Value;
                CurrentAnswer = new ImportAnswer();
                CurrentAnswerNum = int.Parse(snum);
                CurrentAnswer.Num = int.Parse(snum);
                CurrentAnswer.Answer = content.Substring(content.IndexOf(m.Value) + m.Value.Length + 1);
                CurrentAnswer.BigNum = CurrentAnswerBigNum;
            }
            //if(content.StartsWith("２"))
            //{


            //}
            //while (Char.IsNumber(content[i]) && i < content.Length)
            //{
            //    snum += content[i];
            //    i++;
            //}
            //if (i == 0)
            //    return;

        }
        public bool IsAnswerRemark()
        {
            var s = GetCurrentParaText();
            var temp = s.TrimStart();

            Match m = Regex.Match(temp, rexRemark);

            if (!m.Success) return false;
            CurrentAnswer.Info += temp.Substring(temp.IndexOf(m.Value) + m.Value.Length);
            while (true)
            {
                //;
                //s = GetCurrentParaText();
                if (!MoveNext())
                {
                    break;
                }
                else
                {
                    if (!IsNewCtype())
                        CurrentAnswer.Info += GetCurrentParaText();
                    else
                    {
                        MoveBack();
                        break;
                    }
                }
            }
            return true;
        }

        public void ReadAnswer()
        {

            Match m = Regex.Match(GetCurrentParaText().TrimStart(), rexAnswer2);

            Match m1 = m.NextMatch();
            if (m1.Success)//判断当前段中是否包含多个小题答案。
            {
                do
                {
                    CurrentAnswer = new ImportAnswer();
                    GetAnswer(m.Value);
                    Paper.Answers.Add(CurrentAnswer);
                    m = m1;
                    m1 = m1.NextMatch();
                } while (m1.Success);
            }
            GetAnswer(m.Value);

            //判断下一行是否为本题的延续
            bool end = false;
            do
            {
                if (MoveNext())
                {
                    if (!IsNewCtype())
                        CurrentAnswer.Answer += GetCurrentParaText();
                    else
                    {
                        if (!IsAnswerRemark())
                        {
                            end = true;
                            MoveBack();

                        }

                    }
                }
                else
                {
                    end = true;
                }
            } while (!end);
            Paper.Answers.Add(CurrentAnswer);
        }
        void ReadAnswerList()
        {
            InitAnswerImport();
            while (MoveNext())
            {
                var temp = GetCurrentParaText();
                if (IsAnswerBigNum())
                {

                }
                else if (IsQuestionNum())
                {
                    ReadAnswer();
                }
            }
            Paper.CombineAnswer();
        }

        Word.Application wordApplication;
        NetOffice.WordApi.Document doc;
        public ImportPaper GetPaperFormFile(string file)
        {

            wordApplication = new Word.Application();
            wordApplication.DisplayAlerts = WdAlertLevel.wdAlertsNone;
            // create a utils instance, not need for but helpful to keep the lines of code low
            Word.Tools.CommonUtils utils = new Word.Tools.CommonUtils(wordApplication);
            doc = wordApplication.Documents.Open(file);
            InitImporter();
            handler.Setting(doc, wordApplication);
            paras = doc.Paragraphs;
            SetSubAndSup();
            ReadTitle();

            //ImageHandler handler = new ImageHandler();
            //Thread t = new Thread(handler.GetImages);
            //t.SetApartmentState(ApartmentState.STA);
            //t.Start();
            do
            {
                if (IsBigQuestion()) { }
                else if (IsQuestionNum())
                {
                    ReadQuestion();
                }
                else if (IsAnswerList())
                {

                    ReadAnswerList();
                }

            } while (MoveNext());
            
            wordApplication.Quit();
            wordApplication.Dispose();
            Paper.Standardize();
            return Paper;
        }
    }






    public class ImageHandler
    {

        private System.Drawing.Bitmap BitmapFromSource(BitmapSource bitmapsource)
        {
            System.Drawing.Bitmap bitmap;
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();

                enc.Frames.Add(BitmapFrame.Create(bitmapsource));
                enc.Save(outStream);
                bitmap = new System.Drawing.Bitmap(outStream);
            }
            return bitmap;
        }
        Word.Document doc;
        Word.Application app;
        Word.InlineShapes shapes;
        public string ImagePath
        {
            get { return imgPath; }
            set { imgPath = value; imgLocalDir = CUrl.MapPath( imgPath); }
        }
        string imgPath;
        string imgLocalDir;
        List<string> imageFiles = new List<string>();

        public List<string> ImageFiles
        {
            get { return imageFiles; }
        }

        public void Setting(Word.Document doc, Word.Application app)
        {
            this.doc = doc;
            this.app = app;
        }
        public void Setting(Word.InlineShapes shapes)
        {
            this.shapes = shapes;

        }

        public void GetImagesFromShapes()
        {
            imageFiles.Clear();
            int i = 0;
            foreach (Word.InlineShape ish in shapes)
            {
                if (ish.Type != NetOffice.WordApi.Enums.WdInlineShapeType.wdInlineShapeWebVideo)
                {

                    ish.Select();
                    app.Selection.Copy();
                    //System.Windows.Clipboard.getd
                    var image = BitmapFromSource(System.Windows.Clipboard.GetImage());
                    //image.Metadata

                    Bitmap bitmap = new Bitmap(image);
                    string file = Guid.NewGuid().ToString() + ".jpg";
                    bitmap.Save(imgLocalDir + file);
                    ImageFiles.Add(imgPath + file);
                    i++;
                }
            }
        }
        public void GetImages()
        {
            imageFiles.Clear();
            int i = 0;
            foreach (Word.InlineShape ish in doc.InlineShapes)
            {
                if (ish.Type != NetOffice.WordApi.Enums.WdInlineShapeType.wdInlineShapeWebVideo)
                {

                    ish.Select();
                    app.Selection.Copy();
                    var image = BitmapFromSource(System.Windows.Clipboard.GetImage());
                    //image.Metadata

                    Bitmap bitmap = new Bitmap(image);
                    string file = Guid.NewGuid().ToString() + ".jpg";
                    bitmap.Save(imgLocalDir + file);
                    ImageFiles.Add(imgPath + file);
                    i++;
                }
            }
        }
    }
}