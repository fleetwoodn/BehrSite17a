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
    public class BlogBController : Controller
    {
        private MainContext db = new MainContext();

        // GET: BlogB
        public ActionResult Index()
        {
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

        //
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
