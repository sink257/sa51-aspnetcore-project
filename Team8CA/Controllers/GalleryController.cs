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

            return View();
        }


        public IActionResult BusinessAndOffice()
        {
            List<Products> product = db.Products.Where(p => (p.ProductCategory == "BusinessAndOffice")).ToList();


            return View();
        }

        public IActionResult DesignAndIllustration()
        {
            List<Products> product = db.Products.Where(p => (p.ProductCategory == "DesignAndIllustration")).ToList();


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
            Products product = db.Products.First(p => p.Id == id);
            ViewData["product"] = product;
            return View("ProductDetailPage");
        }
    }
}