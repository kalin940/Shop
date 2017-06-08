using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace MVCBlog.Data
{
    public partial class Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            OrderedProducts = new HashSet<OrderedProducts>();
        }
        [Key]
        public int Id { get; set; }

        public int OrderStatusId { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOrdered { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateRecieved { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TotalCost { get; set; }

        [ForeignKey("UserId")]
        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual ICollection<OrderedProducts> OrderedProducts { get; set; }
        [ForeignKey("OrderStatusId")]
        public virtual OrderStatus OrderStatus { get; set; }
    }
}
