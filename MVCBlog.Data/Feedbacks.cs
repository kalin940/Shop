using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace MVCBlog.Data
{
    public partial class Feedbacks
    {
        public int Id { get; set; }

        [Required]
        public string Feedback { get; set; }

        public int ProductId { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AspNetUsers AspNetUsers { get; set; }

        [ForeignKey("ProductId")]
        public virtual Products Products { get; set; }
    }
}
