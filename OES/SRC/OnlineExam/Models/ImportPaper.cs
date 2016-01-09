using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
namespace OnlineExam.Models
{
    public class ImportPaper
    {
        [Key]
        public int ID { get; set; }
        public string ExamTitle { get; set; }//标题
        public string Grade { get; set; }//年级
        public string Year { get; set; }//试卷年份
        public string SubjectName { get; set; }
        public string SubSubjectName { get; set; }//科目的子名称(上册、下册、必修一等)
        public int SubjectId { get; set; }
        public string ExamType { get; set; }
        public ImportPaper()
        {
            ID = 0;
            SubjectId = -1;
            ExamTitle = ""; Grade = ""; Year = "";
            SubjectName = ""; SubSubjectName = "";
            TotleScore = 0;
            Answers = new List<ImportAnswer>();
            Questions = new List<ImportQuestion>();
        }
        public int TotleScore { get; set; }
        public int SingleChoiceQuantity { get; set; }
        public int SingleChoiceGrade { get; set; }
        public int MultipleChoiceQuantity { get; set; }
        public int MultipleChoiceGrade { get; set; }
        public int CompletionQuantity { get; set; }
        public int CompletionGrade { get; set; }
        public int ShortAnswerQuantity { get; set; }
        public int ShortAnswerGrade { get; set; }
        public int DiscussionQuantity { get; set; }
        public int DiscussionGrade { get; set; }
        public List<ImportAnswer> Answers { get; set; }
        public List<ImportQuestion> Questions { get; set; }
        public void CombineAnswer()
        {
            foreach (var a in Answers)
            {
                var query = Questions.Where(m => m.QuestionNum == a.Num && (a.BigNum == 0 || a.BigNum == m.BigNum));
                if (query.Count() == 1)
                {
                    var q = query.Single();
                    q.Answer = a.Answer;
                }
            }
        }
        //List<QuestionChoice> ChoiceList;
        //List<QuestionEssay> ShortAnswerList;
        //List<QuestionEssay> DiscussionList;
        //List<QuestionChoice> SingleChoiceList;
        //List<QuestionEssay> CompletionList;
        //List<Resource> QuestionResult;
        public void Standardize()
        {
            SingleChoiceQuantity = MultipleChoiceQuantity = ShortAnswerQuantity = DiscussionQuantity = CompletionQuantity = 0;
            foreach (var q in Questions)
            {
                q.Standardize();
                //统计各题型题数
                switch (q.QType)
                {
                    case QuestionType.SingleChoice:
                        SingleChoiceQuantity++;
                        break;
                    case QuestionType.MultipleChoice:
                        MultipleChoiceQuantity++;
                        break;
                    case QuestionType.Completion:
                        CompletionQuantity++;
                        break;
                    case QuestionType.Discussion:
                        DiscussionQuantity++;
                        break;
                    case QuestionType.ShortAnswer:
                    case QuestionType.UnKnown:
                        ShortAnswerQuantity++;
                        break;
                }
            }
            ExamEntities ee = new ExamEntities();
            var subjects = ee.Subject.ToList();
            foreach (var s in subjects)
            {
                if (ExamTitle.IndexOf(s.SubjectName) > 0)
                {
                    SubjectName = s.SubjectName;
                    SubjectId = s.SubjectID;
                }
            }
            string rex = "([12][0-9]{3})|([一二][零一二三四五六七八九]{3})";//匹配年份
            Match m = Regex.Match(ExamTitle, rex);
            if (m.Success)
            {
                Year = ParseCNNumber.ParseCn(m.Value).ToString();
            }
            rex = "[一二三四五六七八九十零]+(?=年级)";
            m = Regex.Match(ExamTitle, rex);
            if (m.Success)
            {
                Grade = ParseCNNumber.ParseCnToInt(m.Value).ToString();
            }
            rex = "(期末考试)|(自测题)|(模拟考试)|(期中考试)|(练习题)|(测验)";
            m = Regex.Match(ExamTitle, rex);
            if (m.Success)
            {
                ExamType = m.Value;
            }
            rex = "[上|下|第一|第二]学期";
            m = Regex.Match(ExamTitle, rex);
            if (m.Success)
            {
                SubSubjectName = m.Value;
            }
        }
    }
    public class ImportAnswer
    {
        public int BigNum { get; set; }//大题号
        public int Num { get; set; }//题目编号
        public string Answer { get; set; }//答案
        public string Info { get; set; }//题解、备注、评分标准
        public ImportAnswer()
        {
            Num = 0; Answer = ""; Info = "";
            BigNum = 0;
        }
    }
    public class ImportQuestion
    {
        readonly string rexFile = string.Format("(?<=(src=(\"|'){0}))[^\"']+(?=(\"|'))", CUrl.ImportPaperImage);//用于匹配导入题目中的问题；
        public int ID { get; set; }
        public string QuestionBigNum { get; set; }
        public int BigNum { get; set; }
        public int QuestionNum { get; set; }
        public QuestionType QType { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Info { get; set; }
        public List<Option> Options { get; set; }
        public List<string> Resources;
        public ImportQuestion()
        {
            QuestionBigNum = "";
            BigNum = -1;
            QuestionNum = -1;
            QType = QuestionType.UnKnown;
            Question = "";
            Answer = "";
            Info = "";
            Options = new List<Option>();
        }
        string TransferFile(string temp)
        {
            if (!string.IsNullOrWhiteSpace(temp))
            {
                Match m = Regex.Match(temp, rexFile);
                while (m.Success)
                {
                    var file = m.Value;
                    var dest = CUrl.QuestionResource+file;
                    var src = CUrl.ImportPaperImage+file;
                    //temp = temp.Replace(dest.Replace("/","\\"), src.Replace("/","\\"));
                    dest = CUrl.MapPath( dest);
                    src = CUrl.MapPath( src);
                    System.IO.File.Copy(src,dest);
                    m = m.NextMatch();
                }
                temp = temp.Replace(CUrl.ImportPaperImage, CUrl.QuestionResource);
            }
            return temp;
        }
        void Transfer()
        {
            Question = TransferFile(Question);
            Answer = TransferFile(Answer);
            foreach (var o in Options)
            {
                o.Content = TransferFile(o.Content);
            }
        }
        //选择题标准化
        public void Standardize()
        {
            string rexSingleOption = @"(\s?)([\(（]?)[A-Fa-f]([\)）、\.．，, ]).*?((?=\2[a-fA-F]\3)|(\r\n)|\n|$)"; //匹配选项 
            char[] c1 = { 'a', 'b', 'c', 'd', 'e', 'f' };
            char[] c2 = { 'A', 'B', 'C', 'D', 'E', 'F' };
            List<Option> newOps = new List<Option>();
            if (Options.Count > 0)
            {
                //选项中可能包含多个选项，需要拆分
                if (Options.Count < 3)
                {
                    var content = Options[0].Content;
                    Match m = Regex.Match(content, rexSingleOption);
                    while (m.Success)
                    {
                        Option op = new Option();
                        op.Op = m.Value.Substring(0, 1);
                        op.Op = m.Value.Substring(2);
                        newOps.Add(op);
                        m = m.NextMatch();
                    }
                    this.Options = newOps;
                }
            }
            if (Options.Count >= 1 && string.IsNullOrWhiteSpace(Answer))
            {
                //如果答案位于题目中，需要
                string rexAnswerInQuestion = @"(?<=[\(（])\s*[A-Fa-f]\s*(?=[）\)])";
                Match m = Regex.Match(Question, rexAnswerInQuestion);
                if (m.Success)
                {
                    Match m1 = m.NextMatch();
                    while (m1.Success)
                    {
                        m = m1;
                        m1 = m.NextMatch();
                    }
                    Answer = m.Value.Trim();
                    Question = Question.Substring(0, m.Index) + " " + Question.Substring(m.Index + m.Length);
                }
            }
            if (Options.Count > 1 && QType == QuestionType.UnKnown)
            {
                QType = QuestionType.MultipleChoice;
            }
        }
        public QuestionChoice ToChoice()
        {
            Transfer();
            QuestionChoice q = new QuestionChoice();
            q.Question = Question;
            foreach (var op in Options)
            {
                switch (op.Op)
                {
                    case "a":
                    case "A":
                        q.OptionA = op.Content;
                        break;
                    case "B":
                    case "b":
                        q.OptionB = op.Content;
                        break;
                    case "c":
                    case "C":
                        q.OptionC = op.Content;
                        break;
                    case "d":
                    case "D":
                        q.OptionD = op.Content;
                        break;
                    case "E":
                    case "e":
                        q.OptionE = op.Content;
                        break;
                    case "f":
                    case "F":
                        q.OptionF = op.Content;
                        break;
                }
            }
            int RightCount = 0;
            foreach (char c in Answer)
            {
                switch (c)
                {
                    case 'a':
                    case 'A':
                        q.AisTrue = true; RightCount++;
                        break;
                    case 'B':
                    case 'b':
                        q.BisTrue = true; RightCount++; break;
                    case 'c':
                    case 'C':
                        q.CisTrue = true; RightCount++; break;
                    case 'd':
                    case 'D':
                        q.DisTrue = true; RightCount++; break;
                    case 'E':
                    case 'e':
                        q.EisTrue = true; RightCount++; break;
                    default: break;
                }
            }
            q.IsMultiple = RightCount >= 2;
            q.Frequency = 1;
            q.DateAdded = DateTime.Now;
            q.Difficulty = 1;
            q.Audit = Auditing.Unreviewed;
            q.IsImport = true;
            q.ModificationTeacherID = HttpContext.Current.User.Identity.Name;
            q.ModificationTeacher = SessionHelper.UserProfile.RealName + "(从word导入)";
          
            return q;
        }
        public QuestionEssay ToEssay(string qt)
        {
            Transfer();
            QuestionEssay q = new QuestionEssay();
            q.Question = Question;
            q.QuestionType = qt;
            q.Answer = Answer;
            q.Frequency = 1;
            q.DateAdded = DateTime.Now;
            q.Difficulty = 1;
            q.Audit = Auditing.Unreviewed;
            //q.ModificationTeacher = HttpContext.Current.User.Identity.Name + "(从word导入)";
            q.IsImport = true;
            q.ModificationTeacherID = HttpContext.Current.User.Identity.Name;
            q.ModificationTeacher = SessionHelper.UserProfile.RealName + "(从word导入)";
            return q;
        }
        public object ToSpecificQuestion()
        {
            object o;
            switch (QType)
            {
                case QuestionType.SingleChoice:
                case QuestionType.MultipleChoice:
                    o = ToChoice();
                    break;
                case QuestionType.Completion:
                case QuestionType.Discussion:
                default:
                    var qt = QType == QuestionType.UnKnown ? QuestionType.ShortAnswer : QType;
                    o = ToEssay(qt.ToString());
                    break;
            }
            return o;
        }
    }
    public class Option
    {
        public string Op { get; set; }
        public string Content { get; set; }
        public Option(string o, string c) { Op = o; Content = c; }
        public Option(string o) { Op = o; }
        public Option()
        {
            Op = "";
            Content = "";
        }
    }
}