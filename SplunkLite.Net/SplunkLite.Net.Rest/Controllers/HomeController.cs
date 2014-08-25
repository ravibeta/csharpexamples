using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SplunkLite.Net.Rest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to SplunkLite.Net";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "This searches events.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Please email rravishankar@gmail.com";

            return View();
        }
    }
}
