using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Team8CA.Models;

namespace Team8CA.Controllers
{
    public class AccountController : Controller
    {
                
        //GET : Account
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Verify(Account acc)
        {
            return View();
        }
    }
}
