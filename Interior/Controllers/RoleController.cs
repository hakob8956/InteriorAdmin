using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interior.Enums;
using Interior.Models.Interface;
using Interior.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Interior.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet("get-roles")]
        public async Task<IActionResult> GetRoles()
        {
            return  Ok(ResponseSuccess.Create(await _roleService.GetAllRolesAsync()));
        }
    }
}