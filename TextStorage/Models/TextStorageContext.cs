using System.Data.Entity;

namespace TextStorage.Models
{
    public class TextStorageContext : DbContext
    {    
        public TextStorageContext() : base("TextStorage")
        {
        }

        public DbSet<Term> Terms { get; set; }
        public DbSet<Text> Texts { get; set; }
        public DbSet<TermDescription> Descriptions { get; set; }
    }
}
