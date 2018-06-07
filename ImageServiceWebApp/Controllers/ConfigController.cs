using Communication.Infrastructure;
using ImageServiceWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWebApp.Controllers
{
    public class ConfigController : Controller
    {
        private static ConfigModel model = new ConfigModel();
        private static string deleteHandler;
        
        public ConfigController()
        {
            model.NotifyRefresh -= Refresh;
            model.NotifyRefresh += Refresh;
        }
        public void Refresh()
        {
            Config();
        }
        // GET: Config
        public ActionResult Config()
        {
            return View(model);
        }
        // GET: Config
        public ActionResult Confim()
        {
            return View(model);
        }
        public ActionResult ClickedOnHandler(string clickedHandler)
        {
            deleteHandler = clickedHandler;
            return RedirectToAction("ConfirmView");
        }
        public ActionResult ApprovedDelete()
        {
            model.SendCommandToService(new CommandRecievedEventArgs((int)CommandEnum.CloseCommand, new string[] { deleteHandler }, null));
            return RedirectToAction("ConfigView");
        }
        public ActionResult CanceledDelete()
        {
            return RedirectToAction("ConfigView");
        }
    }
}