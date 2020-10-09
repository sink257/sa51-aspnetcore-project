using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Team8CA.DataAccess;
using Team8CA.Models;
//using Team8CA.Services;

namespace Team8CA.Controllers
{
    public class CreateReviewController : Controller
    {
        protected AppDbContext db;
        public CreateReviewController(AppDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index(int id)
        {  
            Products product = db.Products.First(p => p.Id == id);

            ViewData["username"] = Request.Cookies["username"];
            ViewData["sessionId"] = Request.Cookies["sessionId"];            
            ViewData["product"] = product;

            return View();
        }


    }
}
