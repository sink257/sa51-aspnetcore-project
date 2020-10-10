﻿using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using X.PagedList;
//using X.PagedList.Mvc;
//using X.PagedList.Mvc.Core;
//using X.PagedList.Web.Common;
using Team8CA.DataAccess;
using Team8CA.Models;
using PagedList;
using PagedList.Mvc;
using PagedList.Core.Mvc;
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

            ViewData["username"] = Request.Cookies["username"];
            ViewData["sessionId"] = Request.Cookies["sessionId"];
            return View();
        }

        public IActionResult BusinessAndOffice()
        {
            List<Products> product = db.Products.Where(p => (p.ProductCategory == "BusinessAndOffice")).ToList();
            ViewData["product"] = product;

            ViewData["username"] = Request.Cookies["username"];
            ViewData["sessionId"] = Request.Cookies["sessionId"];
            return View();
        }

        public IActionResult DesignAndIllustration()
        {
            List<Products> product = db.Products.Where(p => (p.ProductCategory == "DesignAndIllustration")).ToList();
            ViewData["product"] = product;

            ViewData["username"] = Request.Cookies["username"];
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

        public IActionResult PageNumber(int ? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(db.Products.ToList().ToPagedList(pageNumber, pageSize));
        }

        //Link to productDetailPage
        public IActionResult ProductDetailPage(int id)
        {
            Products product = db.Products.FirstOrDefault(p => p.Id == id);
            List<Products> similarProducts = db.Products.Where(p => 
                                               (p.ProductCategory == product.ProductCategory) && (p!=product))
                                                .ToList();
            
            var reviews = db.Reviews.Where(r => r.ProductID == product.Id).ToList();
            double averageRating = reviews.Average(r=>r.StarRating);
            ViewData["reviews"] = reviews;
            ViewData["averageRating"] = averageRating;
            ViewData["product"] = product;
            ViewData["similarProducts"] = similarProducts;
            ViewData["username"] = Request.Cookies["username"];
            ViewData["sessionId"] = Request.Cookies["sessionId"];
            return View("ProductDetailPage");
        }

    }
}