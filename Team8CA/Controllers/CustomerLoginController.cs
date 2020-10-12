using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
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

        public static string hashPwd(string password)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            return System.Text.Encoding.ASCII.GetString(data);
        }

        public IActionResult Authenticate(string username, string password)
        {
            Customer customers;
            customers = db.Customers.Where(u => u.Username.Equals(username)).FirstOrDefault();

            if (!string.IsNullOrEmpty(password))
            {
                password = hashPwd(password);

                if (customers == null)
                {
                    ViewData["username"] = username;
                    ViewData["loginerror"] = "Incorrect Username";
                    return View("Index");
                }
                else if (customers.Password != password)
                {
                    ViewData["username"] = username;
                    ViewData["loginerror"] = "Incorrect Password";
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

                    //List<ShoppingCart> shoppingcart = db.ShoppingCart.Where(x => x.ShoppingCartId == "0").ToList();
                    //foreach(ShoppingCart cart in shoppingcart)
                    //{
                    //    db.ShoppingCart.Remove(cart);
                    //    ShoppingCart newcart = new ShoppingCart()
                    //    {
                    //        ShoppingCartId = session.CustomerID,
                    //        CustomerId = session.CustomerID,
                    //        OrderCreationTime = DateTime.Now,
                    //        IsCheckOut = false,
                    //    };
                    //    db.ShoppingCart.Add(newcart);
                    //}
                    //db.SaveChanges();

                    //List<ShoppingCartItem> shoppingcartNull = db.ShoppingCartItem.Where(x => x.ShoppingCartId == "0").ToList();
                    //foreach (ShoppingCartItem shoppingcartitem in shoppingcartNull)
                    //{
                    //    db.ShoppingCartItem.Remove(shoppingcartitem);
                    //    ShoppingCartItem newcartitem = new ShoppingCartItem()
                    //    {
                    //        ShoppingCartItemId = shoppingcartitem.ShoppingCartItemId,
                    //        ShoppingCartId = session.CustomerID,
                    //        CustomerId = session.CustomerID,
                    //        ProductsId = shoppingcartitem.ProductsId,
                    //        Quantity = shoppingcartitem.Quantity
                    //    };
                    //    db.ShoppingCartItem.Add(newcartitem);
                    //}

                    //db.SaveChanges();

                    Response.Cookies.Append("username", session.Username);
                    Response.Cookies.Append("firstname", session.FirstName);
                    Response.Cookies.Append("customerId", session.CustomerID);
                    Response.Cookies.Append("sessionId", session.SessionID);
                    return RedirectToAction("Index", "Gallery");
                }
            }
            else
            {
                ViewData["username"] = username;
                ViewData["loginerror"] = "Please enter your password";
                return View("Index");
            }
        }
    }
}
