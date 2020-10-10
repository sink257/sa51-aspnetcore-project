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
            ViewData["sessionId"] = Request.Cookies["sessionId"];
            return View();
        }

        public static string hashPwd(string password)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            return System.Text.Encoding.ASCII.GetString(data);
        }

        public IActionResult Authenticate(string username, string password, string firstname)
        {
            Customer customers;
            customers = db.Customers.Where(x => x.Username == username).FirstOrDefault();

            string pwd = customers.Password;
            password = hashPwd(password);

            if (pwd != password && customers == null)
            {
                ViewData["username"] = username;
                ViewData["loginerror"] = "Incorrect Username or Password";
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
