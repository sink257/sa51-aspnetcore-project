using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Team8CA.Models;

namespace Team8CA.Controllers
{
    public class SearchController : Controller
    {
        //[HttpPost]
        //public IActionResult Index(string keywords)
        //{
        //    string[] products = { "product1", "product2", "product3", "product4" };
            
        //    int x;

        //    bool result = int.TryParse(keywords, out x);

        //    var product = (from p in products
        //                   where p == keywords
        //                   select p).ToList();

        //    return View(product);
        //}
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(string productName)
        {
            ViewBag.SearchKey = productName;

            return View();
        }
    }
}
