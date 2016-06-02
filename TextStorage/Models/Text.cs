using System.Collections.Generic;

namespace TextStorage.Models
{
    public class Text
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual string TextContent { get; set; }
        public virtual string TextContentOriginal { get; set; }
    }
}
