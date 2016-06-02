using TextStorage.Models;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextStorage.TermSearcher
{
    public class RegexpSearcher
    {
        const string REG_EXP_SENTENSE = @"\.*\s*[А-Я][а-яА-Я́\s]+\s*(\([^\)]*\))?\s+([—])\s[^\.\n]*[\.\n]\s*";
        const string REG_EXP_TERM = @"[А-Я][а-яА-Я́\s]+";
        //тут є "Знак ударения" з кодом 769

        public static async Task<int> SaveTerms(Text textEntity, TextStorageContext dbContext)
        {
            var regex = new Regex(REG_EXP_SENTENSE, RegexOptions.CultureInvariant);
            int resultCount = 0;
            foreach (Match sentMatch in regex.Matches(textEntity.TextContentOriginal))
            {
                var value = sentMatch.Value.Trim('.', ' ', '\n', '\t', '\r');
                var termMatch = Regex.Match(value, REG_EXP_TERM, RegexOptions.CultureInvariant);
                var term = termMatch.Value.Trim();
                var termStemmed = Stemmer.Stem(term);
                if (termStemmed.Length <= 2) continue;
                var termEntity = dbContext.Terms.Where(e => e.StemmedName == termStemmed).FirstOrDefault();
                if (termEntity == null)
                    termEntity = dbContext.Terms.Add(new Term()
                    {
                        Name = term,
                        StemmedName = termStemmed
                    });
                dbContext.Descriptions.Add(new TermDescription()
                {
                    Description = value,
                    TermId = termEntity.Id,
                    TextId = textEntity.Id                   
                });
                await dbContext.SaveChangesAsync();
                resultCount++;
            }
            return resultCount;
        }
    }
}