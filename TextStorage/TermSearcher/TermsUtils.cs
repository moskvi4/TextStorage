using System;
using System.Linq;
using TextStorage.Models;

namespace TextStorage.TermSearcher
{
    public class TermsUtils
    {
        public static void ApplyTerms(TextStorageContext dbContext)
        {
            var terms = dbContext.Terms.Select(t => new
            {
                Id = t.Id,
                StemmedName = t.StemmedName
            }).ToList();
            var maxWordsInTerm = terms.Max(t => t.StemmedName.Split(new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries).Length);
            foreach (var text in dbContext.Texts)
            {
                
            }
        }
    }
}