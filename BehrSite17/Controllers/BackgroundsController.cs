using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BehrSite17.Models;
using System.IO;

namespace BehrSite17.Controllers
{
    public class BackgroundsController : Controller
    {
        private MainContext db = new MainContext();

        // GET: Backgrounds
        public ActionResult Index()
        {
            return View(db.Backgrounds.ToList());
        }

        // GET: Backgrounds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Backgrounds backgrounds = db.Backgrounds.Find(id);
            if (backgrounds == null)
            {
                return HttpNotFound();
            }
            return View(backgrounds);
        }

        // GET: Backgrounds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Backgrounds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SiteLocation,BackImage")] Backgrounds backgrounds)
        {

            var file = Request.Files[0];
            var sessUnq = Session.SessionID.Substring(Session.SessionID.Length - 4);
            if (file != null && file.ContentLength > 0)
            {
                var fileName = sessUnq + Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Images/BackImages"), fileName);
                file.SaveAs(path);
                backgrounds.BackImage = fileName;
            }

            if (ModelState.IsValid)
            {
                db.Backgrounds.Add(backgrounds);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(backgrounds);
        }

        // GET: Backgrounds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Backgrounds backgrounds = db.Backgrounds.Find(id);
            if (backgrounds == null)
            {
                return HttpNotFound();
            }
            return View(backgrounds);
        }

        // POST: Backgrounds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SiteLocation,BackImage")] Backgrounds backgrounds)
        {
            if (ModelState.IsValid)
            {
                db.Entry(backgrounds).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(backgrounds);
        }

        // GET: Backgrounds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Backgrounds backgrounds = db.Backgrounds.Find(id);
            if (backgrounds == null)
            {
                return HttpNotFound();
            }
            return View(backgrounds);
        }

        // POST: Backgrounds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Backgrounds backgrounds = db.Backgrounds.Find(id);
            db.Backgrounds.Remove(backgrounds);
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
