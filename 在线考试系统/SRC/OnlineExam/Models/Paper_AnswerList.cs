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
    
    public partial class Paper_AnswerList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Paper_AnswerList()
        {
            this.Paper_AnswerList_Item = new HashSet<Paper_AnswerList_Item>();
        }
    
        public long AnserListID { get; set; }
        public string PaperID { get; set; }
        public string StudentID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Paper_AnswerList_Item> Paper_AnswerList_Item { get; set; }
        public virtual TestPaper TestPaper { get; set; }
    }
}
