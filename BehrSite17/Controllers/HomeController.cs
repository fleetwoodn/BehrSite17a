using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using BehrSite17.Models;
using BehrSite17.ViewModels;
using System.IO;

namespace BehrSite17.Controllers
{
    public class HomeController : Controller
    {
        private MainContext db = new MainContext();

        public ActionResult Index()
        {
            var iBackImage = (from j in db.Backgrounds where j.SiteLocation == "Air" orderby Guid.NewGuid() select j.BackImage).Take(1).SingleOrDefault();
            TempData["BackImage"] = iBackImage;
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}