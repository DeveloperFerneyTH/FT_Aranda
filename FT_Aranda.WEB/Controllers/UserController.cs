using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FT_Aranda.Models.ViewModels;
using FT_Aranda.WEB.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FT_Aranda.WEB.Controllers
{
    public class UserController : Controller
    {
        // Globals
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpConsumerFactory httpConsumer;

        public UserController(ILogger<HomeController> logger, IHttpConsumerFactory httpConsumer)
        {
            _logger = logger;
            this.httpConsumer = httpConsumer;
        }

        public async Task<IActionResult> Index()
        {
            var users = await GetUsers();
            return View(users);
        }


        #region Methods

        private async Task<List<UserViewModel>> GetUsers()
        {
            var consumer = await httpConsumer.GetAsync(Properties.Resources.API_USER_GETUSERS, HttpContext.Session.GetString("JWToken"));

            if (consumer.IsSuccessStatusCode)
            {
                var users = JsonConvert.DeserializeObject<List<UserViewModel>>(consumer.Content.ReadAsStringAsync().Result);

                return users;
            }

            return new List<UserViewModel>();
        }

        #endregion
    }
}
