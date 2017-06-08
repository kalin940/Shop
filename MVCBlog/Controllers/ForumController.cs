using MVCBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MVCBlog.Data;
namespace MVCBlog.Controllers
{
    public class ForumController : Controller
    {
        public ActionResult Index()
        {
            var db = new ApplicationDbContext();
            var post = db.Posts.Include(p => p.Author).OrderByDescending(p => p.Date).Take(4).Select(
                    p => new PostViewModel()
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Body = p.Body,
                        Date = p.Date,
                        Author = p.Author.FullName,
                        Img = p.Img,
                        Tags = p.Tag

             });  
            var postWithMostComments = db.Posts.Include(p => p.Author).OrderByDescending(p => p.Comments.Count).Take(4).Select(
                p => new PostViewModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    Body = p.Body,
                    Date = p.Date,
                    Author = p.Author.FullName,
                    Img = p.Img,
                    Tags = p.Tag
                });
            var showPosts = new ShowPostsViewModel()
            {
                Posts = post,
                PostWithMostComments= postWithMostComments
            };
            return View(showPosts);
            
        }
    }
}