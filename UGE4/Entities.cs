using System.Data.Entity;
using UGE4.Models;

namespace UGE4
{
    public class Entities : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<UGE4.Entities>());

        public Entities() : base("name=Entities")
        {
        }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Chapter> Chapters { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<MCQ> MCQs { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<LinksToDisplay> LinksToDisplays { get; set; }

        public DbSet<Choice> Choices { get; set; }
    }
}
