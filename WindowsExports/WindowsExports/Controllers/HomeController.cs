using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WindowsExports.Models;
using System.IO;
using System.Drawing;

namespace WindowsExports.Controllers
{
    public class HomeController : Controller
    {
        public WindowsExportsEntities WindowsExportsConnection = new WindowsExportsEntities();
        public ActionResult Index()
        {
            ViewBag.Message = "List of registered Commands";


            return View(WindowsExportsConnection.Commands.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Register Commands and Owners on this site.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Please email rajamani@adobe.com for any questions/comments.";

            return View();
        }

        [HandleError]
        public ActionResult Create(string name, string ownerName, HttpPostedFileBase file)
        {
            ViewBag.Message = "Create Command Owner Association";
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
                    var owner = WindowsExportsConnection.Owners.FirstOrDefault(x => x.Name.ToLower() == ownerName.ToLower());
                    if (owner == null)
                    {
                        owner = new Owner { Name = ownerName };
                        WindowsExportsConnection.Owners.Add(owner);
                        WindowsExportsConnection.SaveChanges();
                    }

                    Commands d = new Commands() { Name = name, Owner = owner, Binary = data };
                    WindowsExportsConnection.Commands.Add(d);
                    WindowsExportsConnection.SaveChanges();

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
            ViewBag.Message = "Details of Command.";
            var Command = WindowsExportsConnection.Commands.FirstOrDefault(x => x.Name.ToLower() == name);
            return View(Command);
        }

        public ActionResult Search(string name, string ownerName)
        {
            ViewBag.Message = "Search by name or owner name.";
            var Commands = new List<Commands>();
            if (string.IsNullOrEmpty(name) == false)
            {
                Commands = WindowsExportsConnection.Commands.Where(x => x.Name.ToLower() == name.ToLower()).ToList();
                return View("Index", Commands);
            }
            if (string.IsNullOrEmpty(ownerName) == false)
            {
                Commands = WindowsExportsConnection.Commands.Where(x => x.Owner.Name.ToLower() == ownerName.ToLower()).ToList();
                return View("Index", Commands);
            }
            return View();
        }

        //private Binary ResizeBinary(Binary img, int width, int height)
        //{
        //    Bitmap b = new Bitmap(width, height);
        //    using (Graphics g = Graphics.FromBinary((Binary)b))
        //    {
        //        g.DrawBinary(img, 0, 0, width, height);
        //    }

        //    return (Binary)b;
        //}

        //private Binary ToBinary(Byte[] bytes)
        //{
        //    Binary Binary = null;
        //    using (var mem = new MemoryStream(bytes))
        //        Binary = Binary.FromStream(mem);
        //    return Binary;
        //}
    }
}
