using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FT_Aranda.WEB.Controllers
{
    public class WelcomeController : Controller
    {
        public IActionResult Index(string name)
        {
            ViewBag.Name = name;
            return View();
        }
    }
}
