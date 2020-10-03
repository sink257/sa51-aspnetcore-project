using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using Team8CA.Models;

namespace Team8CA.Controllers
{
    public class GalleryController : Controller
    {
        private const int pageSize = 3;
        public IActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            IPagedList<GalleryProducts> model = GetGetList().ToPagedList(pageNumber, pageSize);

            string[] imgs = {
                "photo-1593642632559-0c6d3fc62b89",
                "photo-1497366754035-f200968a6e72",
                "photo-1497366811353-6870744d04b2",
                "photo-1524758631624-e2822e304c36",
                "photo-1531973576160-7125cd663d86",
                "photo-1505409859467-3a796fd5798e",
                "photo-1564069114553-7215e1ff1890"
            };

            ViewData["images"] = imgs;
            ViewData["url_prefix"] = "https://images.unsplash.com/";
            ViewData["url_postfix"] = "?w=350";

            return View(model);
        }

        private static List<GalleryProducts> GetGetList()
        {
            return GalleryProducts.GetList;
        }
    }
}
