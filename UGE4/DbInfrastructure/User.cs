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
    
    public partial class User
    {
        public User()
        {
            this.ArticleMistakes = new HashSet<ArticleMistake>();
            this.Bookmarks = new HashSet<Bookmark>();
            this.Ratings = new HashSet<Rating>();
            this.ReplyAgainstMistakes = new HashSet<ReplyAgainstMistake>();
            this.WishLists = new HashSet<WishList>();
        }
    
        public int UserID { get; set; }
        public string LogName { get; set; }
    
        public virtual ICollection<ArticleMistake> ArticleMistakes { get; set; }
        public virtual ICollection<Bookmark> Bookmarks { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<ReplyAgainstMistake> ReplyAgainstMistakes { get; set; }
        public virtual ICollection<WishList> WishLists { get; set; }
    }
}