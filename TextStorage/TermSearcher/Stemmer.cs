using Iveonik.Stemmers;
using System;
using System.Linq;

namespace TextStorage.TermSearcher
{
    public class Stemmer
    {
        public static string Stem(string s)
        {
            var stemmer = new RussianStemmer();
            var separators = new char[] { ' ', '\t', '\r' };
            var result = s.Replace("́", string.Empty)
                .Split(separators, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => stemmer.Stem(t));
            return string.Join(" ", result);
        }
    }
}