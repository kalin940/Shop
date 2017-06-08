using MVCBlog.Data;
using MVCBlog.Models;
using MVCBlog.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVCBlog.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Products()
        {
            var products = new List<ProductsViewModel>();
            using (var db = new ApplicationDbContext())
            {
                products = ProductsRepo.ProductsOrderedByDate();

            }

            return View(products);
        }
        public ActionResult PromoProducts()
        {
            var products = ProductsRepo.PromoProducts();

            return View(products);
        }
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = ProductsRepo.ProductDetail(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }
        [HttpPost]
        [ActionName("Search")]
        public JsonResult Search(string keyword)
        {
            var results = ProductsRepo.SearchProducts(keyword);
            Session["Results"] = results;
            
            return Json("true");
        }

        public ActionResult SearchProducts()
        {
            var model = (List<ProductsViewModel>)Session["Results"];
            return View(model);
        }
    }
}