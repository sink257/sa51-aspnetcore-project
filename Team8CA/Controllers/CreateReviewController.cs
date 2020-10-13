using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Team8CA.DataAccess;
using Team8CA.Models;
//using Team8CA.Services;

namespace Team8CA.Controllers
{
    public class CreateReviewController : Controller
    {
        protected AppDbContext db;
        public CreateReviewController(AppDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index(int id)
        {  
            Products product = db.Products.First(p => p.ProductId == id);

            ViewData["firstname"] = Request.Cookies["firstname"];
            string sessionid = Request.Cookies["sessionId"];
            ViewData["sessionId"] = sessionid;
            string customerId = Request.Cookies["customerId"];
            ViewData["customerid"] = customerId;
            List<ShoppingCartItem> shoppingcart = db.ShoppingCartItem.Where(x => x.ShoppingCartId == customerId).ToList();
            List<ShoppingCartItem> shoppingcartNull = db.ShoppingCartItem.Where(x => x.ShoppingCartId == "0").ToList();
            if (sessionid != null)
            {
                ViewData["cartcount"] = shoppingcart.Count;
            }
            else
            {
                ViewData["cartcount"] = shoppingcartNull.Count;
            }
            ViewData["product"] = product;

            return View();
        }

        [HttpPost]
        public IActionResult Create(int rating, string details, int productId, int?orderId)
        {
            if (!ModelState.IsValid)
                return View();
            Models.Review review = new Models.Review(productId, Request.Cookies["firstname"], rating, details, DateTime.Now);
            string customerId = Request.Cookies["customerId"];
            review.CustomerId = customerId;
            Products p = db.Products.FirstOrDefault(p => p.ProductId == productId);
            if (orderId == null)
            {
                Models.Order OrderwithProduct = db.Order
                    .FirstOrDefault(o => o.CustomerId == customerId && o.OrderDetails.Any(d => d.ProductId == p.ProductId && d.reviewed != true) == true);
                review.OrderId = OrderwithProduct.OrderId;

            }
            else
            {
                review.OrderId = (int)orderId;
            }

            db.OrderDetails.FirstOrDefault(o => o.OrderId == review.OrderId && o.ProductId == productId).reviewed = true;
            db.Reviews.Add(review);
            db.SaveChanges();

            return RedirectToAction("ProductDetailPage","Gallery", new { id = productId });
        }


    }
}
