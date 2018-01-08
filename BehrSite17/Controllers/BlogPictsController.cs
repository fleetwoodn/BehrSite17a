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
    public class BlogPictsController : Controller
    {
        private MainContext db = new MainContext();

        // GET: BlogPicts
        public ActionResult Index()
        {
            return View(db.BlogPicts.ToList());
        }

        // GET: BlogPicts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPicts blogPicts = db.BlogPicts.Find(id);
            if (blogPicts == null)
            {
                return HttpNotFound();
            }
            return View(blogPicts);
        }

        // GET: BlogPicts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogPicts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PostFK,PicTitle,EditDate,PictPict")] BlogPicts blogPicts)
        {
            var file = Request.Files[0];
            var sessUnq = Session.SessionID.Substring(Session.SessionID.Length - 4);
            if (file != null && file.ContentLength > 0)
            {
                var fileName = sessUnq + Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Images/BlogImages/"), fileName);
                file.SaveAs(path);
                blogPicts.PictPict = fileName;
            }

            if (ModelState.IsValid)
            {
                db.BlogPicts.Add(blogPicts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blogPicts);
        }

        // GET: BlogPicts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPicts blogPicts = db.BlogPicts.Find(id);
            if (blogPicts == null)
            {
                return HttpNotFound();
            }
            return View(blogPicts);
        }

        // POST: BlogPicts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PostFK,PicTitle,EditDate,PictPict")] BlogPicts blogPicts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogPicts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogPicts);
        }

        // GET: BlogPicts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPicts blogPicts = db.BlogPicts.Find(id);
            if (blogPicts == null)
            {
                return HttpNotFound();
            }
            return View(blogPicts);
        }

        // POST: BlogPicts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPicts blogPicts = db.BlogPicts.Find(id);
            db.BlogPicts.Remove(blogPicts);
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
