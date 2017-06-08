using Microsoft.Ajax.Utilities;
using MVCBlog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MVCBlog.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public string DateString { get; set; }
        public string Author { get; set; }
        public byte[] Img { get; set; }
        public IEnumerable<Tag> Tags { get; set; }

    }
    public class ShowPostsViewModel
    {
        public IEnumerable<PostViewModel> Posts { get; set; }
        public IEnumerable<PostViewModel> PostWithMostComments { get; set; }
    }

    public class PostDetailsViewModel
    {
        public int Id { get; set; }
       //public string Title { get; set; }
       //public string Body { get; set; }
       //public DateTime Date { get; set; }
       //public string Author { get; set; }
        public string AuthorId { get; set; }
        public IEnumerable<CommentViewModel> Comments { get; set; }
        public static Expression<Func<Post, PostDetailsViewModel>> ViewModel
        {
            get
            {
                return e => new PostDetailsViewModel()
                {
                    Id = e.Id,
                    //Title = e.Title,
                    //Body = e.Body,
                    //Date = e.Date,
                    //Author = e.Author.FullName,
                    AuthorId = e.Author_Id,
                    Comments = e.Comments.AsQueryable().Select(CommentViewModel.ViewModel)
                };
            }
        }
    }
}