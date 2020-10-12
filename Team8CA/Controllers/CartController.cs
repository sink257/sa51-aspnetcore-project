using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Team8CA.DataAccess;
using Team8CA.Models;
using Team8CA.Services;

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
            string sessionid = Request.Cookies["sessionId"];
            ViewData["sessionId"] = sessionid;
            string customerId = Request.Cookies["customerId"];
            ViewData["customerid"] = customerId;
            ViewData["firstname"] = Request.Cookies["firstname"];
            if (sessionid == null)
            {
                return Redirect("http://localhost:61024/Login");
            }
            else
            {

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
        }

        public IActionResult AddToShoppingCart([FromServices] CartRelatedService service, int productid)
        {

            var productselected = db.Products.FirstOrDefault(x => x.ProductId == productid);

            string customerid = Request.Cookies["customerId"];

            string sessionid = Request.Cookies["sessionId"];

            if (productselected != null)
            {
                service.AddToCart(productselected, productid, 1, customerid, sessionid);
            }
            return Redirect("http://localhost:61024/");
        }

        public IActionResult AddSimilarToShoppingCart([FromServices] CartRelatedService service, int productid)
        {

            var productselected = db.Products.FirstOrDefault(x => x.ProductId == productid);

            string customerid = Request.Cookies["customerId"];

            string sessionid = Request.Cookies["sessionId"];

            if (productselected != null)
            {
                service.AddToCart(productselected, productid, 1, customerid, sessionid);
            }
            return RedirectToAction("ProductDetailPage", "Gallery", new { id = productid });
        }

        public IActionResult AddMultipleToShoppingCart([FromServices] CartRelatedService service, int productid, int quantity, bool? buyNow)
        {

            var productselected = db.Products.FirstOrDefault(x => x.ProductId == productid);

            string customerid = Request.Cookies["customerId"];

            string sessionid = Request.Cookies["sessionId"];

            if (productselected != null)
            {
                service.AddToCart(productselected, productid, quantity, customerid, sessionid);
            }
            if (buyNow == true)
            {
                return RedirectToAction("Index", "Cart");
            }
            else
            {
                return RedirectToAction("ProductDetailPage", "Gallery", new { id = productid });
            }
        }

        public IActionResult AddInCart([FromServices] CartRelatedService service, int productid)
        {
            var productselected = db.Products.FirstOrDefault(x => x.ProductId == productid);

            string customerid = Request.Cookies["customerId"];

            string sessionid = Request.Cookies["sessionId"];
            if (sessionid == null)
            {
                customerid = "0";
            }

            if (productselected != null)
            {
                service.AddToCart(productselected, productid, 1, customerid, sessionid);
            }
            return Redirect("http://localhost:61024/Cart");
        }

        public IActionResult RemoveFromShoppingCart([FromServices] CartRelatedService service, int productid)
        {
            var productselected = db.Products.FirstOrDefault(x => x.ProductId == productid);

            string customerid = Request.Cookies["customerId"];

            string sessionid = Request.Cookies["sessionId"];
            if (sessionid == null)
            {
                customerid = "0";
            }

            if (productselected != null)
            {
                service.RemoveFromCart(productselected, customerid, sessionid);
            }
            return Redirect("http://localhost:61024/Cart");
        }

        public IActionResult RemoveCartRow([FromServices] CartRelatedService service, int productid)
        {
            var productselected = db.Products.FirstOrDefault(x => x.ProductId == productid);

            string customerid = Request.Cookies["customerId"];

            string sessionid = Request.Cookies["sessionId"];
            if (sessionid == null)
            {
                customerid = "0";
            }

            if (productselected != null)
            {
                service.RemoveRow(productselected, customerid, sessionid);
            }
            return Redirect("http://localhost:61024/Cart");
        }

        public IActionResult Checkout([FromServices] CartRelatedService service)
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

            service.CheckoutCart(customerId);
            service.ClearCart(customerId);

            return RedirectToAction("CheckoutComplete");
        }

        public IActionResult CheckoutComplete()
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

            ViewBag.CheckoutCompleteMessage = "Thank you for shopping with us. Please enjoy your products!";
            return View();
        }
    }
}
