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
    
    public partial class Major_Subject
    {
        public int MajorID { get; set; }
        public int SubjectID { get; set; }
        public Nullable<int> Socre { get; set; }
    
        public virtual Major Major { get; set; }
        public virtual Subject Subject { get; set; }
    }
}