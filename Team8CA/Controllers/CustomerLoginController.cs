using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Team8CA.DataAccess;
using Team8CA.Models;

namespace Team8CA.Controllers
{
    
    public class CustomerLoginController : Controller
    {
        private readonly Customer customers;
        private readonly Session sessions;
        protected AppDbContext db;

        public CustomerLoginController(Customer customers, Session sessions, AppDbContext db)
        {
            this.customers = customers;
            this.sessions = sessions;
            this.db = db;
        }

        [Route("Login")]
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
            return View();
        }
        public IActionResult Authenticate(string username, string password, string firstname)
        {
            Customer customers;
            customers = db.Customers.Where(x => x.Username == username && x.Password == password).FirstOrDefault();

            if (customers == null)
            {
                ViewData["username"] = username;
                ViewData["errMsg"] = "haha loser can't login";
                return View("Index");
            }
            else
            {
                Session session = new Session()
                {
                    SessionID = Guid.NewGuid().ToString(),
                    Username = customers.Username,
                    FirstName = customers.FirstName,
                    Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
                    CustomerID = customers.CustomerID
                };
                db.Sessions.Add(session);
                db.SaveChanges();

                Response.Cookies.Append("username", session.Username);
                Response.Cookies.Append("firstname", session.FirstName);                
                Response.Cookies.Append("customerId", session.CustomerID);
                Response.Cookies.Append("sessionId", session.SessionID);
                return RedirectToAction("Index", "Gallery");
            }
        }
    }
}
