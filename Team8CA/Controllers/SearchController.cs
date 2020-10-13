using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                        p.ProductDescription.ToLower().Contains(query.ToLower()) || 
                        p.ProductCategory.ToLower().Contains(query.ToLower()))
                    .ToListAsync();
                }

                return db.Products.ToList();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(string query)
        {
            ViewData["username"] = Request.Cookies["username"];
            ViewData["firstname"] = Request.Cookies["firstname"];
            string sessionid = Request.Cookies["sessionId"];
            ViewData["sessionId"] = sessionid;
            string customerId = Request.Cookies["customerId"];
            ViewData["customerid"] = customerId;
            List<ShoppingCartItem> shoppingcart = db.ShoppingCartItem.Where(x => x.ShoppingCartId == customerId).ToList();
            List<ShoppingCartItem> shoppingcartNull = db.ShoppingCartItem.Where(x => x.ShoppingCartId == "0").ToList();
            if (sessionid != null)
            {
                ViewData["cartcount"] = shoppingcart.Count;
            }
            else
            {
                ViewData["cartcount"] = shoppingcartNull.Count;
            }
            List<Products> product = await GetProducts(query);
            ViewBag.keyword = query;
            ViewData["product"] = product;
            return View("Index");
        }
    }
}
