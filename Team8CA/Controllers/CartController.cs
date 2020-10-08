using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Team8CA.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            ViewData["username"] = Request.Cookies["username"];
            ViewData["sessionId"] = Request.Cookies["sessionId"];

            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }





    }
}
