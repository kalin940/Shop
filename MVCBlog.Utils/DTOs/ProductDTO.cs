using System;
using System.Collections.Generic;
using System.Text;

namespace MVCBlog.Utils.DTOs
{
    public class ProductDTO
    {
        public int id { get; set; }
        public string product { get; set; }
        public decimal price { get; set; }
        public string status { get; set; }
        public string brand { get; set; }
        public int quantity { get; set; }

    }
}
