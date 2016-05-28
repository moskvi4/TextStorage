
namespace TextStorage.Models
{
    public class TermDescription
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int TermId { get; set; }
        public virtual Term Term { get; set; }
        public int TextId { get; set; }
        public virtual Text Text { get; set; }
    }
}
