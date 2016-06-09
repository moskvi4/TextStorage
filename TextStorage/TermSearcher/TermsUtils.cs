using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextStorage.Models;

namespace TextStorage.TermSearcher
{
	public class TermsUtils
	{
		public static void ApplyTerms(TextStorageContext dbContext) {
			var terms = dbContext.Terms.OrderByDescending(t => t.StemmedName.Length).ToList();
			if (terms.Count == 0)
				return;
			var maxWordsInTerm = terms.Max(t => t.StemmedName.Split(new char[] { ' ' },
				StringSplitOptions.RemoveEmptyEntries).Length);
			string[] words = new string[maxWordsInTerm];
			string[] temp = new string[maxWordsInTerm];
			string textOriginal = "";
			int position = 0;
			int termStartPosition = 0;
			StringBuilder sb;
			bool hasWordFlag = false;
			int termId = -1;
			foreach (var text in dbContext.Texts) {
				sb = new StringBuilder();
				textOriginal = text.TextContentOriginal;
				for (position = 0; position < textOriginal.Length; ) {
					while (position < textOriginal.Length && IsSeparatorAll(textOriginal[position])) {
						sb.Append(textOriginal[position]);
						position++;
					}
					words = new string[maxWordsInTerm];
					temp = new string[maxWordsInTerm];
					termStartPosition = position;
					for (int i = 0; i < maxWordsInTerm && position < textOriginal.Length; i++) {
						while (position < textOriginal.Length && !IsSeparatorAll(textOriginal[position])) {
							for (int j = i; j < maxWordsInTerm; j++) {
								hasWordFlag = true;
								words[j] += textOriginal[position];
								temp[j] += textOriginal[position];
							}
							position++;
						}
						while (position < textOriginal.Length && hasWordFlag && IsSeparator(textOriginal[position])) {
							for (int j = i; j < maxWordsInTerm; j++) {
								temp[j] += textOriginal[position];
							}
							position++;
						}
						for (int j = i; j < maxWordsInTerm && hasWordFlag; j++) {
							words[j] += " ";
						}
						hasWordFlag = false;
					}
					termId = -1;
					for (int i = maxWordsInTerm - 1; i >= 0; i--) {
						if (!string.IsNullOrWhiteSpace(words[i])) {
							termId = isTerm(words[i], terms);
							if (termId != -1) {
								sb.AppendFormat("<span id='term-{0}'>{1}</span>", termId, temp[i]);
								position = termStartPosition + temp[i].Length;
								break;
							}
						}
					}
					if (termId == -1) {
						sb.Append(temp[maxWordsInTerm - 1]);
					}
				}
				text.TextContent = sb.ToString();
			}
		}
		private static bool IsSeparatorAll(char c) {
			return c == ' ' || c == '\n' || c == '\t' || c == '\r'
				|| c == '(' || c == ')' || c == '[' || c == ']'
				|| c == '—' || c == '«' || c == '»' || c == '\''
				|| c == '"' || c == '.' || c == ',' || c == ';';
		}
		private static bool IsSeparator(char c) {
			return c == ' ' || c == '\r' || c == '\t';
		}
		private static int isTerm(string s, List<Term> terms) {
			var stemmedS = Stemmer.Stem(s);
			var term = terms.Where(t => t.StemmedName == stemmedS);
			if (term.Count() > 0)
				return term.First().Id;
			else
				return -1;
		}
	}
}