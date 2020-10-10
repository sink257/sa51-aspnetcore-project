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

    public class ProductDetailController : Controller
    {
        private readonly AppDbContext db;
        private readonly ShoppingCart _shoppingcart;

        public ProductDetailController(AppDbContext appDbContext, ShoppingCart shoppingcart)
        {
            db = appDbContext;
            _shoppingcart = shoppingcart;
        }

        [Route("ProductDetail")]
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
            }
            else
            {
                ViewData["cartcount"] = shoppingcartNull.Count;
            }
            return View("ProductDetailPage");
        }
    }


}
