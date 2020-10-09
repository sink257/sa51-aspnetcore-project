using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using X.PagedList.Mvc;
using X.PagedList.Mvc.Core;
using Team8CA.DataAccess;
using Team8CA.Models;
using Team8CA.Services;

namespace Team8CA.Controllers
{
    public class GalleryController : Controller
    {
        protected AppDbContext db;

        public IActionResult Index(int ? page)                                          
        {                                                                      
            List<Products> product = db.Products.ToList();          
            ViewData["product"] = product;
            ViewData["sessionId"] = Request.Cookies["sessionId"];

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(db.Products.ToList().ToPagedList(pageNumber, pageSize));
        }

        /*public IActionResult Index([FromQuery] ProductParameters productParameters)
        {
            List<Products> product = db.Product.Skip((productParameters.PageNumber - 1) * productParameters.PageSize).ToList(productParameters);

            ViewData["product"] = product;
            ViewData["sessionId"] = Request.Cookies["sessionId"];

            /*int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(db.Products.ToPagedList(pageNumber, pageSize));
        }*/

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
            Products product = db.Products.First(p => p.Id == id);
            List<Products> similarProducts = db.Products.Where(p => 
                                               (p.ProductCategory == product.ProductCategory) && (p!=product))
                                                .ToList();
            ViewData["product"] = product;
            ViewData["similarProducts"] = similarProducts;
            return View("ProductDetailPage");
        }
    }
}