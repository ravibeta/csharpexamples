using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DogOwner.Models;

namespace DogOwner.Controllers
{
    public class HomeController : Controller
    {
        public DogOwnerEntities DogOwnerConnection = new DogOwnerEntities();
        public ActionResult Index()
        {
            ViewBag.Message = "List of registered dogs";
            var dogs = new List<DogOwner.Models.Dog>();

            DogOwnerConnection.Dogs.ToList().ForEach(x => dogs.Add(new DogOwner.Models.Dog {  Name = x.Name, OwnerName = x.Owner.Name}));
           
            return View(dogs);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Register Dogs and Owners on this site.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Please email ravibeta@hotmail.com for any questions/comments.";

            return View();
        }

        public ActionResult Create(string dogName, string ownerName)
        {
            ViewBag.Message = "Create Dog Owner Association";

            return View();
        }
    }
}
