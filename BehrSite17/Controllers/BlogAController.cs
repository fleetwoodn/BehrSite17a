using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BehrSite17.Models;
using BehrSite17.ViewModels;
using System.IO;

namespace BehrSite17.Controllers
{
    public class BlogAController : Controller
    {
        private MainContext db = new MainContext();

        // GET: Posts
        public ActionResult Index(string searchString)
        {
            var posts = from q in db.BlogPosts
                        select q;

            if (!String.IsNullOrEmpty(searchString))
            {
                posts = posts.Where(r => r.PostTitle.Contains(searchString)
                    || r.PostTags.Contains(searchString)
                    );

            }

            return View(db.BlogPosts.ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Posts posts = db.Posts.Find(id);
            //if (posts == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(posts);

            BlogPosts posts = db.BlogPosts.Find(id);

            var qPict = db.BlogPicts.Where(q => q.PostFK == id).ToList();

            var viewModel = new PostDetailViewModel
            {
                ID = posts.ID,
                PostTitle = posts.PostTitle,
                PostAuthor = posts.PostAuthor,
                PostTags = posts.PostTags,
                PostText = posts.PostText,
                TitlePic = posts.TitlePic,
                EditDate = posts.EditDate,

                BlogPicts = qPict,

            };

            return View(viewModel);

        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PostTitle,PostAuthor,PostTags,PostText,TitlePic,EditDate")] BlogPosts posts)
        {

            if (ModelState.IsValid)
            {
                String aDate = DateTime.UtcNow.ToString("yyMMddHHmmss");
                posts.EditDate = aDate;

                db.BlogPosts.Add(posts);
                db.SaveChanges();

                //grab id for new record

                //Posts posts = db.Posts.

                //int aId = int.Parse(db.Posts
                //    .OrderByDescending(b => b.ID)
                //    .Select(c => c.ID)
                //    .First()
                //    .ToString());

                int aId = posts.ID;

                //return RedirectToAction("Index");
                return RedirectToAction("Details", new { id = aId });
            }

            //nn to drive to edit action...fuck fuck fuck --- done done done motherfucker

            return View(posts);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPosts posts = db.BlogPosts.Find(id);
            if (posts == null)
            {
                return HttpNotFound();
            }

            ViewBag.aId = id;

            return View(posts);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PostTitle,PostAuthor,PostTags,PostText,TitlePic,EditDate")] BlogPosts posts)
        {
            if (ModelState.IsValid)
            {

                String aDate = DateTime.UtcNow.ToString("yyMMddHHmmss");
                posts.EditDate = aDate;

                int aId = posts.ID;

                db.Entry(posts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = aId });
            }
            return View(posts);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPosts posts = db.BlogPosts.Find(id);
            if (posts == null)
            {
                return HttpNotFound();
            }
            return View(posts);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPosts posts = db.BlogPosts.Find(id);
            db.BlogPosts.Remove(posts);

            //BlogPicts picts = db.BlogPicts.Where(j => j.PostFK == id);
            //db.BlogPicts.Remove(picts);
            //string fullPath = Request.MapPath
            //db.BlogPicts.RemoveRange(db.BlogPicts.Where(j => j.PostFK == id));

            //mark blogpicts as deletable for later housekeeping
            foreach (var delpicts in db.BlogPicts.Where(j => j.PostFK == id))
            {
                delpicts.EditDate = "deleted";
                db.Entry(delpicts).State = EntityState.Modified;
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //***** picts area

        // GET: Picts
        public ActionResult PictIndex(string searchString)
        {
            var picts = from q in db.BlogPicts
                        select q;

            if (!String.IsNullOrEmpty(searchString))
            {
                picts = picts.Where(r => r.PostFK.Equals(searchString)
                    || r.EditDate.Contains(searchString)
                );
            }

            return View(db.BlogPicts.ToList());
        }

        //create [add] picture

        // GET: Picts/Create
        public ActionResult PictCreate(int? fkid)
        {
            if (fkid == null)
            {
                ViewBag.fkid = "no post attached, go back!!!";
            }

            ViewBag.fkid = fkid;

            return View();
        }

        // POST: Picts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PictCreate([Bind(Include = "ID,PostFK,PicTitle,EditDate,PictPict")] BlogPicts picts)
        {

            var file = Request.Files[0];
            var sessUnq = Session.SessionID.Substring(Session.SessionID.Length - 4);
            if (file != null && file.ContentLength > 0)
            {
                var fileName = sessUnq + Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Images/BlogImages"), fileName);
                file.SaveAs(path);
                picts.PictPict = fileName;
            }

            if (ModelState.IsValid)
            {
                //picts.PostFK = ViewBag.aId;

                String aDate = DateTime.UtcNow.ToString("yyMMddHHmmss");
                picts.EditDate = aDate;

                db.BlogPicts.Add(picts);
                db.SaveChanges();

                int fkid = picts.PostFK;

                return RedirectToAction("Details", new { id = fkid });
            }

            return View(picts);
        }

        // GET: Picts/Delete/5
        public ActionResult PictDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPicts picts = db.BlogPicts.Find(id);
            if (picts == null)
            {
                return HttpNotFound();
            }
            return View(picts);
        }

        // POST: Picts/Delete/5
        [HttpPost, ActionName("PictDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult PictDeleteConfirmed(int id)
        {
            BlogPicts picts = db.BlogPicts.Find(id);

            int fkid = picts.ID;

            string fullPath = Request.MapPath("~/Content/Images/BlogImages/" + picts.PictPict);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            db.BlogPicts.Remove(picts);
            db.SaveChanges();


            return RedirectToAction("Index", new { id = fkid });
            //return RedirectToAction("Index");
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