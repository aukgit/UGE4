//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UGE4.DbInfrastructure
{
    using System;
    using System.Collections.Generic;
    
    public partial class Book
    {
        public Book()
        {
            this.Chapters = new HashSet<Chapter>();
        }
    
        public int BookID { get; set; }
        public byte SubjectID { get; set; }
        public string BookName { get; set; }
    
        public virtual Subject Subject { get; set; }
        public virtual ICollection<Chapter> Chapters { get; set; }
    }
}
