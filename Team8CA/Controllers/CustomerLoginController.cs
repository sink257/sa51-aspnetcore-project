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

                    //Merging of shopping cart
                    List<ShoppingCart> shoppingcartnull = db.ShoppingCart.Where(x => x.ShoppingCartId == "0").ToList();
                    foreach (ShoppingCart cart in shoppingcartnull)
                    {
                        TemporaryShoppingCart temporaryshoppingCarts = db.TemporaryShoppingCart.FirstOrDefault(
                            x => x.CustomerId == session.CustomerID && x.IsCheckOut == false);

                        if (temporaryshoppingCarts == null)
                        {
                            TemporaryShoppingCart tempnewcart = new TemporaryShoppingCart()
                            {
                                TemporaryShoppingCartId = session.CustomerID,
                                CustomerId = session.CustomerID,
                                OrderCreationTime = DateTime.Now,
                                IsCheckOut = false,
                            };
                            db.TemporaryShoppingCart.Add(tempnewcart);
                        }
                        db.SaveChanges();
                    }

                    List<ShoppingCartItem> shoppingcartitemnull = db.ShoppingCartItem.Where(x => x.ShoppingCartId == "0").ToList();
                    foreach (ShoppingCartItem tempshoppingcartitem in shoppingcartitemnull)
                    {
                        TemporaryShoppingCartItem tempShoppingCartItems = db.TemporaryShoppingCartItem.FirstOrDefault(
                           x => x.Products.ProductId == tempshoppingcartitem.ProductsId && x.TemporaryShoppingCartId == session.CustomerID && x.TemporaryShoppingCart.IsCheckOut == false);
                        if (tempShoppingCartItems == null)
                        {

                            TemporaryShoppingCartItem tempnewcartitem = new TemporaryShoppingCartItem()
                            {
                                TemporaryShoppingCartId = session.CustomerID,
                                CustomerId = session.CustomerID,
                                ProductsId = tempshoppingcartitem.ProductsId,
                                Quantity = tempshoppingcartitem.Quantity
                            };
                            db.TemporaryShoppingCartItem.Add(tempnewcartitem);
                        }
                        else
                        {
                            tempShoppingCartItems.Quantity++;
                        }
                    }

                    db.ShoppingCart.RemoveRange(shoppingcartnull);
                    db.ShoppingCartItem.RemoveRange(shoppingcartitemnull);
                    db.SaveChanges();

                    List<TemporaryShoppingCart> shoppingcart = db.TemporaryShoppingCart.Where(x => x.TemporaryShoppingCartId == session.CustomerID).ToList();

                    foreach (TemporaryShoppingCart newshoppingcart in shoppingcart)
                    {
                        ShoppingCart shoppingCarts = db.ShoppingCart.FirstOrDefault(
                            x => x.CustomerId == session.CustomerID && x.IsCheckOut == false);

                        if (shoppingCarts == null)
                        {
                            ShoppingCart newcart = new ShoppingCart()
                            {
                                ShoppingCartId = session.CustomerID,
                                CustomerId = session.CustomerID,
                                OrderCreationTime = DateTime.Now,
                                IsCheckOut = false,
                            };
                            db.ShoppingCart.Add(newcart);
                        }
                        db.SaveChanges();
                    }

                    List<TemporaryShoppingCartItem> shoppingcartitem = db.TemporaryShoppingCartItem.Where(x => x.TemporaryShoppingCartId == session.CustomerID).ToList();
                    foreach (TemporaryShoppingCartItem newshoppingcartitem in shoppingcartitem)
                    {
                        ShoppingCartItem shoppingCartItems = db.ShoppingCartItem.FirstOrDefault(
                            x => x.Products.ProductId == newshoppingcartitem.ProductsId && x.ShoppingCartId == session.CustomerID && x.ShoppingCart.IsCheckOut == false);
                        if (shoppingCartItems == null)
                        {
                            ShoppingCartItem newcartitem = new ShoppingCartItem()
                            {
                                ShoppingCartId = session.CustomerID,
                                CustomerId = session.CustomerID,
                                ProductsId = newshoppingcartitem.ProductsId,
                                Quantity = newshoppingcartitem.Quantity
                            };
                            db.ShoppingCartItem.Add(newcartitem);
                        }
                        else
                        {
                            shoppingCartItems.Quantity++;
                        }
                    }
                    db.TemporaryShoppingCart.RemoveRange(shoppingcart);
                    db.TemporaryShoppingCartItem.RemoveRange(shoppingcartitem);
                    db.SaveChanges();

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
