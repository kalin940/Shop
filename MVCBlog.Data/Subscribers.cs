using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace MVCBlog.Data
{
    public partial class Subscribers
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string EmailAddress { get; set; }
    }
}
