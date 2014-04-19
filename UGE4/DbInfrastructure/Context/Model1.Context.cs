﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class UGEContext : DbContext
    {
        public UGEContext()
            : base("name=UGEContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleMistake> ArticleMistakes { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<LinksToDisplay> LinksToDisplays { get; set; }
        public DbSet<MCQ> MCQs { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<ReplyAgainstMistake> ReplyAgainstMistakes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<WatchedReference> WatchedReferences { get; set; }
        public DbSet<WishList> WishLists { get; set; }
    }
}