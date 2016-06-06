using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextStorage.Models;

namespace TextStorage.TermSearcher
{
    public class TermsUtils
    {
        public static void ApplyTerms(TextStorageContext dbContext)
        {
            var terms = dbContext.Terms.OrderByDescending(t => t.StemmedName.Length).ToList();
			if (terms.Count == 0) return;
            var maxWordsInTerm = terms.Max(t => t.StemmedName.Split(new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries).Length);
			string[] words = new string[maxWordsInTerm];
			string temp;
			int position;
			StringBuilder sb;
            foreach (var text in dbContext.Texts)
            {
				temp = "";
				position = 0;
				sb = new StringBuilder();

            }
        }
		private static bool IsSeparator(char c)
		{
			return c == ' ' || c == '\n' || c == '\t' || c == '\r' || c == '('
				|| c == ')' || c == '[' || c == ']' || c == '—';
		}
		private static int isTerm(string s, List<Term> terms)
		{
			var stemmedS = Stemmer.Stem(s);
			var term = terms.Where(t => t.StemmedName == stemmedS);
			if (terms.Count > 0)
				return terms.First().Id;
			else
				return -1;
		}
    }
}