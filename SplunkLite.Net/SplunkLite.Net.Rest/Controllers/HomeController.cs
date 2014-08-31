using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SplunkLite.Net.Rest;

namespace SplunkLite.Net.Rest.Controllers
{
    public class HomeController : Controller
    {
        private SplunkLite.Net.Rest.EventsEntities entities = new SplunkLite.Net.Rest.EventsEntities();

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

        public ActionResult Events()
        {
            ViewBag.Message = "returns all events";
            return View(entities.Events);
        }

        public ActionResult Input(SplunkLite.Net.Rest.Models.InputItemModel item) 
        {
            ViewBag.Message = "adding raw data and extracting fields";
            if (item != null  && 
                String.IsNullOrWhiteSpace(item.Raw) == false)
            {
                // parse fields and store as xml in DB
            }
            return View();
        }
    }
}
