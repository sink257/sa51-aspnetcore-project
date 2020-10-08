using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Team8CA.Models;
using Team8CA.Services;

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

    }
}
