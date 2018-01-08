using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.Entity;
using System.Net;
using System.Net.Mail;
using BehrSite17.Models;



namespace BehrSite17.Controllers
{
    public class TravelController : Controller
    {
        private MainContext db = new MainContext();

        // GET: Travel blah
        public ActionResult Index()
        {
            return View();
        }

        // GET: Flight
        public ActionResult Flight(string siid = "129673")
        {
            ViewBag.Message = "Flight Search";

            //siid is dynamic and passed thru url
            TempData["siid"] = siid;

            //get all the specials for display

            var spec = (from i in db.Specials where i.SiteLocation == "Air" select i).ToList();


            return View();
        }

        //GET: Hotel
        public ActionResult Hotel()
        {
            return View();
        }

        //nothing
        public ActionResult test()
        {
            return View();
        }

        //nothing2
        public ActionResult testa()
        {
            return View();
        }
    }
}