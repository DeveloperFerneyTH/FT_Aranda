using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using FT_Aranda.Models.Entities;
using FT_Aranda.Models.ViewModels;
using FT_Aranda.SQLReposity.Workers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FT_Aranda.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        // Globals 
        private readonly ILoginWorker loginWorker;
        private readonly IMapper mapper;
        private readonly IWritterLog log;

        public LoginController(ILoginWorker loginWorker, IMapper mapper, IWritterLog log)
        {
            this.loginWorker = loginWorker;
            this.mapper = mapper;
            this.log = log;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Authenticate(AuthenticateViewModel model)
        {
            try
            {
                log.WriteLog("Login", MethodBase.GetCurrentMethod().Name, model);
                int badStatusCode = (int)HttpStatusCode.BadRequest;
                var loginInfo = await loginWorker.GetLoginInfo(model.UserName);

                if (loginInfo != null)
                {
                    if (loginInfo.Attemps >= 3)
                    {
                        return BadRequest(new { code = badStatusCode, message = "Has intentado ingresar más de 3 veces, por favor intenta más tarde o comunicate con algún administrador" });
                    }
                    if ((bool)loginInfo.Blocked)
                    {
                        return BadRequest(new { code = badStatusCode, message = "Tu usuario se encuentra bloqueado, por favor comunicate con algún administrador" });
                    }
                }

                var user = await loginWorker.Auhenticate(model.UserName, model.Password);

                if (user == null)
                {
                    await loginWorker.CreateorUpdateLoginInfo(model.UserName, false);
                    return BadRequest(new { code = badStatusCode, message = "Tu usuario o contraseña no son conrrectos, por favor intentalo de nuevo" });
                }

                await loginWorker.CreateorUpdateLoginInfo(user.UserName, true);
                user.Password = string.Empty;

                return Ok(user);
            }
            catch (Exception ex)
            {
                int statusCode = (int)HttpStatusCode.InternalServerError;
                return StatusCode(statusCode, new { code = statusCode, message = ex.Message });
            }
        }
    }
}
