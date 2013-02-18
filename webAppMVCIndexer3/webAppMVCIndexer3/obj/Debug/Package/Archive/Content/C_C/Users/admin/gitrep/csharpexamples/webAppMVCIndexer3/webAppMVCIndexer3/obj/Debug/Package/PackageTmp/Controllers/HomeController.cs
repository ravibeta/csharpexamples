using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webAppMVCIndexer3.Models;
using webappMVCIndexer3;

namespace webAppMVCIndexer3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Automated Document Indexing of unstructured text";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        

        public ActionResult Text(FormCollection form)
        {
            var req = new TextDataModel();
            if (form.Count != 0)
            {
                TryUpdateModel(req);
                if (ModelState.IsValid)
                {
                    var i = new Indexer();
                    if (String.IsNullOrEmpty(req.text) == false)
                        req.text = i.Process(req.text);
                    return View("Result", req);
                }
            }
            return View();
        }

    }
}
