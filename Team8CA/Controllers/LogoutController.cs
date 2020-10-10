using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Team8CA.DataAccess;
using Team8CA.Models;

namespace Team8CA.Controllers
{
    public class LogoutController : Controller
    {
        protected AppDbContext db;
        private readonly Session sessions;

        public LogoutController(AppDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {

            string sessionId = HttpContext.Request.Cookies["sessionId"];
            db.Sessions.Remove(new Session()
            {
                SessionID = sessionId
            }
            );

            HttpContext.Response.Cookies.Delete("sessionId");
            //Session session = new Session()
            //{
            //    SessionID = "00",
            //    Username = "00",
            //    FirstName = "00",
            //    Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
            //    CustomerID = "00",
            //};
            //db.Sessions.Add(session);
            //db.SaveChanges();
            //Response.Cookies.Append("sessionId", session.SessionID);

            ViewData["username"] = Request.Cookies["username"];
            ViewData["sessionId"] = Request.Cookies["sessionId"];
            return View ("Logout");
        }
    }
}
