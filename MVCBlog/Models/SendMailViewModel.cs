using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCBlog.Models
{
    public class SendMailViewModel
    {
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Content { get; set; }
    }
}