using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
//using X.PagedList.Mvc;
using X.PagedList.Mvc.Core;
using Team8CA.DataAccess;
using Team8CA.Models;
//using Team8CA.Services;

namespace Team8CA.Controllers
{
    public class GalleryController : Controller
    {
        protected AppDbContext db;

        private readonly ShoppingCart _shoppingcart;

        public IActionResult Index(int? page)                                          
        {                                                                      
            List<Products> product = db.Products.ToList();          
            ViewData["product"] = product;

            var pageNumber = page ?? 1; 
            var onePageOfProducts = product.ToPagedList(pageNumber, 6);
            ViewData["OnePageOfProducts"] = onePageOfProducts;

            ViewData["username"] = Request.Cookies["username"];
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

            return View();
        }

        public IActionResult AntivirusAndSecurity()
        {
            List<Products> product = db.Products.Where(p => (p.ProductCategory == "AntivirusandSecurity")).ToList();
            ViewData["product"] = product;

            ViewData["firstname"] = Request.Cookies["firstname"];
            ViewData["sessionId"] = Request.Cookies["sessionId"];
            return View();
        }

        public IActionResult BusinessAndOffice()
        {
            List<Products> product = db.Products.Where(p => (p.ProductCategory == "BusinessAndOffice")).ToList();
            ViewData["product"] = product;

            ViewData["firstname"] = Request.Cookies["firstname"];
            ViewData["sessionId"] = Request.Cookies["sessionId"];
            return View();
        }

        public IActionResult DesignAndIllustration()
        {
            List<Products> product = db.Products.Where(p => (p.ProductCategory == "DesignAndIllustration")).ToList();
            ViewData["product"] = product;

            ViewData["firstname"] = Request.Cookies["firstname"];
            ViewData["sessionId"] = Request.Cookies["sessionId"];
            return View();
        }

        public GalleryController(AppDbContext db)
        {
            this.db = db;
        }
        public List<Products> GetProducts(string keyword)
        {
            List<Products> products = db.Products.ToList();
            {
                if (keyword == "" || keyword == null)
                {
                    return db.Products.ToList();
                }

                return db.Products.Where(p =>
                        p.ProductName.ToLower().Contains(keyword.ToLower()) ||
                        p.ProductDescription.ToLower().Contains(keyword.ToLower()))
                    .ToList();
            }
        }

        [HttpPost]
        public IActionResult Search(string keyword = "")
        {
            List<Products> product = GetProducts(keyword);
            ViewBag.keyword = keyword;
            ViewData["product"] = keyword;
            return View("Index");
        }

        //Link to productDetailPage
        public IActionResult ProductDetailPage(int id)
        {
            ViewData["username"] = Request.Cookies["username"];
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

            Products product = db.Products.FirstOrDefault(p => p.ProductId == id);
            List<Products> similarProducts = db.Products.Where(p => 
                                               (p.ProductCategory == product.ProductCategory) && (p!=product))
                                                .ToList();
            
            var reviews = db.Reviews.Where(r => r.ProductID == product.ProductId).ToList();
            double averageRating = reviews.Average(r=>r.StarRating);
            ViewData["reviews"] = reviews;
            ViewData["averageRating"] = averageRating;
            ViewData["product"] = product;
            ViewData["similarProducts"] = similarProducts;
            ViewData["firstname"] = Request.Cookies["firstname"];
            ViewData["sessionId"] = Request.Cookies["sessionId"];

            sessionid = Request.Cookies["sessionId"];
            ViewData["sessionId"] = sessionid;
            customerId = Request.Cookies["customerId"];
            ViewData["customerid"] = customerId;
            shoppingcart = db.ShoppingCartItem.Where(x => x.ShoppingCartId == customerId).ToList();
            shoppingcartNull = db.ShoppingCartItem.Where(x => x.ShoppingCartId == "0").ToList();

            if (sessionid != null)
            {
                ViewData["cartcount"] = shoppingcart.Count;
            }
            else
            {
                ViewData["cartcount"] = shoppingcartNull.Count;
            }

            bool boughtThis = false;
            List<Order> customerOrders = db.Order.Where(o => o.CustomerId == customerId).ToList();
            foreach (Order order in customerOrders) 
            {
                if (order.OrderDetails.Any(o => o.ProductId == id)) 
                {
                    boughtThis = true;
                    break;
                }
            }
            ViewData["boughtThis"] = boughtThis;

            bool reviewedThis = db.Reviews.Any(r => (r.CustomerId == customerId && r.ProductID == id));
            ViewData["reviewedThis"] = reviewedThis;

            return View();

        }

        public IActionResult RemoveReview(int id)
        {
            string customerid = Request.Cookies["customerId"];
            ViewData["customerid"] = customerid;
            string sessionid = Request.Cookies["sessionId"];
            if (sessionid == null)
            {
                customerid = "0";
            }
            var reviewselected = db.Reviews.FirstOrDefault(r => r.ProductID == id && r.CustomerId == customerid);
            if (reviewselected != null)
            {
                db.Reviews.Remove(reviewselected);
                db.SaveChanges();
            }
            return RedirectToAction("ProductDetailPage", "Gallery", new { id = id});
        }

    }
}