﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Team8CA.Controllers
{
    public class OrderHistory : Controller
    {
        public IActionResult Index()
        {
            ViewData["sessionId"] = Request.Cookies["sessionId"];
            return View();
        }
    }
}
