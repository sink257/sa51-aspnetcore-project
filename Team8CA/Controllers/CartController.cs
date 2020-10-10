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

        private readonly AppDbContext _appDbContext;
        private readonly ShoppingCart _shoppingcart;

        public CartController(AppDbContext appDbContext, ShoppingCart shoppingcart)
        {
            _appDbContext = appDbContext;
            _shoppingcart = shoppingcart;
        }

        public IActionResult Index()
        {
            ViewData["firstname"] = Request.Cookies["firstname"];
            string sesID = Request.Cookies["sessionId"];
            ViewData["sessionId"] = sesID;
            List<ShoppingCartItem> shoppingcart = _appDbContext.ShoppingCartItem.Where(x => x.ShoppingCartId == sesID).ToList();
            List<ShoppingCartItem> shoppingcartNull = _appDbContext.ShoppingCartItem.Where(x => x.ShoppingCartId == null).ToList(); 
            if (sesID != null)
            {
                ViewData["cartcount"] = shoppingcart.Count;
            }
            else
            {
                ViewData["cartcount"] = shoppingcartNull.Count;
            }

            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }



        public IActionResult AddToShoppingCart(int productid)
        {
            var productselected = _appDbContext.Products.FirstOrDefault(x => x.Id == productid);
            
            string customerid = Request.Cookies["customerId"];

            string sessionid = Request.Cookies["sessionId"];
            if (sessionid == null)
            {
                customerid = "0";
            }

            if(productselected != null)
            {
                _shoppingcart.AddToCart(productselected, 1, customerid, sessionid);
            }
            return Redirect("http://localhost:61024/");
        }

        public IActionResult RemoveFromShoppingCart(int productid)
        {
            var productselected = _appDbContext.Products.FirstOrDefault(x => x.Id == productid);
            if (productselected != null)
            {
                _shoppingcart.RemoveFromCart(productselected);
            }
            return Redirect("http://localhost:61024/");
        }
    }
}
