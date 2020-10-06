using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using Team8CA.Models;
//using Team8CA.Services;

namespace Team8CA.Controllers
{
    public class GalleryController : Controller
    {
        public IActionResult Index()
        {

            string[] imgs = {
                "photo-1593642632559-0c6d3fc62b89",
                "photo-1497366754035-f200968a6e72",
                "photo-1497366811353-6870744d04b2",
                "photo-1524758631624-e2822e304c36",
                "photo-1531973576160-7125cd663d86",
                "photo-1505409859467-3a796fd5798e",
                "photo-1564069114553-7215e1ff1890"
            };

            ViewData["images"] = imgs;
            ViewData["url_prefix"] = "https://images.unsplash.com/";
            ViewData["url_postfix"] = "?w=350";

            return View();
        }

        //public GalleryController(int ID, string ProductName, double ProductPrice, bool ProductAvailability, string ProductDescription)
        //{ 

        //}

        public IActionResult AntivirusAndSecurity()
        {
            return View();
        }



        public IActionResult BusinessAndOffice()
        {
            return View();

        }

        public IActionResult DesignAndIllustration()
        {
            return View();
        }

        //public IActionResult AddToCart([FromServices] CartRelatedService srv, int prdId)
        //{
        //    var customerId = HttpContext.Session.GetInt32("customerId") ?? 0;
        //    //if (customerId == 0)
        //    //{
        //    //    AddToCartForSession(srv, prdId, 1);
        //    //}
        //    //else
        //    //{
        //        ViewData["ItemCount"] = srv.AddProductsToCart(customerId, prdId, 1);
        //    //}
        //    return PartialView("_CartIcon");
        //}









    }
}