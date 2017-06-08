using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace MVCBlog.Data
{
    public partial class Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Products()
        {
            Feedbacks = new HashSet<Feedbacks>();
            OrderedProducts = new HashSet<OrderedProducts>();
            ProductImgs = new HashSet<ProductImgs>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public int ProductStatus { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Rating { get; set; }

        public int? Producer { get; set; }

        public string Description { get; set; }

        public string Specification { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateAdded { get; set; }

        public int? Count { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Price { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedbacks> Feedbacks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderedProducts> OrderedProducts { get; set; }

        [ForeignKey("Producer")]
        public virtual Producers Producers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductImgs> ProductImgs { get; set; }

        [ForeignKey("ProductStatus")]
        public virtual ProductStatus ProductStatus1 { get; set; }
    }
}
