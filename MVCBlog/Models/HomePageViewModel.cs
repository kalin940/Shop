using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCBlog.Models
{
    public class HomePageViewModel
    {
        public List<ProductsViewModel> PromoProducts { get; set; }
        public List<ProductsViewModel> NewProducts { get; set; }
        public int ClientsCount { get; set; }
        public int ProductsCount { get; set; }
    }
}