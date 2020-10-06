using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team8CA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Team8CA.DataAccess
{
    public class DBInitialiser
    {
        public DBInitialiser (AppDbContext db)
        {
            db.Customers.Add(new Customer("admin", "password1"));

            db.Products.Add(new Product(3333, "Adobe Creative Cloud", 150.00, true, "Creative Cloud is a collection of 20+ desktop and mobile apps and services for photography, design, video, web, UX and more. Now you can take your ideas to new places with Photoshop on the iPad, draw and paint with Fresco, and design for 3D and AR.", "BusinessAndOffice"));
            db.Products.Add(new Product(3344, "Microsoft Visio 2016 Professional ", 130.00, true, "Microsoft Visio Professional 2016 is a powerful diagramming platform with a rich set of built-in stencils. It helps you simplify complex information through simple, easy-to-understand diagrams.", "BusinessAndOffice"));
            db.Products.Add(new Product(3355, "Microsoft Project 2019 Professional", 150.00, true, "Manage your projects from one convenient location using Microsoft Project Professional 2019. The project management software can be bused to plan projects, collaborate with others and stay up-to-date on project progress. Project Professional 2019 includes cutting edge tools that will help your whole team work better together. This user-preferred software can be used during the entire creative process, from brainstorming sessions to production to the release of the finished product.", "BusinessAndOffice"));
            db.Products.Add(new Product(2233, "Avira Antivirus (1 year)", 59.00, true, "Avira Antivirus Pro 1 year - 1 device is a professional antivirus software aiming to provide you with a high level of protection against the most common digital threats.", "AntivirusAndSecurity"));
            db.Products.Add(new Product(2244, "Malwarebytes for Mac", 59.00, true, "Malwarebytes 4 takes out malware, adware, spyware, and other threats before they can infect your machine and ruin your day. It'll keep you safe online and your Mac running like it should.", "AntivirusAndSecurity"));
            db.Products.Add(new Product(2255, "Adguard for Windows", 20.00, true, "AdGuard for Windows is not just another ad blocker, it is a multipurpose tool that combines all necessary features for the best web experience. It blocks ads and dangerous websites, speeds up page loading and protects your children when they are online.", "AntivirusAndSecurity"));
            db.Products.Add(new Product(1133, "Adobe Photoshop Elements 2020", 120.00, true, "Adobe Photoshop Elements 2020 It’s never been easier to create incredible photos and keepsakes. Put your best photos forward with auto-generated creations and intelligent editing options. Easily organize and share your photos, and even turn your favorites into frame-worthy prints and memorable gifts.", "DesignAndIllustration"));
            db.Products.Add(new Product(1144, "Adobe Illustrator 5", 130.00, true, "Adobe Illustrator CS5 software helps you create distinctive vector artwork for any project. Take advantage of the precision and power of sophisticated drawing tools, expressive natural brushes, and a host of time-savers.", "DesignAndIllustration"));
            db.Products.Add(new Product(1155, "Adobe Creative Suite 5.5", 399.00, true, "Adobe Creative Suite 5.5 delivers important advances in HTML5 and Flash authoring, enabling designers and developers to create compelling content and applications. Creative Suite 5.5 products also feature significant innovation in the areas of video production and editing. Finally, Adobe Creative Suite 5.5 delivers exciting new capabilities in the exploding area of digital publishing.", "DesignAndIllustration"));

        }
    }
}
