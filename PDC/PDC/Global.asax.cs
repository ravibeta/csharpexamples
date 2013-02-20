using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data;

namespace PDC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            var ds = new DataSet();
            ds.Tables.Add();
            ds.Tables[0].Columns.Add(new DataColumn("Id", (new Int32()).GetType()));
            ds.Tables[0].Columns.Add(new DataColumn("FirstName"));
            ds.Tables[0].Columns.Add(new DataColumn("LastName"));
            ds.Tables[0].Columns.Add(new DataColumn("Email"));
            ds.Tables[0].Columns.Add(new DataColumn("Male"));
            ds.Tables[0].Columns.Add(new DataColumn("Height", (new double()).GetType()));
            ds.Tables[0].Columns.Add(new DataColumn("Weight", (new double()).GetType()));
            ds.Tables[0].Columns.Add(new DataColumn("BodyFat", (new double()).GetType()));
            ds.Tables[0].Columns.Add(new DataColumn("Neck", (new double()).GetType()));
            ds.Tables[0].Columns.Add(new DataColumn("Shoulders", (new double()).GetType()));
            ds.Tables[0].Columns.Add(new DataColumn("Chest", (new double()).GetType()));
            ds.Tables[0].Columns.Add(new DataColumn("Waist", (new double()).GetType()));
            ds.Tables[0].Columns.Add(new DataColumn("Hip", (new double()).GetType()));
            ds.Tables[0].Columns.Add(new DataColumn("ThighLeft", (new double()).GetType()));
            ds.Tables[0].Columns.Add(new DataColumn("ThighRight", (new double()).GetType()));
            ds.Tables[0].Columns.Add(new DataColumn("CalfLeft", (new double()).GetType()));
            ds.Tables[0].Columns.Add(new DataColumn("CalfRight", (new double()).GetType()));
            ds.Tables[0].Columns.Add(new DataColumn("ArmsLeft", (new double()).GetType()));
            ds.Tables[0].Columns.Add(new DataColumn("ArmsRight", (new double()).GetType()));
            ds.Tables[0].Columns.Add(new DataColumn("ForeArmLeft", (new double()).GetType()));
            ds.Tables[0].Columns.Add(new DataColumn("ForeArmRight", (new double()).GetType()));
            ds.AcceptChanges();
            Application["PDC"] = ds;
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}