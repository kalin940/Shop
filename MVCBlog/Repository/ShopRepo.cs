using MVCBlog.Data;
using MVCBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCBlog.Repository
{
    public class ShopRepo
    {

        public static HomePageViewModel HomePageInfo()
        {
            var model = new HomePageViewModel();
            using (var db = new ApplicationDbContext())
            {
                var promoProducts = db.Products.Where(p => p.ProductStatus == 2).Select(p => new ProductsViewModel
                {
                    Id = p.Id,
                    Brand = p.Producers.Name,
                    Imgs = p.ProductImgs.Select(w => w.Img).Take(1).ToList(),
                    Price = p.Price,
                    ProductName = p.Name,
                    Status = p.ProductStatus1.Status
                }).Take(4).ToList();
                model.PromoProducts = promoProducts;

                var newProducts = db.Products.OrderByDescending(p => p.DateAdded).Select(p => new ProductsViewModel
                {
                    Id = p.Id,
                    Brand = p.Producers.Name,
                    Imgs = p.ProductImgs.Select(w => w.Img).Take(1).ToList(),
                    Price = p.Price,
                    ProductName = p.Name,
                    Status = p.ProductStatus1.Status
                }).Take(6).ToList();

                model.NewProducts = newProducts;

                model.ClientsCount = db.Users.Count();

                model.ProductsCount = db.Products.Count();

                return model;
            }
        }
    }
}