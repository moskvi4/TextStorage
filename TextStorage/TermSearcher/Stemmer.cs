using System.Text;

namespace TextStorage.TermSearcher
{
    public class Stemmer
    {
        public static string Stem(string s)
        {
            var sb = new StringBuilder(s.ToLower().Trim());
            return sb
                .Replace("а", string.Empty)
                .Replace("е", string.Empty)
                .Replace("є", string.Empty)
                .Replace("и", string.Empty)
                .Replace("і", string.Empty)
                .Replace("ї", string.Empty)
                .Replace("о", string.Empty)
                .Replace("у", string.Empty)
                .Replace("ю", string.Empty)
                .Replace("́", string.Empty)
                .Replace("я", string.Empty).ToString();

        }
    }
}