using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Team8CA.Controllers
{
    public class ProductDetailController : Controller
    {
        [Route("ProductDetail")]
        public IActionResult ProductDetailPage()
        {
            return View();
        }



    }


}
