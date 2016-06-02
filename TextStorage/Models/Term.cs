using System.Collections.Generic;

namespace TextStorage.Models
{
    public class Term
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StemmedName { get; set; }

        public virtual ICollection<TermDescription> Descriptions { get; set; }

        public Term()
        {
            Descriptions = new List<TermDescription>();
        }
    }
}
