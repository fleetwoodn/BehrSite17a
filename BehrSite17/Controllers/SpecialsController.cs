using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BehrSite17.Models;

namespace BehrSite17.Controllers
{
    public class SpecialsController : Controller
    {
        private MainContext db = new MainContext();

        // GET: Specials
        public ActionResult Index()
        {
            return View(db.Specials.ToList());
        }

        // GET: Specials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specials specials = db.Specials.Find(id);
            if (specials == null)
            {
                return HttpNotFound();
            }
            return View(specials);
        }

        // GET: Specials/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Specials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Section,Title,SiteLocation,Content,ButtonTitle,LinkUrl,SpecialImage")] Specials specials)
        {
            if (ModelState.IsValid)
            {
                db.Specials.Add(specials);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(specials);
        }

        // GET: Specials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specials specials = db.Specials.Find(id);
            if (specials == null)
            {
                return HttpNotFound();
            }
            return View(specials);
        }

        // POST: Specials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Section,Title,SiteLocation,Content,ButtonTitle,LinkUrl,SpecialImage")] Specials specials)
        {
            if (ModelState.IsValid)
            {
                db.Entry(specials).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(specials);
        }

        // GET: Specials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specials specials = db.Specials.Find(id);
            if (specials == null)
            {
                return HttpNotFound();
            }
            return View(specials);
        }

        // POST: Specials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Specials specials = db.Specials.Find(id);
            db.Specials.Remove(specials);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
