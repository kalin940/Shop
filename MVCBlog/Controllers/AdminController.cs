using MVCBlog.Data;
using MVCBlog.Extensions;
using MVCBlog.Models;
using MVCBlog.Repository;
using MVCBlog.Services;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MVCBlog.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SendMailToSubscribers(SendMailViewModel letter)
        {
            var emailsList = MailRepo.GetAllSubscribersEmails();
            MailServices.sendMailToSubscribers(emailsList, letter.Subject, letter.Content);
            this.AddNotification("Successfuly send", NotificationType.INFO);
            return Redirect(Request.UrlReferrer.ToString());
        }
        
        public ActionResult CreateProduct(ProductsViewModel product)
        {
            Products productItem = new Products()
            {
                 Name=product.ProductName,
                  Price=product.Price,
                   Count=product.Count,
                    Description= product.Description,
                     Specification= product.Specification,
                       Rating= product.Rating,
                        DateAdded = DateTime.Now
            };
            using (var db = new ApplicationDbContext())
            {
                var producerCheck = db.Producers.FirstOrDefault(p => p.Name.Equals(product.Brand, StringComparison.InvariantCultureIgnoreCase));
                if (producerCheck!=null)
                {
                    productItem.Producer = producerCheck.Id;
                }
                var statusCheck = db.ProductStatus.FirstOrDefault(p => p.Status.Equals(product.Status, StringComparison.InvariantCultureIgnoreCase));
                if (statusCheck != null)
                {
                    productItem.ProductStatus = statusCheck.Id;
                }
                else
                {
                    productItem.ProductStatus = 1;
                }
                db.Products.Add(productItem);
                db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}