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

        public LogoutController(AppDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            ViewData["cartcount"] = 0;
            string sessionId = HttpContext.Request.Cookies["sessionId"];
            db.Sessions.Remove(new Session()
            {
                SessionID = sessionId
            }
            );

            HttpContext.Response.Cookies.Delete("sessionId");

            return View ("Logout");
        }
    }
}
