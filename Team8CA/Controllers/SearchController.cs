using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Products>> GetProducts(string query)
        {
            List<Products> product = db.Products.ToList();
            {
                if (!String.IsNullOrEmpty(query))
                {
                    return await db.Products.Where(p =>
                        p.ProductName.ToLower().Contains(query.ToLower()) ||
                        p.ProductDescription.ToLower().Contains(query.ToLower()))
                    .ToListAsync();
                }

                return db.Products.ToList();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(string query)
        {
            List<Products> product = await GetProducts(query);
            ViewBag.keyword = query;
            ViewData["product"] = product;
            return View("Index");
        }
    }
}
