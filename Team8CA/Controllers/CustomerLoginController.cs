using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Team8CA.DataAccess;
using Team8CA.Models;

namespace Team8CA.Controllers
{
    public class CustomerLoginController : Controller
    {
        private readonly CustomerLogin customers;
        protected AppDbContext db;

        public CustomerLoginController(CustomerLogin customers, AppDbContext db)
        {
            this.customers = customers;
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Authenticate(string username, string password)
        {
            CustomerLogin customers;
            customers = (CustomerLogin)db.CustomerLogin.Where(x => x.Username == username && x.Password == password);
            return View("Index");
        }
    }
}
