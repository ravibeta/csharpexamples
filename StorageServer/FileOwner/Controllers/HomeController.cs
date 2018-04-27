using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FileOwner.Models;
using System.IO;
using System.Drawing;

namespace FileOwner.Controllers
{
    public class HomeController : Controller
    {
        FileOwnerEntities FileOwnerConnection = new FileOwnerEntities();
        public ActionResult Index()
        {
            ViewBag.Message = "List of registered Files";


            return View(FileOwnerConnection.Files.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Register Files and Owners on this site.";

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
            ViewBag.Message = "Create File Owner Association";
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
                    var owner = FileOwnerConnection.Owners.FirstOrDefault(x => x.Name.ToLower() == ownerName.ToLower());
                    if (owner == null)
                    {
                        owner = new Owner { Name = ownerName };
                        FileOwnerConnection.Owners.Add(owner);
                        FileOwnerConnection.SaveChanges();
                    }

                    File d = new File() { Name = name, Owner = owner, Image = data };
                    FileOwnerConnection.Files.Add(d);
                    FileOwnerConnection.SaveChanges();

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
            ViewBag.Message = "Details of File.";
            var File = FileOwnerConnection.Files.FirstOrDefault(x => x.Name.ToLower() == name);
            return View(File);
        }

        public ActionResult Search(string name, string ownerName)
        {
            ViewBag.Message = "Search by name or owner name.";
            var Files = new List<File>();
            if (string.IsNullOrEmpty(name) == false)
            {
                Files = FileOwnerConnection.Files.Where(x => x.Name.ToLower() == name.ToLower()).ToList();
                return View("Index", Files);
            }
            if (string.IsNullOrEmpty(ownerName) == false)
            {
                Files = FileOwnerConnection.Files.Where(x => x.Owner.Name.ToLower() == ownerName.ToLower()).ToList();
                return View("Index", Files);
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
