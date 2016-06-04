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
            var result = s.Replace("́", string.Empty)
                .Split(new char[]{ ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => stemmer.Stem(t));
            return string.Join(" ", result);
        }
    }
}