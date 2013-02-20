using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PDC.Models;
using System.Data;

namespace PDC.Controllers
{
    public class HomeController : Controller
    {
        DataSet ds;
        
        public HomeController()
        {
            // ds = this.HttpContext.Application["PDC"] as DataSet;
            ds = this.ControllerContext.HttpContext.ApplicationInstance.Application["PDC"] as DataSet;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Empowered Nutrition Products Inc.";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection fc)
        {
            var m = new PersonalDataModel();
            // Deserialize (Include white list!)
            TryUpdateModel(m);


            // If valid, save request to Database
            if (ModelState.IsValid && ds != null)
            {
                var result = new Results();
                //var dr = ds.Tables[0].NewRow();
                //dr.ItemArray = new object[] { 
                //                        m.FirstName,
                //                        m.LastName,
                //                        m.Email,
                //                        m.Male,
                //                        m.Height,
                //                        m.Weight,
                //                        m.BodyFat,
                //                        m.Neck,
                //                        m.Shoulders,
                //                        m.Chest,
                //                        m.Waist,
                //                        m.Hip,
                //                        m.ThighLeft,
                //                        m.ThighRight,
                //                        m.CalfLeft,
                //                        m.CalfRight,
                //                        m.ArmsLeft,
                //                        m.ArmsRight,
                //                        m.ForeArmLeft,
                //                        m.ForeArmRight };

                //dr.AcceptChanges();
                //ds.AcceptChanges();
                result.BMI = (m.Height != 0) ? m.Weight / ((m.Height / 100) * (m.Height / 100)) : 0;
                result.LBM = m.Male ?
                    (0.32810 * m.Weight) + (0.33929 * m.Height) - 29.5336 :
                    (0.29569 * m.Weight) + (0.41813 * m.Height) - 43.2933;
                result.TFM = m.Height != 0 ? ((100 * m.Hip) / (m.Height * Math.Sqrt(m.Height))) - 18 : 0;
                return View("Result", result);
            }
            
            
            // Otherwise, reshow form
            return View();
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        
    }
}
