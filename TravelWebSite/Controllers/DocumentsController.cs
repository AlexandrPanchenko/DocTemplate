/* Controller Documents
The controller provides methods to interact with the database
*/

using NReco.PdfGenerator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using TravelWebSite.Models;

namespace TravelWebSite.Controllers
{
    public class DocumentsController : Controller
    {
        IDocRepository db;

        public DocumentsController(IDocRepository repository)
        {
            db = repository;
        }
        public DocumentsController()
        {
            db = new DocumentRepository();
        }
        /// <summary>
        /// This method returns all the documents from the database
        /// </summary>

        // GET: Documents
        public ActionResult Index()
        {
            return View(db.GetDocumentsList());
        }
        /// <summary>
        /// The method generates a sample pdf-file
        /// </summary>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.GetDocumentById(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            string correctHtml = document.Text;
            string htm = "<html><head><meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\"/><title>Контракт</title></head><body>" + document.Text + "</body></html>";
            ///
            string pattern = @"{{(?<val>.*?)}}";
            RegexOptions options = RegexOptions.Compiled | RegexOptions.Singleline;
            Regex regex = new Regex(pattern, options);
            Match match = regex.Match(htm);
            int i = 0;
            while (match.Success)
            {
                correctHtml = correctHtml.Replace("{{" + match.Groups["val"].Value + "}}", "______");
                match = match.NextMatch();
                i++;
            }
            ///
            var htmlToPdf = new HtmlToPdfConverter();
            var pdfBytes = htmlToPdf.GeneratePdf(correctHtml);
           
            return File(pdfBytes, "application/pdf", "df");
        }

        // <summary>
        /// Get Method create a new document in the database
        /// </summary>
        public ActionResult Create()
        {
            return View();
        }

        // <summary>
        /// Post Method create a new document in the database
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "DocumentID,Title,Count,Text")] Document document, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null) {
                    string imageFileName = Path.GetFileName(imageFile.FileName);
                    string imageExtension = Path.GetExtension(imageFile.FileName);
                    List<string> imageExtensions = new List<string>() { ".jpg", ".png" };
                    if (imageExtensions.Contains(imageExtension))
                    {
                        imageFile.SaveAs(Server.MapPath("~/Content/Upload/" + imageFileName));

                        ViewBag.Message = "Файл сохранен";
                    }
                    else
                    {
                        ViewBag.Message = "Ошибка расширения файлов ";
                    }
                }
                
                //
                string pattern = @"{{(?<val>.*?)}}";
                RegexOptions options = RegexOptions.Compiled | RegexOptions.Singleline;
                Regex regex = new Regex(pattern, options);
                if (document.Text != null)
                {
                Match match = regex.Match(document.Text);
             
                while (match.Success)
                {
                    if (document.Tags.Any(x => x.Name == match.Groups["val"].Value.Trim())) match = match.NextMatch();
                    else {
                        document.Tags.Add(new Tag { Name = match.Groups["val"].Value });
                        match = match.NextMatch();
                       }
                }
                //
                }
                db.Create(document);
              
                return RedirectToAction("Index");
            }

            return View(document);
        }
        // <summary>
        /// GET Method updates the document in the database
        /// </summary>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.GetDocumentById(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // <summary>
        /// Post Method updates the document in the database
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "DocumentID,Title,Count,Text")] Document document)
        {
            if (ModelState.IsValid)
            {
                db.Update(document);
                return RedirectToAction("Index");
            }
            return View(document);
        }
        // <summary>
        /// Get Method deletes the document in the database
        /// </summary>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.GetDocumentById(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }
        // <summary>
        /// Post Method deletes the document in the database
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.Delete(id);
            return RedirectToAction("Index");
        }
        // <summary>
        /// Get Method assistance in using the aministrator
        /// </summary>
        public ActionResult Help()
        {
            return View();
        }
        /// <summary>
        /// /////
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
