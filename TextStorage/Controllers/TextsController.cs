using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData;
using TextStorage.Models;

namespace TextStorage.Controllers
{
    public class TextsController : ODataController
    {
        private TextStorageContext db = new TextStorageContext();

        // GET: odata/Texts
        [EnableQuery]
        public IQueryable<Text> GetTexts()
        {
            return db.Texts;
        }

        // GET: odata/Texts(5)
        [EnableQuery]
        public SingleResult<Text> GetText([FromODataUri] int key)
        {
            return SingleResult.Create(db.Texts.Where(text => text.Id == key));
        }

        // POST: odata/Texts
        public async Task<IHttpActionResult> Post()
        {
            var file = HttpContext.Current.Request.Files["UploadedText"];
            var fileName = HttpContext.Current.Request["UploadedTextName"];

            var streamReader = new StreamReader(file.InputStream);
            var textContent = streamReader.ReadToEnd();

            var newText = new Text()
            {
                Name = fileName,
                TextContent = textContent
            };

            db.Texts.Add(newText);
            await db.SaveChangesAsync();

            return Ok();
        }

        // DELETE: odata/Texts(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Text text = await db.Texts.FindAsync(key);
            if (text == null)
            {
                return NotFound();
            }

            db.Texts.Remove(text);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TextExists(int key)
        {
            return db.Texts.Count(e => e.Id == key) > 0;
        }
    }
}
