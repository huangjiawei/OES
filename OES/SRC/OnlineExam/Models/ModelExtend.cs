using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime;
using System.ComponentModel.DataAnnotations;
namespace OnlineExam.Models
{
    //public partial class Paper_QuestionCategory
    //{
    //    public int RandomKey { get; set; }
    //}
    public partial class QuestionChoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuestionChoice()
        {
            ID = -1;
            Description = "";
            Frequency = 0;
            Difficulty = 0;
            DateAdded = DateTime.Now;
            ModificationDate = DateTime.Now;
            Audit = 0;
            OptionD = OptionE = OptionF = "";
            Info = "";
            this.Paper_Choice = new HashSet<Paper_Choice>();
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Paper_Choice> Paper_Choice { get; set; }
        public static void AppendSubjects(string sids, long qid)
        {
            ExamEntities ee = new ExamEntities();
            var temp = sids.Split(',');
            List<int> ids = new List<int>();
            List<Subject_Choice> ses = new List<Subject_Choice>();
            foreach (var i in temp)
            {
                ids.Add(int.Parse(i.Trim()));
                var t = new Subject_Choice();
                t.SubjectID = int.Parse(i.Trim());
                t.QuestionID = qid;
                ses.Add(t);
            }
            ee.Subject_Choice.RemoveRange(ee.Subject_Choice.Where(m => m.QuestionID == qid));
            //string sql = "delete Subject_Essay where QuestionId={0};insert into Subject_Essay(subjectid,questionid) values {2}"; 
            //sql=string.Format(sql,qid)
            ee.Subject_Choice.AddRange(ses.AsEnumerable());
            ee.SaveChanges();
        }
        public void AppendSubjects(string sids)
        {
            ExamEntities ee = new ExamEntities();
            var temp = sids.Split(',');
            long qid = ID;
            List<int> ids = new List<int>();
            List<Subject_Choice> ses = new List<Subject_Choice>();
            foreach (var i in temp)
            {
                ids.Add(int.Parse(i.Trim()));
                var t = new Subject_Choice();
                t.SubjectID = int.Parse(i.Trim());
                t.QuestionID = qid;
                ses.Add(t);
            }
            ee.Subject_Choice.RemoveRange(ee.Subject_Choice.Where(m => m.QuestionID == qid));
            //string sql = "delete Subject_Essay where QuestionId={0};insert into Subject_Essay(subjectid,questionid) values {2}"; 
            //sql=string.Format(sql,qid)
            ee.Subject_Choice.AddRange(ses.AsEnumerable());
            ee.SaveChanges();
        }
        [Display(Name = "编号")]
        public long ID { get; set; }
        [Required]
        [Display(Name = "题目")]
        public string Question { get; set; }
        [Required]
        [Display(Name = "选项A")]
        public string OptionA { get; set; }
        [Required]
        [Display(Name = "选项B")]
        public string OptionB { get; set; }
        [Display(Name = "选项C")]
        public string OptionC { get; set; }
        [Display(Name = "选项D")]
        public string OptionD { get; set; }
        [Display(Name = "选项E")]
        public string OptionE { get; set; }
        [Display(Name = "选项F")]
        public string OptionF { get; set; }
        public bool AisTrue { get; set; }
        public bool BisTrue { get; set; }
        public bool CisTrue { get; set; }
        public bool DisTrue { get; set; }
        public bool EisTrue { get; set; }
        public bool FisTrue { get; set; }
        public bool IsMultiple { get; set; }
        [Display(Name = "解析")]
        public string Info { get; set; }
        [Display(Name = "修改教师")]
        public string ModificationTeacher { get; set; }
        [Display(Name = "修改时间")]
        public Nullable<System.DateTime> ModificationDate { get; set; }
        [Display(Name = "添加日期")]
        public System.DateTime DateAdded { get; set; }
        [Display(Name = "难度系数")]
        public short Difficulty { get; set; }
        [Display(Name = "考试频率")]
        public short Frequency { get; set; }
        [Display(Name = "相关描述")]
        public string Description { get; set; }
        [Display(Name = "审核状态")]
        public int Audit { get; set; }
        [Display(Name = "评价")]
        public string Assessment { get; set; }
        [Display(Name = "修改人")]
        public string ModificationTeacherID { get; set; }
        [Display(Name = "审核人")]
        public string Auditor { get; set; }
        [Display(Name = "修改人ID")]
        public string AuditorID { get; set; }
        [Display(Name = "自动导入")]
        public bool IsImport { get; set; }
    }
    public partial class QuestionEssay
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Paper_Essay> Paper_Essay { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuestionEssay()
        {
            ID = -1;
            Description = "";
            Frequency = 0;
            Difficulty = 0;
            DateAdded = DateTime.Now;
            ModificationDate = DateTime.Now;
            Audit = 0;
            this.Paper_Essay = new HashSet<Paper_Essay>();
        }
        public void AppendSubjects(string sids)
        {
            ExamEntities ee = new ExamEntities();
            var temp = sids.Split(',');
            long qid = ID;
            List<int> ids = new List<int>();
            List<Subject_Essay> ses = new List<Subject_Essay>();
            foreach (var i in temp)
            {
                ids.Add(int.Parse(i.Trim()));
                var t = new Subject_Essay();
                t.SubjectID = int.Parse(i.Trim());
                t.QuestionID = qid;
                ses.Add(t);
            }
            ee.Subject_Essay.RemoveRange(ee.Subject_Essay.Where(m => m.QuestionID == qid));
            //string sql = "delete Subject_Essay where QuestionId={0};insert into Subject_Essay(subjectid,questionid) values {2}"; 
            //sql=string.Format(sql,qid)
            ee.Subject_Essay.AddRange(ses.AsEnumerable());
            ee.SaveChanges();
        }
        public static void AppendSubjects(string sids, long qid)
        {
            ExamEntities ee = new ExamEntities();
            var temp = sids.Split(',');
            List<int> ids = new List<int>();
            List<Subject_Essay> ses = new List<Subject_Essay>();
            foreach (var i in temp)
            {
                ids.Add(int.Parse(i.Trim()));
                var t = new Subject_Essay();
                t.SubjectID = int.Parse(i.Trim());
                t.QuestionID = qid;
                ses.Add(t);
            }
            ee.Subject_Essay.RemoveRange(ee.Subject_Essay.Where(m => m.QuestionID == qid));
            //string sql = "delete Subject_Essay where QuestionId={0};insert into Subject_Essay(subjectid,questionid) values {2}"; 
            //sql=string.Format(sql,qid)
            ee.Subject_Essay.AddRange(ses.AsEnumerable());
            ee.SaveChanges();
        }
        [Display(Name = "编号")]
        public long ID { get; set; }
        [Required]
        [Display(Name = "题目")]
        public string Question { get; set; }
        [Display(Name = "题目类型")]
        public string QuestionType { get; set; }
        [Display(Name = "答案")]
        public string Answer { get; set; }
        [Display(Name = "解析")]
        public string Info { get; set; }
        [Display(Name = "修改教师")]
        public string ModificationTeacher { get; set; }
        [Display(Name = "修改时间")]
        public Nullable<System.DateTime> ModificationDate { get; set; }
        [Display(Name = "添加日期")]
        public System.DateTime DateAdded { get; set; }
        [Display(Name = "难度系数")]
        public short Difficulty { get; set; }
        [Display(Name = "考试频率")]
        public short Frequency { get; set; }
        [Display(Name = "相关描述")]
        public string Description { get; set; }
        [Display(Name = "审核状态")]
        public int Audit { get; set; }
        [Display(Name = "评价")]
        public string Assessment { get; set; }
        [Display(Name = "修改人")]
        public string ModificationTeacherID { get; set; }
        [Display(Name = "审核人")]
        public string Auditor { get; set; }
        [Display(Name = "修改人ID")]
        public string AuditorID { get; set; }
        [Display(Name = "自动导入")]
        public bool IsImport { get; set; }
    }
    public partial class Paper_QuestionCategory
    {
        public Paper_QuestionCategory(string paperId,int qnumber, QuestionType qtype, double score,int quantity)
        {
            this.PaperID = paperId;
            this.Quantity = quantity;
            this.QuestionType = qtype.ToString();
            this.Sequence = qnumber;
            this.ScorePerQuestion = score;
            this.Title = qtype.GetInfo();
            
        }
    }
    //    public partial class TestPaper
    //    {
    //        QuestionChoice qb;
    //        public void AutoGenerate()
    //        {
    //            ExamEntities ee = new ExamEntities();
    //            var Choice = new List<QuestionChoice>();
    //            var Essay = new List<QuestionEssay>();
    //#if DEBUG
    //            //不做科目筛选
    //            var choiceids = ee.QuestionChoice.Select(m => m.ID);
    //            var essayids = ee.QuestionEssay.Select(m => m.ID);
    //#else
    //            var choiceids = ee.Subject_Choice.Where(m => m.SubjectID == Paper.SubjectID).Select(m => m.QuestionID);
    //            var essayids = ee.Subject_Essay.Where(m => m.SubjectID == Paper.SubjectID).Select(m => m.QuestionID);
    //#endif
    //            var singlechoice = (from s in ee.QuestionChoice
    //                                where s.IsMultiple == false && choiceids.Contains(s.ID)
    //                                orderby Guid.NewGuid()
    //                                select s).Take(this.SingleChoiceQuantity);
    //            var multiplechoice = (from s in ee.QuestionChoice
    //                                  where s.IsMultiple == true && choiceids.Contains(s.ID)
    //                                  orderby Guid.NewGuid()
    //                                  select s).Take(this.MultipleChoiceQuantity);
    //            Choice.AddRange(singlechoice.Union(multiplechoice));
    //            var shortanswer = (from s in ee.QuestionEssay
    //                               where s.QuestionType == QuestionType.ShortAnswer.ToString() && essayids.Contains(s.ID)
    //                               orderby Guid.NewGuid()
    //                               select s).Take(this.ShortAnswerQuantity);
    //            var completion = (from s in ee.QuestionEssay
    //                              where s.QuestionType == QuestionType.Completion.ToString() && essayids.Contains(s.ID)
    //                              orderby Guid.NewGuid()
    //                              select s).Take(this.CompletionQuantity);
    //            var discussion = (from s in ee.QuestionEssay
    //                              where s.QuestionType == QuestionType.Discussion.ToString() && essayids.Contains(s.ID)
    //                              orderby Guid.NewGuid()
    //                              select s).Take(this.DiscussionQuantity);
    //            Essay.AddRange(shortanswer.Union(completion).Union(discussion));
    //        }
    //    }
    //导入的时候，根据题型导入
    //如果题型
    public class QuestionBase
    {
        public long ID { get; set; }
        public string Question { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string OptionE { get; set; }
        public string OptionF { get; set; }
        public bool AisTrue { get; set; }
        public bool BisTrue { get; set; }
        public bool CisTrue { get; set; }
        public bool DisTrue { get; set; }
        public bool EisTrue { get; set; }
        public bool FisTrue { get; set; }
        public string ModificationTeacher { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
        public System.DateTime DateAdded { get; set; }
        public short Difficulty { get; set; }
        public short Frequency { get; set; }
        public string Description { get; set; }
        public int Audit { get; set; }
        public string Assessment { get; set; }
        public void ToQuestion(QuestionType qt)
        {
        }
    }
}