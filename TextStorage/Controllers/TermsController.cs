using System.Data;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using TextStorage.Models;

namespace TextStorage.Controllers
{
    public class TermsController : ODataController
    {
        private TextStorageContext db = new TextStorageContext();

        // GET: odata/Terms
        [EnableQuery]
        public IQueryable<Term> GetTerms()
        {
            return db.Terms;
        }

        // GET: odata/Terms(5)
        [EnableQuery]
        public SingleResult<Term> GetTerm([FromODataUri] int key)
        {
            return SingleResult.Create(db.Terms.Where(term => term.Id == key));
        }

        // GET: odata/Terms(5)/Descriptions
        [EnableQuery]
        public IQueryable<TermDescription> GetDescriptions([FromODataUri] int key)
        {
            return db.Terms.Where(m => m.Id == key).SelectMany(m => m.Descriptions);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TermExists(int key)
        {
            return db.Terms.Count(e => e.Id == key) > 0;
        }
    }
}
