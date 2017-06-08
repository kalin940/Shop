using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCBlog.Data;
using MVCBlog.Models;

namespace MVCBlog.Repository
{
    public class ProductsRepo
    {

        public static List<ProductsViewModel> ProductsOrderedByDate()
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var products = db.Products.OrderByDescending(p => p.DateAdded).Select(p => new ProductsViewModel
                    {
                        Id = p.Id,
                        Brand = p.Producers.Name,
                        Imgs = p.ProductImgs.Select(w => w.Img).Take(1).ToList(),
                        Price = p.Price,
                        ProductName = p.Name,
                        Status = p.ProductStatus1.Status
                    }).ToList();
                    return products;
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public static List<ProductsViewModel> PromoProducts()
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var products = db.Products.Where(p => p.ProductStatus == 2).OrderByDescending(p => p.DateAdded).Select(p => new ProductsViewModel
                    {
                        Id = p.Id,
                        Brand = p.Producers.Name,
                        Imgs = p.ProductImgs.Select(w => w.Img).Take(1).ToList(),
                        Price = p.Price,
                        ProductName = p.Name,
                        Status = p.ProductStatus1.Status
                    }).ToList();
                    return products;

                }
            }
            catch (Exception)
            {

                throw;
            }


        }

        public static ProductsViewModel ProductDetail(int? id)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    Products product = db.Products.FirstOrDefault(p => p.Id == id);

                    if (product == null)
                    {
                        return null;
                    }
                    var model = new ProductsViewModel
                    {
                        Id = product.Id,
                        Brand = product.Producers.Name,
                        Imgs = product.ProductImgs.Select(w => w.Img).ToList(),
                        Price = product.Price,
                        ProductName = product.Name,
                        Status = product.ProductStatus1.Status,
                        Description = product.Description,
                        Specification = product.Specification,
                        Feedback = product.Feedbacks.Select(p => new FeedbackViewModel { Author = p.AspNetUsers.FullName ?? p.AspNetUsers.UserName, Info = p.Feedback }).ToList()
                    };
                    return model;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<ProductsViewModel> SearchProducts(string keyword)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var products = db.Products.Where(p => p.Name.Contains(keyword)).OrderByDescending(p => p.DateAdded).Select(p => new ProductsViewModel
                    {
                        Id = p.Id,
                        Brand = p.Producers.Name,
                        Imgs = p.ProductImgs.Select(w => w.Img).Take(1).ToList(),
                        Price = p.Price,
                        ProductName = p.Name,
                        Status = p.ProductStatus1.Status
                    }).ToList();
                    return products;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}