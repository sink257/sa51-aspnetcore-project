using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Team8CA.DataAccess;
using Team8CA.Models;

namespace Team8CA.Controllers
{
    public class SearchController : Controller
    {
        protected AppDbContext db;

        public SearchController(AppDbContext db)
        {
            this.db = db;
        }

        public List<Products> GetProducts(string query)
        {
            List<Products> product = db.Products.ToList();
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

        [HttpPost]
        public IActionResult Index(string query)
        {
            List<Products> product = GetProducts(query);
            ViewBag.keyword = query;
            ViewData["product"] = product;
            return View("Index");
        }
    }
}
