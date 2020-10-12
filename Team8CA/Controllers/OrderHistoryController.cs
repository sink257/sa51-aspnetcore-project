using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Team8CA.DataAccess;
using Team8CA.Models;


namespace Team8CA.Controllers
{
    public class OrderHistoryController : Controller
    {


        private readonly AppDbContext db;
        private readonly ShoppingCart shoppingcart;
      

        public OrderHistoryController(AppDbContext db, ShoppingCart shoppingcart)
        {
            this.db = db;
            this.shoppingcart = shoppingcart;
        }

        public IActionResult Index()
        {
            string customerId = Request.Cookies["customerId"];
            List<Order> orders = db.Order.Where(o=>o.CustomerId == customerId).ToList();
           
            if(orders.Count == 0)
            {
                return RedirectToAction("EmptyCart");
            }
            else
                ViewData["order"] = orders;

            ViewData["firstname"] = Request.Cookies["firstname"];
            string sessionid = Request.Cookies["sessionId"];
            ViewData["sessionId"] = sessionid;
           
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
            return View();
        }
        public IActionResult RecentOrder(int orderId)
        {
           // List<OrderDetails> orderdetail = db.OrderDetails.Where(o => o.OrderId == orderId ).ToList();
            List<OrderDetails> orderdetails = db.OrderDetails.Where(o => o.OrderId == orderId ).ToList();
            ViewData["orderdetail"] = orderdetails;
            return View();
        }
        public IActionResult EmptyCart()
        {
            return View();
        }
    }
}
