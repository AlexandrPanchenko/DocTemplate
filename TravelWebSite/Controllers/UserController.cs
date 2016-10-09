/* Controller User
The controller provides methods for interacting with the user
*/

using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;
using NReco.PdfGenerator;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using TravelWebSite.Models;

namespace TravelWebSite.Controllers
{
    public class UserController : Controller
    {
        private IDocRepository db;
        public UserController(IDocRepository repository)
        {
            db = repository;
        }
        public UserController()
        {
            db = new DocumentRepository();
        }
 
        /// <summary>
        /// This method returns all the documents from the database
        /// </summary>
    
        public ActionResult Index()
        {
            List<Models.Document> list = db.GetDocumentsList();
            if (list.Count==0) ViewBag.Error = "Адміністратор не створив жодного запису, зайдіть пізніше";
            return View(db.GetDocumentsList());
        }
    
        public ActionResult FillPage(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Document doc = db.GetDocumentById(id);
            ViewBag.EmptyFields = doc.Tags.ToList();
            ViewBag.Fields = doc.Tags.ToList();
            if (doc == null)
            {
                return HttpNotFound();
            }
            return View(doc);
        }
        /// <summary>
        /// ////Method for generating a Pdf document
        /// </summary>
        /// <param name="DocumentBody"></param>
        /// <param name="inputFields"></param>
        /// <param name="titleFields"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult FillPage(Models.Document DocumentBody, string[] inputFields, string[] titleFields)
        {
            
            if (ModelState.IsValid)
            {
                DocumentBody.Count++;
                db.Update(DocumentBody);
                string correctHtml = "";
                string htm = "<html><head><meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\"/><title>Контракт</title></head><body>"+ DocumentBody.Text+"</body></html>";
                ///
                string pattern = @"{{(?<val>.*?)}}";
                RegexOptions options = RegexOptions.Compiled | RegexOptions.Singleline;
                Regex regex = new Regex(pattern, options);
                Match match = regex.Match(htm);
                correctHtml = htm;

                
                        while (match.Success)
                        {
                        for (int j = 0; j < titleFields.Length; j++)
                        {
                        if (match.Groups["val"].Value == titleFields[j])
                        {
                         correctHtml = correctHtml.Replace("{{"+ match.Groups["val"].Value + "}}", inputFields[j]);
                         match = match.NextMatch();
                        }
                        }

                }
                ///


                var htmlToPdf = new HtmlToPdfConverter();
                var pdfBytes = htmlToPdf.GeneratePdf(correctHtml);

                return File(pdfBytes, "application/pdf", "df");
            }
            return View(DocumentBody);
        }

        /// <summary>
        /// Assistance in using a user
        /// </summary>

        public ActionResult Help()
        {
            return View();
        }
    }
}