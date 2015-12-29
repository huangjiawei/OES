using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineExam.Models;
using System.Web.Mvc;

namespace OnlineExam
{
    public class Utitlity
    {
        static ExamEntities ee = new ExamEntities();
        static List<Subject> subjectList = ee.Subject.ToList();
        public static Subject GetSubjct(int sid)
        {
            var s = subjectList.Where(m => m.SubjectID == sid).SingleOrDefault();
            if (s == null)
            {
                subjectList = ee.Subject.ToList();
                s = subjectList.Where(m => m.SubjectID == sid).SingleOrDefault();
            }
            return s;

        }
        public static string GetChoiceAnswer(QuestionChoice q)
        {
            string r = "";
            if (q.AisTrue) r += "A";
            if (q.BisTrue) r += "B";
            if (q.CisTrue) r += "C";
            if (q.DisTrue) r += "D";
            if (q.EisTrue) r += "E";
            if (q.FisTrue) r += "F";
            return r;

        }
        //将字符串转换成QuestionType类型
        public static QuestionType ParseQuestionType(string s)
        {
            if (s == QuestionType.SingleChoice.ToString()) return QuestionType.SingleChoice;
            if (s == QuestionType.MultipleChoice.ToString()) return QuestionType.MultipleChoice;
            if (s == QuestionType.ShortAnswer.ToString()) return QuestionType.ShortAnswer;
            if (s == QuestionType.Discussion.ToString()) return QuestionType.Discussion;
            if (s == QuestionType.Completion.ToString()) return QuestionType.Completion;
            return QuestionType.UnKnown;
        }
        //对问题进行编码
        public static string EncodeQuestionID(long id, QuestionType qtype)
        {
            string code = "000000000000000000000" + id.ToString();
            code = code.Substring(code.Length - 12);
            string pre = Convert.ToChar((Convert.ToInt32('A') + Convert.ToInt32(qtype) - 1)).ToString();
            code = pre + code;
            return code;
        }

        private static List<SelectListItem> ExamTypeSelectList;
        //获取考试类型类型列表，用于填充前端页面
        public static List<SelectListItem> GetExamTypeSelectList()
        {
            if (ExamTypeSelectList == null)
            {
                ExamTypeSelectList = new List<SelectListItem>();
                SelectListItem i1 = new SelectListItem();
                SelectListItem i5 = new SelectListItem();
                SelectListItem i2 = new SelectListItem();
                SelectListItem i3 = new SelectListItem();
                SelectListItem i4 = new SelectListItem();
                i1.Value = i1.Text = ExamType.练习题.ToString();
                i2.Text = ExamType.自测题.ToString();
                i2.Value = ExamType.自测题.ToString();
                i3.Text = ExamType.模拟考试.ToString();
                i3.Value = ExamType.模拟考试.ToString();
                i4.Text = ExamType.期末考试.ToString();
                i4.Value = ExamType.期末考试.ToString();
                i5.Text = ExamType.期中考试.ToString();
                i5.Value = ExamType.期中考试.ToString();
                ExamTypeSelectList.Add(i1);
                ExamTypeSelectList.Add(i2);
                ExamTypeSelectList.Add(i3);
                ExamTypeSelectList.Add(i4);
                ExamTypeSelectList.Add(i5);
            }
            return ExamTypeSelectList;
        }
        private static List<SelectListItem> QuestionTypeSelectList;
        //获取问题类型列表，用于填充前端页面
        public static List<SelectListItem> GetQuestionTypeSelectList()
        {
            if (QuestionTypeSelectList == null)
            {
                QuestionTypeSelectList = new List<SelectListItem>();
                SelectListItem i1 = new SelectListItem();
                SelectListItem i2 = new SelectListItem();
                SelectListItem i3 = new SelectListItem();
                SelectListItem i4 = new SelectListItem();
                SelectListItem i5 = new SelectListItem();
                i1.Value = QuestionType.SingleChoice.ToString();
                i1.Text = QuestionType.SingleChoice.GetInfo();
                i2.Text = QuestionType.MultipleChoice.GetInfo();
                i2.Value = QuestionType.MultipleChoice.ToString();
                i3.Text = QuestionType.Completion.GetInfo();
                i3.Value = QuestionType.Completion.ToString();
                i4.Text = QuestionType.ShortAnswer.GetInfo();
                i4.Value = QuestionType.ShortAnswer.ToString();
                i5.Text = QuestionType.Discussion.GetInfo();
                i5.Value = QuestionType.Discussion.ToString();
                QuestionTypeSelectList.Add(i1);
                QuestionTypeSelectList.Add(i2);
                QuestionTypeSelectList.Add(i3);
                QuestionTypeSelectList.Add(i4);
                QuestionTypeSelectList.Add(i5);
            }
            return QuestionTypeSelectList;
        }
        //判断CkEditor控件的内容是否为空
        public static bool IsEditorEmpty(string txt)
        {
            if (txt == null || txt.Trim() == "" || txt.Trim() == "<p></p>") return true;
            return false;

        }
        public static QuestionChoice ModelStandardize(QuestionChoice q)
        {
            //if (IsEditorEmpty(q.Question)) q.Question = "";
            if (IsEditorEmpty(q.Description)) q.Description = "";
            if (IsEditorEmpty(q.OptionA)) q.OptionA = null;
            if (IsEditorEmpty(q.OptionB)) q.OptionB = null;

            //List<OptionItem> list = new List<OptionItem>();
            //if (!IsEditorEmpty(q.OptionD)) list.Add(new OptionItem(q.OptionD, q.DisTrue));
            //if (!IsEditorEmpty(q.OptionE)) list.Add(new OptionItem(q.OptionE, q.DisTrue));
            //if (!IsEditorEmpty(q.OptionF)) list.Add(new OptionItem(q.OptionF, q.DisTrue));

            #region 用于重组可选选项
            if (IsEditorEmpty(q.OptionD))
            {

                if (IsEditorEmpty(q.OptionE))
                {

                    if (IsEditorEmpty(q.OptionF))
                    {
                        //三个可选答案皆为空，不需重组
                    }
                    else
                    {
                        q.OptionD = q.OptionF;
                        q.DisTrue = q.FisTrue;
                        q.OptionF = "";
                        q.FisTrue = false;
                    }

                }
                else
                {
                    q.OptionD = q.OptionE;
                    q.DisTrue = q.EisTrue;
                    q.OptionE = "";
                    q.EisTrue = false;
                }
            }

            if (IsEditorEmpty(q.OptionE))
            {

                if (!IsEditorEmpty(q.OptionF))
                {
                    q.OptionE = q.OptionF;
                    q.EisTrue = q.EisTrue;
                    q.OptionF = "";
                    q.FisTrue = false;
                }
            }
            #endregion
            if (IsEditorEmpty(q.OptionC)) q.OptionC = "";
            if (IsEditorEmpty(q.OptionD)) q.OptionD = "";
            if (IsEditorEmpty(q.OptionE)) q.OptionE = "";
            if (IsEditorEmpty(q.OptionF)) q.OptionF = "";
            return q;
        }
        public static QuestionEssay ModelStandardize(QuestionEssay q)
        {
            //if (IsEditorEmpty(q.Question)) q.Question = "";
            if (IsEditorEmpty(q.Description)) q.Description = "";
            return q;
        }
        public static QuestionType ToQuestionType(string qt)
        {
            QuestionType t;
            switch (qt)
            {
                case "Discussion":
                    t = QuestionType.Discussion;
                    break;

                case "Completion":
                    t = QuestionType.Completion;
                    break;
                case "SingleChoice":
                    t = QuestionType.SingleChoice;
                    break;
                case "MultipleChoice":
                    t = QuestionType.MultipleChoice;
                    break;
                default:
                    t = QuestionType.ShortAnswer;
                    break;


            }
            return t;

        }
    }
    public class OptionItem
    {
        public string Answer;
        public bool IsRight;
        public OptionItem(string answer, bool isRight)
        {
            Answer = answer;
            IsRight = isRight;
        }

    }
    public enum ExamType
    {
        期末考试 = 1,
        自测题,
        模拟考试,
        练习题, 期中考试
    }

    public class CKey
    {
        public static readonly string DefaultSearchKeyWord = "查询关键字";
        public static readonly string TestEnd = "测试时间已到";

        public static readonly int DefaultPageCount = 10;

        public static readonly int DataListPageCount = 10;
        public static readonly int GridViewPageCount = 20;

        //grid表格中的题目字段的最大长度
        public static readonly int QuestionTitleSummaryLenght = 5;
        //grid表格中的题目的描述字段的最大长度
        public static readonly int QuestionDescriptionSummaryLenght = 4;

        //考试时缓存答案的间隔
        public static readonly int SaveAnswerInterval = 1000 * 60;



    }
    public class SessionString
    {
        public static readonly string ImportPaper = "ImportPaper";
        public static readonly string Profile = "Profile";
        public static readonly string TestEndTime = "TestEndTime";
    }
    class RoleName
    {
        public static readonly string Teacher = "Teacher";
        public static readonly string Editor = "Editor";
        public static readonly string Student = "Student";
        public static readonly string Auditor = "Auditor";
        public static readonly string Admin = "Admin";

    }
    class PermissionList
    {
        //管理员
        public static readonly string Edit_MajorAndSubject = "Edit_MajorAndSubject";
        public static readonly string Admin_Permission = "Admin_Permission";
        //教师、编辑者
        public static readonly string Edit_Question = "Edit_Question";
        public static readonly string Edit_Paper = "Edit_Paper";

        //审核者
        public static readonly string Audit_Question = "Audit_Question";
        public static readonly string Audit_Paper = "Audit_Paper";
        public static readonly string Read_Question = "Read_Question";
        public static readonly string Read_Paper = "Read_Paper";


    }
    public class SessionHelper
    {
        public static readonly string _StrImportPaper = "ImportPaper";
        public static readonly string _StrProfile = "Profile";
        public static readonly string _testPaper = "TestPaper";

        //public ImportPaper ImportPaper(string key)
        //{
        //    get { return (ImportPaper)HttpContext.Current.Session[_StrImportPaper]; }
        //    set
        //    {
        //        if (value is ImportPaper)
        //            HttpContext.Current.Session[_StrImportPaper] = value;
        //        else throw new Exception("类型错误");
        //    }
        //}

        //用于保存测试试题
        public static PaperGenerate TestPaper
        {
            get
            {
                return (PaperGenerate)HttpContext.Current.Session[_testPaper];


            }
            set
            {
                HttpContext.Current.Session[_testPaper] = value;
            }
        }
        public static readonly string _testAnswerList = "TestAnswerList";
        //用于缓存用户正在测试时临时保存的答案
        public static string[] TestAnswerList
        {
            get
            {
                return HttpContext.Current.Session[_testAnswerList] as string[];
            }
            set
            {
                HttpContext.Current.Session[_testAnswerList] = value;
            }
        }
        public static UserProfile UserProfile
        {
            get
            {
                var p = (UserProfile)HttpContext.Current.Session[_StrProfile];
                if (p == null)
                {
                    ExamEntities ee = new ExamEntities();
                    var user = HttpContext.Current.User.Identity.Name;
                    p = ee.UserProfile.Where(m => m.UserId == user).SingleOrDefault();
                }
                return p;
            }
            set
            {
                if (value is UserProfile)
                    HttpContext.Current.Session[_StrProfile] = value;
                else throw new Exception("类型错误");
            }
        }
        //public static UserProfile UserProfile
        //{
        //    get { return (UserProfile)HttpContext.Current.Session[_StrProfile]; }
        //    set
        //    {
        //        if (value is UserProfile)
        //            HttpContext.Current.Session[_StrProfile] = value;
        //        else throw new Exception("类型错误");
        //    }
        //}

    }

    public class Auditing
    {
        public static readonly int Pass = 1;//审核通过
        public static readonly int Fail = -1;//审核不通过i        
        public static readonly int Unreviewed = 0;//暂未审核

    }
    //判断是否处于激活可用状态
    public class Activity
    {
        public static readonly int Active = 1;
        public static readonly int Inactive = -1;

    }
    public enum EditType
    {
        Edit = 0,
        Create
    }
    public class CUrl
    {
        public static readonly string QuestionResourceDir = CUrl.MapPath("/QuestionBankResource/");
        public static readonly string QuestionResource = "/QuestionBankResource/";
        public static readonly string ImportPaper = "/QuestionBankResource/ImportPaper/";
        public static readonly string UserPhoto = "/Resource/UserPhoto/";
        public static readonly string ImportPaperImage = ImportPaper + "Image/";

        public static string MapPath(string path)
        {
            return HttpContext.Current.Server.MapPath(path);

        }
    }
    public enum QuestionType
    {
        UnKnown = 0,
        SingleChoice = 1,
        MultipleChoice,
        Completion,
        ShortAnswer,
        Discussion

    }
    public static class QuestionTypeExtensions
    {

        public static string GetInfo(this QuestionType me)
        {

            switch (me)
            {
                case QuestionType.UnKnown:
                    return "未知(默认设置成简答题)";
                case QuestionType.SingleChoice:
                    return "单选题";
                case QuestionType.MultipleChoice:
                    return "多选题";
                case QuestionType.ShortAnswer:
                    return "简答题/计算题";
                case QuestionType.Completion:
                    return "填空题";
                case QuestionType.Discussion:
                    return "议论题/应用题";
                default:
                    return "";
            }
        }

    }
    public enum DataView
    {
        GridView = 1,
        DataList = 2
    }
    public class JsonReturn
    {
        public int Success;
        public string Message;
        public string Result;
        public object Data;
    }
}
