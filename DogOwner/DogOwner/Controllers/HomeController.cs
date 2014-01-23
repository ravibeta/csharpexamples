using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DogOwner.Models;
using System.IO;
using System.Drawing;

namespace DogOwner.Controllers
{
    public class HomeController : Controller
    {
        public DogOwnerEntities DogOwnerConnection = new DogOwnerEntities();
        public ActionResult Index()
        {
            ViewBag.Message = "List of registered dogs";


            return View(DogOwnerConnection.Dogs.ToList());
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

        [HandleError]
        public ActionResult Create(string name, string ownerName, HttpPostedFileBase file)
        {
            ViewBag.Message = "Create Dog Owner Association";
            if (name == null || ownerName == null) return View();

            try
            {
                // Verify that the user selected a file
                if (file != null && file.ContentLength > 0)
                {
                    // Get file info
                    var fileName = Path.GetFileName(file.FileName);
                    var contentLength = file.ContentLength;
                    var contentType = file.ContentType;

                    // Get file data
                    byte[] data = new byte[] { };
                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        data = binaryReader.ReadBytes(file.ContentLength);
                    }

                    // Save to database
                    var owner = DogOwnerConnection.Owners.FirstOrDefault(x => x.Name.ToLower() == ownerName.ToLower());
                    if (owner == null)
                    {
                        owner = new Owner { Name = ownerName };
                        DogOwnerConnection.Owners.Add(owner);
                        DogOwnerConnection.SaveChanges();
                    }

                    Dog d = new Dog() { Name = name, Owner = owner, Image = data };
                    DogOwnerConnection.Dogs.Add(d);
                    DogOwnerConnection.SaveChanges();

                    // Show success ...
                    return RedirectToAction("Index");
                }
                else
                {
                    // Show error ...
                    return View("Error");
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public ActionResult Detail(string name)
        {
            ViewBag.Message = "Details of Dog.";
            var dog = DogOwnerConnection.Dogs.FirstOrDefault(x => x.Name.ToLower() == name);
            return View(dog);
        }

        public ActionResult Search(string name, string ownerName)
        {
            ViewBag.Message = "Search by name or owner name.";
            var dogs = new List<Dog>();
            if (string.IsNullOrEmpty(name) == false)
            {
                dogs = DogOwnerConnection.Dogs.Where(x => x.Name.ToLower() == name.ToLower()).ToList();
                return View("Index", dogs);
            }
            if (string.IsNullOrEmpty(ownerName) == false)
            {
                dogs = DogOwnerConnection.Dogs.Where(x => x.Owner.Name.ToLower() == ownerName.ToLower()).ToList();
                return View("Index", dogs);
            }
            return View();
        }

        private Image ResizeImage(Image img, int width, int height)
        {
            Bitmap b = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage((Image)b))
            {
                g.DrawImage(img, 0, 0, width, height);
            }

            return (Image)b;
        }

        private Image ToImage(Byte[] bytes)
        {
            Image image = null;
            using (var mem = new MemoryStream(bytes))
                image = Image.FromStream(mem);
            return image;
        }
    }
}
