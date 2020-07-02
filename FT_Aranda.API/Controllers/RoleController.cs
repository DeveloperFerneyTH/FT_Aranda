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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FT_Aranda.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        // Globals 
        private readonly IRoleWorker worker;
        private readonly IMapper mapper;
        private readonly IWritterLog log;

        public RoleController(IRoleWorker worker, IMapper mapper, IWritterLog log)
        {
            this.worker = worker;
            this.mapper = mapper;
            this.log = log;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                log.WriteLog("Role", MethodBase.GetCurrentMethod().Name, new { });
                var roles = await worker.GetRoles();
                var rolesViewModel = mapper.Map<List<Role>>(roles);

                return Ok(rolesViewModel);
            }
            catch (Exception ex)
            {
                int statusCode = (int)HttpStatusCode.InternalServerError;
                return StatusCode(statusCode, new { code = statusCode, message = ex.Message });
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            try
            {
                log.WriteLog("Role", MethodBase.GetCurrentMethod().Name, new { id = id });
                int notFountStatus = (int)HttpStatusCode.NotFound;
                var role = await worker.GetRoleByID(id);

                if (role != null) return Ok(mapper.Map<Role>(role));
                else return NotFound(new { code = notFountStatus, message = $"No se encontro nigún rol con id {id}" });
            }
            catch (Exception ex)
            {
                int statusCode = (int)HttpStatusCode.InternalServerError;
                return StatusCode(statusCode, new { code = statusCode, message = ex.Message });
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetRoleLikeName(string name)
        {
            try
            {
                log.WriteLog("Role", MethodBase.GetCurrentMethod().Name, new { name});
                int notFountStatus = (int)HttpStatusCode.NotFound;
                var role = await worker.GetRoleLikeName(name);

                if (role != null) return Ok(mapper.Map<Role>(role));
                else return NotFound(new { code = notFountStatus, message = $"No se encontro nigún rol con el nombre {name}" });
            }
            catch (Exception ex)
            {
                int statusCode = (int)HttpStatusCode.InternalServerError;
                return StatusCode(statusCode, new { code = statusCode, message = ex.Message });
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {
            try
            {
                log.WriteLog("Role", MethodBase.GetCurrentMethod().Name, model);
                int badStatusCode = (int)HttpStatusCode.BadRequest;

                if (!ModelState.IsValid)
                {
                    return BadRequest(new { code = badStatusCode, message = ModelState });
                }

                var roleExits = await worker.GetRoleByName(model.Name);

                if (roleExits != null)
                {
                    return BadRequest(new { code = badStatusCode, message = $"Encontramos el perfil {model.Name} ya creado. Intenta con otro." });
                }

                var role = mapper.Map<Role>(model);
                bool result = await worker.CreateRole(role);

                if (result) return Ok(new { code = 200, message = "Rol creado exitosamente :D" });
                else return StatusCode(500, new { code = 500, message = "Houston tenemos un problema. Por favor intenta más tarde" });
            }
            catch (Exception ex)
            {
                int statusCode = (int)HttpStatusCode.InternalServerError;
                return StatusCode(statusCode, new { code = statusCode, message = ex.Message });
            }
        }

        [HttpPatch("[action]")]
        public async Task<IActionResult> UpdateRole(RoleViewModel model)
        {
            try
            {
                log.WriteLog("Role", MethodBase.GetCurrentMethod().Name, model);
                int badStatusCode = (int)HttpStatusCode.BadRequest;

                if (!ModelState.IsValid)
                {
                    return BadRequest(new { code = badStatusCode, message = ModelState });
                }
                
                var role = mapper.Map<Role>(model);
                bool result = await worker.UpdateRole(role);

                if (result) return Ok(new { code = 200, message = "Rol actualizado exitosamente :D" });
                else return StatusCode(500, new { code = 500, message = "Houston tenemos un problema. Por favor intenta más tarde" });
            }
            catch (Exception ex)
            {
                int statusCode = (int)HttpStatusCode.InternalServerError;
                return StatusCode(statusCode, new { code = statusCode, message = ex.Message });
            }
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeletRole(int id)
        {
            try
            {
                log.WriteLog("Role", MethodBase.GetCurrentMethod().Name, new { id });
                int notFountStatus = (int)HttpStatusCode.NotFound;
                var roleDeleted = await worker.GetRoleByID(id);

                if (roleDeleted != null)
                {
                    return NotFound(new { code = notFountStatus, message = $"No se encontro nigún rol con id {id}" });
                }

                bool result = await worker.UpdateRole(roleDeleted);

                if (result) return Ok(new { code = 200, message = "Rol eliminado exitosamente :D" });
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
