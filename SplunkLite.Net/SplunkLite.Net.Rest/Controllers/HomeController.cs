using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SplunkLite.Net.Rest;
using System.Xml.Linq;

//TODO : Run Stylecop
namespace SplunkLite.Net.Rest.Controllers
{
    public class HomeController : Controller
    {
        private SplunkLite.Net.Rest.EventsEntities entities = new SplunkLite.Net.Rest.EventsEntities();
        private SplunkLite.Net.Rest.EventsDBEntities dbentities = new SplunkLite.Net.Rest.EventsDBEntities();

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
                var fieldMap = new Dictionary<string, string>();
                var keyValues = item.Raw.Split(new char[] {','});
                var xmlTree = new XElement("xml");
                foreach (var keyValue in keyValues)
                {
                    var terms = keyValue.Split(new char[] { '=' });
                    var key = terms[0];
                    var value = terms[1];
                    fieldMap.Add(key, value);
                    xmlTree.Add(new XElement(key, value));
                }
                // TODO: add a different model
                dbentities.Events.Add(new Event() { Raw = item.Raw, Host = "Local", Source = "Local", SourceType = "Local", Timestamp = DateTime.Now, FieldMap = xmlTree.ToString() });
                dbentities.SaveChanges();
            }
            return View();
        }

        public ActionResult Search()
        {
            // TODO: xquery on FieldMap
            ViewBag.Message = "returns all events";
            return View(entities.Events);
        }
    }
}
