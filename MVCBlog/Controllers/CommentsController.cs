using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCBlog.Data;

namespace MVCBlog.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Author).Include(c => c.Post).OrderByDescending(c => c.Date);
            return View(comments.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        [Authorize]
        public ActionResult Create(int id) //Add id to be passed to Create View
        {
            ViewBag.Author_Id = new SelectList(db.Users, "Id", "FullName");
            ViewBag.PostID = new SelectList(db.Posts, "Id", "Title");
            var PostIdTest = db.Posts.FirstOrDefault(p => p.Id == id);
            var userFullName = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            ViewBag.User = userFullName.FullName;
            ViewBag.PostIdDemo = PostIdTest.Id;
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Text,PostID")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.Author = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);             
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Author_Id = new SelectList(db.Users, "Id", "FullName", comment.Author_Id);
            ViewBag.PostID = new SelectList(db.Posts, "Id", "Title", comment.PostID);
            return View(comment);
        }

        // GET: Comments/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.Author_Id = new SelectList(db.Users, "Id", "FullName", comment.Author_Id);
            ViewBag.PostID = new SelectList(db.Posts, "Id", "Title", comment.PostID);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Text,PostID,Author_Id,Date")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Author_Id = new SelectList(db.Users, "Id", "FullName", comment.Author_Id);
            ViewBag.PostID = new SelectList(db.Posts, "Id", "Title", comment.PostID);
            return View(comment);
        }

        // GET: Comments/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
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
