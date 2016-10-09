/// <summary>
/// //////
/// </summary>

using System;
using System.Collections.Generic;
/* Controller Home
The controller provides method to return main page
*/
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelWebSite.Models;

namespace TravelWebSite.Controllers
{
    public class HomeController : Controller
    {
   
        public ActionResult Index()
        {
            return View();
        }
    
    }
}