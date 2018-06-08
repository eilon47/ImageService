using ImageServiceWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class ImageWebController : Controller
    {
        private static ImageWebModel model = new ImageWebModel();
        // GET: ImageWeb
        public ActionResult ImageWeb()
        {
            ViewBag.PhotosCounter = model.PhotosCounter;
            ViewBag.Status = model.Status;
            ViewBag.Students = model.Students;
            return View(model);
        }
    }
}