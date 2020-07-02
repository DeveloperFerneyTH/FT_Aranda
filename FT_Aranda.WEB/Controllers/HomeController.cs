using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FT_Aranda.WEB.Models;
using FT_Aranda.Models.Entities;
using FT_Aranda.WEB.Tools;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace FT_Aranda.WEB.Controllers
{
    public class HomeController : Controller
    {
        // Globals
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpConsumerFactory httpConsumer;

        public HomeController(ILogger<HomeController> logger, IHttpConsumerFactory httpConsumer)
        {
            _logger = logger;
            this.httpConsumer = httpConsumer;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            var model = new User();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User model)
        {
            var consumer = await httpConsumer.PostAsync(Properties.Resources.API_LOGIN_AUTHENTICATE, model);

            if (consumer.IsSuccessStatusCode)
            {
                var user = JsonConvert.DeserializeObject<User>(consumer.Content.ReadAsStringAsync().Result);

                if (user.Token == null)
                {
                    return View();
                }

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, user.Role.Name));
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


                HttpContext.Session.SetString("JWToken", user.Token);
                return RedirectToAction("Index", "Welcome", new { name = user.FirtsName });
            }
            else
            {
                return View();
            }            
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
