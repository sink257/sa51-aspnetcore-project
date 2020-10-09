using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            ViewData["username"] = Request.Cookies["username"];
            ViewData["sessionId"] = Request.Cookies["sessionId"];

            return View();
        }

        public IActionResult AddToShoppingCart(int productid)
        {
            var productselected = _appDbContext.Products.FirstOrDefault(x => x.Id == productid);
            string customerid = Request.Cookies["customerId"];
            if(productselected != null)
            {
                _shoppingcart.AddToCart(productselected, 1, customerid);
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


        //private void AddCartItems([FromServices] CartRelatedService cartservice, int productid, int quantity)
        //{
        //    var session = Request.Cookies["sessionId"];
        //    var cart = new ShoppingCart();
        //    if (session == null)
        //    {
        //        cartservice.AddCartItems(productid, quantity, cart);
        //    }
        //    else
        //    {
        //        var shpcart = cart.CartItems.FirstOrDefault(x => x.ProductId == productid);
        //        cartservice.AddCartItems(productid, quantity, cart);
        //    }
        //}

        //public IActionResult AddCartGallery ([FromServices] CartRelatedService cartservice, int productid)
        //{
        //    var customerid = Request.Cookies["customerId"];
        //    if (customerid ==  null)
        //    {
        //        AddCartItems(cartservice, productid, 1);
        //    }
        //    else
        //    {
        //        ViewData["ItemCount"] = cartservice.AddToCart(customerid, productid, 1, cartid);
        //    }

        //    return View("Gallery");
        //}




    }
}
