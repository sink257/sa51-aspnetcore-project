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
                "/images/adguardpic.jpg",
                "/images/avira.jpg",
                "/images/creative_cloud.jpg",
                "/images/creativesuite.jpg",
                "/images/illustrator.jpg",
                "/images/malwarebytes.png",
                "/images/photoshop1.jpg",
                "/images/project.jpg",
                "/images/visio.jpg",
            };

            ViewData["images"] = imgs;

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