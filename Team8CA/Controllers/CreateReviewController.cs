using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Create(int rating, string details, int productId)
        {
            if (!ModelState.IsValid)
                return View();
            Review review = new Review(productId, Request.Cookies["username"], rating, details, DateTime.Now);

            db.Reviews.Add(review);
            db.SaveChanges();

            return RedirectToAction("ProductDetailPage","Gallery", new { id = productId });
        }


    }
}
