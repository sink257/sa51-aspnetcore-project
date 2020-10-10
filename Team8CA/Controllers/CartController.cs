using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Team8CA.DataAccess;
using Team8CA.Models;

namespace Team8CA.Controllers
{
    public class CartController : Controller
    {

        private readonly AppDbContext db;
        private readonly ShoppingCart _shoppingcart;

        public CartController(AppDbContext appDbContext, ShoppingCart shoppingcart)
        {
            db = appDbContext;
            _shoppingcart = shoppingcart;
        }

        public IActionResult Index()
        {
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
                ViewData["shoppingcartitems"] = shoppingcart;

            }
            else
            {
                ViewData["cartcount"] = shoppingcartNull.Count;
                ViewData["shoppingcartitems"] = shoppingcartNull;
            }

            return View();
        }

        public IActionResult Checkout()
        {
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
                ViewData["shoppingcartitems"] = shoppingcart;

            }
            else
            {
                ViewData["cartcount"] = shoppingcartNull.Count;
                ViewData["shoppingcartitems"] = shoppingcartNull;
            }
            return View();
        }

        public IActionResult AddToShoppingCart(int productid)
        {
            var productselected = db.Products.FirstOrDefault(x => x.ProductId == productid);
            
            string customerid = Request.Cookies["customerId"];

            string sessionid = Request.Cookies["sessionId"];
            if (sessionid == null)
            {
                customerid = "0";
            }

            if(productselected != null)
            {
                _shoppingcart.AddToCart(productselected, productid, 1, customerid, sessionid);
            }
            return Redirect("http://localhost:61024/");
        }

        public IActionResult RemoveFromShoppingCart(int productid)
        {
            var productselected = db.Products.FirstOrDefault(x => x.ProductId == productid);
            if (productselected != null)
            {
                _shoppingcart.RemoveFromCart(productselected);
            }
            return Redirect("http://localhost:61024/");
        }
    }
}
