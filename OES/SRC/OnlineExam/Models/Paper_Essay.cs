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
    
    public partial class Paper_Essay
    {
        public string PaperID { get; set; }
        public long QuestionID { get; set; }
        public double Score { get; set; }
        public int BigQuestionNumber { get; set; }
        public int SmallQuestionNumber { get; set; }
    
        public virtual Paper_QuestionCategory Paper_QuestionCategory { get; set; }
        public virtual QuestionEssay QuestionEssay_ { get; set; }
        public virtual TestPaper TestPaper { get; set; }
    }
}
