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
      using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Subject
    {
        public Subject()
        {
            this.Books = new HashSet<Book>();
        }
    
              [DisplayName("Subject ID")]      
      public byte SubjectID { get; set; }
        
              [DisplayName("Subject Name")]      
      [StringLength(50)]
      [Required]
public string SubjectName { get; set; }
    
              [DisplayName("Books")]      
      public virtual ICollection<Book> Books { get; set; }
    }
}
