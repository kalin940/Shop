
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCBlog.Data;
using MVCBlog.Extensions;
using Newtonsoft.Json;
using MVCBlog.Models;

namespace MVCBlog.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ActionResult Index()
        {
            var postWithAuthors = db.Posts.Include(p => p.Author).OrderByDescending(p => p.Date).ToList();
            return View(postWithAuthors);
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            //COMMENTS
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            var postComments = db.Comments.Include(u=>u.Author).Where(u => u.PostID == post.Id).OrderByDescending(u=>u.Date).ToList();                          
            ViewBag.PostComments = postComments;
            ViewBag.CurrPostTags = post.Tag.ToArray();
            return View(post);
        }

        // GET: Posts/Create
        [Authorize]
        public ActionResult Create()
        {
            var tags = db.Tags.ToList();
            ViewBag.Tags = tags;
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Body")] Post post, HttpPostedFileBase upload,string tags)
        {       
            //ViewBag.Tags = db.Tags.ToList();
            if (!string.IsNullOrEmpty(tags))
            {
                var currTags = tags.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(s => s.Trim()).Select(int.Parse).ToArray();
                List<Tag> addTags = new List<Tag>();
                foreach (var item in currTags)
                {
                    var found = db.Tags.FirstOrDefault(p => p.Id == item);
                    if (found!=null)
                    {
                        addTags.Add(found);
                    }
                    post.Tag = addTags;
                }
                
            }
            var postTags = tags.Split(';').ToArray();
            if (ModelState.IsValid)
            {              
                post.Author = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                if (upload != null && upload.ContentLength > 0)
                {

                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        post.Img = reader.ReadBytes(upload.ContentLength);
                    }
                }
                db.Posts.Add(post);
                db.SaveChanges();
                this.AddNotification("Post created", NotificationType.INFO);
                return RedirectToAction("Index");
            }
            else
            {
                this.AddNotification("Failed to create post", NotificationType.ERROR);
            }
            return View(post);
        }

        [HttpPost]
        [ActionName("SearchPosts")]
        public JsonResult SearchPosts(string keyword, string tag)
        {
            var searchResults = new List<PostViewModel>();
            if (string.IsNullOrEmpty(tag))
            {
                if (string.IsNullOrEmpty(keyword))
                {
                    searchResults = db.Posts.Select(p => new PostViewModel { Id = p.Id, Author = p.Author.FullName, Body = p.Body, DateString = p.Date.ToString(), Title = p.Title }).ToList();
                    return Json(searchResults);
                }
                searchResults = db.Posts.Where(p => p.Title.Contains(keyword)).Select(p => new PostViewModel { Id = p.Id, Author = p.Author.FullName, Body = p.Body, DateString = p.Date.ToString(), Title = p.Title }).ToList();
            }
            else
            {
                if (string.IsNullOrEmpty(keyword))
                {
                    var postsIds = db.Tags.FirstOrDefault(p => p.TagName == tag).Posts.Select(p => p.Id).ToList();
                    searchResults = db.Posts.Where(p => postsIds.Contains(p.Id)).Select(p => new PostViewModel { Id = p.Id, Author = p.Author.FullName, Body = p.Body, DateString = p.Date.ToString(), Title = p.Title }).ToList();
                    return Json(searchResults);
                }
                var postsIdsKeyword = db.Tags.FirstOrDefault(p => p.TagName == tag).Posts.Where(p=>p.Title.Contains(keyword)).Select(p => p.Id).ToList();
                searchResults = db.Posts.Where(p => postsIdsKeyword.Contains(p.Id)).Select(p => new PostViewModel { Id = p.Id, Author = p.Author.FullName, Body = p.Body, DateString = p.Date.ToString(), Title = p.Title }).ToList();
            }
            return Json(searchResults);
        }

        [HttpGet]
        [ActionName("GetTags")]
        public JsonResult GetTags()
        {
            var tags = db.Tags.Select(p => p.TagName).ToList();
            return Json(tags, JsonRequestBehavior.AllowGet);
        }

        // GET: Posts/Edit/5
        [Authorize(Roles = "Administrators")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);      
            if (post == null)
            {
                return HttpNotFound();
            }
            var authors = db.Users.ToList();
            ViewBag.Authors = authors;
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Body,Date,Author_Id")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize(Roles = "Administrators")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [Authorize(Roles = "Administrators")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
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
