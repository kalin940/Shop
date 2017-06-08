using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCBlog.Models
{
    public class NewsLetterViewModel
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
    
    }
}