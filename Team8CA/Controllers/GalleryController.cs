using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using Team8CA.DataAccess;
using Team8CA.Models;
using Team8CA.Services;
//using Team8CA.Services;

namespace Team8CA.Controllers
{
    public class GalleryController : Controller
    {

        protected AppDbContext db;
        public IActionResult Index()
        {
            List<Products> product = db.Products.ToList();

            ViewData["product"] = product;
            ViewData["username"] = Request.Cookies["username"];
            ViewData["sessionId"] = Request.Cookies["sessionId"];


            return View();
        }

        //public GalleryController(int ID, string ProductName, double ProductPrice, bool ProductAvailability, string ProductDescription)
        //{ 

        //}

        public IActionResult AntivirusAndSecurity()
        {

            List<Products> product = db.Products.Where(p => (p.ProductCategory == "AntivirusandSecurity")).ToList();


            ViewData["product"] = product;

            ViewData["sessionId"] = Request.Cookies["sessionId"];

            return View();
        }


        public IActionResult BusinessAndOffice()
        {
            List<Products> product = db.Products.Where(p => (p.ProductCategory == "BusinessAndOffice")).ToList();

            ViewData["product"] = product;

            ViewData["sessionId"] = Request.Cookies["sessionId"];


            return View();
        }

        public IActionResult DesignAndIllustration()
        {
            List<Products> product = db.Products.Where(p => (p.ProductCategory == "DesignAndIllustration")).ToList();

            ViewData["product"] = product;

            ViewData["sessionId"] = Request.Cookies["sessionId"];

            return View();
        }

        public GalleryController(AppDbContext db)
        {
            this.db = db;
        }
        public List<Products> GetProducts(string query)
        {
            List<Products> products = db.Products.ToList();
            {
                if (query == "" || query == null)
                {
                    return db.Products.ToList();
                }

                return db.Products.Where(p =>
                        p.ProductName.ToLower().Contains(query.ToLower()) ||
                        p.ProductDescription.ToLower().Contains(query.ToLower()))
                    .ToList();
            }
        }

        //Link to productDetailPage
        public IActionResult ProductDetailPage(int id)
        {
            Products product = db.Products.FirstOrDefault(p => p.Id == id);
            List<Products> similarProducts = db.Products.Where(p => 
                                               (p.ProductCategory == product.ProductCategory) && (p!=product))
                                                .ToList();
            
            var reviews = db.Reviews.Where(r => r.ProductID == id).ToList();
            double averageRating = reviews.Average(r=>r.StarRating);
            ViewData["reviews"] = reviews;
            ViewData["averageRating"] = averageRating;
            ViewData["product"] = product;
            ViewData["similarProducts"] = similarProducts;
            return View("ProductDetailPage");
        }
    }
}