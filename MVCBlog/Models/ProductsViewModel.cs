using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCBlog.Models
{
    public class ProductsViewModel
    {
        public int Id { get; set; }
        public List<string> Imgs { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
        public decimal? Price { get; set; }
        public string Status { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public string Specification { get; set; }
        public decimal? Rating { get; set; }
        public List<FeedbackViewModel> Feedback { get; set; }
        public string FirstImg {
            get {
                if (Imgs.Count==0)
                {
                    return "../../Content/images/product-2.jpg";
                }
                return Imgs[0];
            }
            set {
                FirstImg = value;
            }
        }
    }
}