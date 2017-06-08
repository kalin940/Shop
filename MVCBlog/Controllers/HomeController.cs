using MVCBlog.Models;
using System;
using System.Web.Mvc;
using System.Net.Mime;
using MVCBlog.Services;
using MVCBlog.Repository;
using MVCBlog.Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;
using MVCBlog.Data;
using Microsoft.AspNet.Identity;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;

namespace MVCBlog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           
            var model = ShopRepo.HomePageInfo();
            return View(model);
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contacts()
        {
            return View();
        }
        public ActionResult Services()
        {
            return View();
        }
        public ActionResult Cart()
        {
            return View();
        }

        public ActionResult NewsLetterSubscribe(NewsLetterViewModel letter)
        {
            //Save MailAddress in DB
            var saveSub = MailRepo.SaveNewSubscriber(letter.EmailAddress);
            //Send Mail
            if (saveSub)
            {
                MailServices.sendWelcomeMail(letter.EmailAddress);
                this.AddNotification("Successful subscription", NotificationType.INFO);
                return Redirect(Request.UrlReferrer.ToString());
            }
            this.AddNotification("Email is already used!", NotificationType.ERROR);
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult ContactUs(ContactUsViewModel contact)
        {
            MailServices.ContactUsMail(contact);
            this.AddNotification("Successfuly send", NotificationType.INFO);
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        [ActionName("PostOrder")]
        public JsonResult PostOrder(string keyword)
        {
            var orderClient = JsonConvert.DeserializeObject<List<ProductDTO>>(keyword);
            decimal totalCost = 0;
            List<OrderedProducts> products = new List<OrderedProducts>();
            var userId = User.Identity.GetUserId();
            
            foreach (var item in orderClient)
            {
                totalCost += item.price;
                OrderedProducts ord = new OrderedProducts()
                {
                     ProductId=item.id,
                      Quantity=item.quantity,                      
                };
                products.Add(ord);
            }
          
            using (var db=new ApplicationDbContext())
            {
                var test = User.Identity.GetUserId();
                Orders order = new Orders()
                {
                    DateOrdered = DateTime.Now,
                    TotalCost = totalCost,
                    UserId = userId,
                    OrderedProducts = products,
                    OrderStatusId = 1,
                };
                db.Orders.Add(order);
                db.OrderedProducts.AddRange(products);
                db.SaveChanges();
            }

            return Json("true");
        }


    }
}