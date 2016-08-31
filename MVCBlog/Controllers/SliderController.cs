using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCBlog.Models;

namespace MVCBlog.Controllers
{
    public class SliderController : Controller
    {
        // GET: Slider
        public ActionResult Index()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return View(db.gallery.ToList());
            }
            //return View();
        }

        public ActionResult AddImage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddImage(HttpPostedFileBase ImagePath)
        {
            if (ImagePath != null)
            {
                System.Drawing.Image img = System.Drawing.Image.FromStream(ImagePath.InputStream);
                if ((img.Width != 800) || (img.Height != 356))
                {
                    ModelState.AddModelError("", "Image resolution must be 800 x 356 pixels");
                    return View();
                }

                string pic = System.IO.Path.GetFileName(ImagePath.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Content/images"), pic);
                ImagePath.SaveAs(path);
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    Gallery gallery = new Gallery { ImagePath = "~/Content/images/" + pic };
                    db.gallery.Add(gallery);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        
        public ActionResult DeleteImages()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return View(db.gallery.ToList());
            }
        }

        [HttpPost]
        public ActionResult DeleteImages(IEnumerable<int> ImagesIDs)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                foreach (var id in ImagesIDs)
                {
                    var image = db.gallery.Single(s => s.ID == id);
                    string imgPath = Server.MapPath(image.ImagePath);
                    db.gallery.Remove(image);
                    if (System.IO.File.Exists(imgPath))
                        System.IO.File.Delete(imgPath);


                }
                db.SaveChanges();
            }
            return RedirectToAction("DeleteImages");
        }
    }
}