using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackTraceDatabase;

namespace StackTraceWeb.Controllers
{
    public class HomeController : Controller
    {
        #region Members
        protected Adapter entites;
        #endregion

        public HomeController()
        {
            entites = new Adapter();
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Stack Trace Identification Service";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [HandleError]
        public ActionResult Dumps()
        {
            ViewData.Model = entites.GetAll();
            return View("Dumps", ViewData.Model);
        }

        [HandleError]
        public FileContentResult Download(int id)
        {
            var item = entites.Get(id);
            if (item != null && String.IsNullOrEmpty(item.DumpFile) == false && System.IO.File.Exists(item.DumpFile))
            {
                this.HttpContext.Response.Clear();
                this.HttpContext.Response.ContentType = "application";
                this.HttpContext.Response.AddHeader("content-disposition", "attachment; filename=\"" + item.DumpFile + "\"");
                this.HttpContext.Response.WriteFile(item.DumpFile);
                this.HttpContext.Response.End();
                using (var reader = new System.IO.StreamReader(item.DumpFile))
                    return new FileContentResult(System.IO.File.ReadAllBytes(item.DumpFile), "application");
            }
            else
            {
                return null;
            }
        }
    }
}
