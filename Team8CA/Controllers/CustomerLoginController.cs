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
        public IActionResult Authenticate(string username, string password)
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
                    Id = Guid.NewGuid().ToString(),
                    Username = customers.Username, // to check this
                    Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds()
                };
                db.Sessions.Add(session);
                db.SaveChanges();

                Response.Cookies.Append("username", session.Username);
                Response.Cookies.Append("sessionId", session.Id);
                return RedirectToAction("Index", "Gallery");
            }
        }
    }
}
