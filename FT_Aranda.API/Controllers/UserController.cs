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
    public class UserController : ControllerBase
    {
        // Globals 
        private readonly IUserWorker worker;
        private readonly IMapper mapper;
        private readonly IWritterLog log;

        public UserController(IUserWorker worker, IMapper mapper, IWritterLog log)
        {
            this.worker = worker;
            this.mapper = mapper;
            this.log = log;
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                log.WriteLog("User", MethodBase.GetCurrentMethod().Name, new { });
                var Users = await worker.GetUsers();
                var UsersViewModel = mapper.Map<List<User>>(Users);

                return Ok(UsersViewModel);
            }
            catch (Exception ex)
            {
                int statusCode = (int)HttpStatusCode.InternalServerError;
                return StatusCode(statusCode, new { code = statusCode, message = ex.Message });
            }
        }

        [HttpGet("[action]/{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                log.WriteLog("User", MethodBase.GetCurrentMethod().Name, new { id = id });
                int notFountStatus = (int)HttpStatusCode.NotFound;
                var User = await worker.GetUserByID(id);

                if (User != null) return Ok(mapper.Map<User>(User));
                else return NotFound(new { code = notFountStatus, message = $"No se encontro nigún usuario con id {id}" });
            }
            catch (Exception ex)
            {
                int statusCode = (int)HttpStatusCode.InternalServerError;
                return StatusCode(statusCode, new { code = statusCode, message = ex.Message });
            }
        }

        [HttpGet("[action]/{firtsName}")]
        [Authorize]
        public async Task<IActionResult> GetUserLikeFirtsName(string firtsName)
        {
            try
            {
                log.WriteLog("User", MethodBase.GetCurrentMethod().Name, new { firtsName });
                int notFountStatus = (int)HttpStatusCode.NotFound;
                var User = await worker.GetUserLikeFirtsName(firtsName);

                if (User != null) return Ok(mapper.Map<User>(User));
                else return NotFound(new { code = notFountStatus, message = $"No se encontro nigún usuario con el nombre {firtsName}" });
            }
            catch (Exception ex)
            {
                int statusCode = (int)HttpStatusCode.InternalServerError;
                return StatusCode(statusCode, new { code = statusCode, message = ex.Message });
            }
        }

        [HttpGet("[action]/{lastName}")]
        [Authorize]
        public async Task<IActionResult> GetUserLikeLastName(string lastName)
        {
            try
            {
                log.WriteLog("User", MethodBase.GetCurrentMethod().Name, new { lastName });
                int notFountStatus = (int)HttpStatusCode.NotFound;
                var User = await worker.GetUserLikeLastName(lastName);

                if (User != null) return Ok(mapper.Map<User>(User));
                else return NotFound(new { code = notFountStatus, message = $"No se encontro nigún usuario con el apellido {lastName}" });
            }
            catch (Exception ex)
            {
                int statusCode = (int)HttpStatusCode.InternalServerError;
                return StatusCode(statusCode, new { code = statusCode, message = ex.Message });
            }
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "Administrador, Editor")]
        public async Task<IActionResult> CreateUser(UserViewModel model)
        {
            try
            {
                log.WriteLog("User", MethodBase.GetCurrentMethod().Name, model);
                int badStatusCode = (int)HttpStatusCode.BadRequest;

                if (!ModelState.IsValid)
                {
                    return BadRequest(new { code = badStatusCode, message = ModelState });
                }

                var User = mapper.Map<User>(model);
                bool result = await worker.CreateUser(User);

                if (result) return Ok(new { code = 200, message = "Usuario creado exitosamente :D" });
                else return StatusCode(500, new { code = 500, message = "Houston tenemos un problema. Por favor intenta más tarde" });
            }
            catch (Exception ex)
            {
                int statusCode = (int)HttpStatusCode.InternalServerError;
                return StatusCode(statusCode, new { code = statusCode, message = ex.Message });
            }
        }

        [HttpPatch("[action]")]
        [Authorize(Roles = "Administrador, Editor")]
        public async Task<IActionResult> UpdateUser(UserViewModel model)
        {
            try
            {
                log.WriteLog("User", MethodBase.GetCurrentMethod().Name, model);
                int badStatusCode = (int)HttpStatusCode.BadRequest;

                if (!ModelState.IsValid)
                {
                    return BadRequest(new { code = badStatusCode, message = ModelState });
                }

                var User = mapper.Map<User>(model);
                bool result = await worker.UpdateUser(User);

                if (result) return Ok(new { code = 200, message = "Usuario actualizado exitosamente :D" });
                else return StatusCode(500, new { code = 500, message = "Houston tenemos un problema. Por favor intenta más tarde" });
            }
            catch (Exception ex)
            {
                int statusCode = (int)HttpStatusCode.InternalServerError;
                return StatusCode(statusCode, new { code = statusCode, message = ex.Message });
            }
        }

        [HttpDelete("[action]")]
        [Authorize(Roles = "Administrador, Editor")]
        public async Task<IActionResult> DeletUser(int id)
        {
            try
            {
                log.WriteLog("User", MethodBase.GetCurrentMethod().Name, new { id });
                int notFountStatus = (int)HttpStatusCode.NotFound;
                var UserDeleted = await worker.GetUserByID(id);

                if (UserDeleted != null)
                {
                    return NotFound(new { code = notFountStatus, message = $"No se encontro nigún usuario con id {id}" });
                }

                bool result = await worker.UpdateUser(UserDeleted);

                if (result) return Ok(new { code = 200, message = "Usuario eliminado exitosamente :D" });
                else return StatusCode(500, new { code = 500, message = "Houston tenemos un problema. Por favor intenta más tarde" });
            }
            catch (Exception ex)
            {
                int statusCode = (int)HttpStatusCode.InternalServerError;
                return StatusCode(statusCode, new { code = statusCode, message = ex.Message });
            }
        }
    }
}
