using System;
using System.Collections.Generic;
using System.Text;

namespace MVCBlog.Utils.DTOs
{
    public class Posts
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public  ICollection<Tags> Tags { get; set; }
    }
}
