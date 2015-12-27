//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineExam.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TestPaper
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TestPaper()
        {
            this.Paper_AnswerList = new HashSet<Paper_AnswerList>();
            this.Paper_Choice = new HashSet<Paper_Choice>();
            this.Paper_Essay = new HashSet<Paper_Essay>();
            this.Paper_QuestionCategory = new HashSet<Paper_QuestionCategory>();
            this.PaperCollection = new HashSet<PaperCollection>();
        }
    
        public string ID { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public bool IsReady { get; set; }
        public string ExamType { get; set; }
        public int SubjectID { get; set; }
        public System.DateTime ExamTime { get; set; }
        public Nullable<int> TimeDuration { get; set; }
        public int Active { get; set; }
        public int Audit { get; set; }
        public string ModificationTeacher { get; set; }
        public string ModificationTeacherID { get; set; }
        public System.DateTime ModificationDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Paper_AnswerList> Paper_AnswerList { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Paper_Choice> Paper_Choice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Paper_Essay> Paper_Essay { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Paper_QuestionCategory> Paper_QuestionCategory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaperCollection> PaperCollection { get; set; }
    }
}
