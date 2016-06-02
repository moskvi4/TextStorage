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
        }
    }
}