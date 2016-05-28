using System.Data;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData;
using TextStorage.Models;

namespace TextStorage.Controllers
{
    public class TermDescriptionsController : ODataController
    {
        private TextStorageContext db = new TextStorageContext();

        // GET: odata/TermDescriptions
        [EnableQuery]
        public IQueryable<TermDescription> GetTermDescriptions()
        {
            return db.Descriptions;
        }

        // GET: odata/TermDescriptions(5)
        [EnableQuery]
        public SingleResult<TermDescription> GetTermDescription([FromODataUri] int key)
        {
            return SingleResult.Create(db.Descriptions.Where(termDescription => termDescription.Id == key));
        }

        // GET: odata/TermDescriptions(5)/Term
        [EnableQuery]
        public SingleResult<Term> GetTerm([FromODataUri] int key)
        {
            return SingleResult.Create(db.Descriptions.Where(m => m.Id == key).Select(m => m.Term));
        }

        // GET: odata/TermDescriptions(5)/Text
        [EnableQuery]
        public SingleResult<Text> GetText([FromODataUri] int key)
        {
            return SingleResult.Create(db.Descriptions.Where(m => m.Id == key).Select(m => m.Text));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TermDescriptionExists(int key)
        {
            return db.Descriptions.Count(e => e.Id == key) > 0;
        }
    }
}
