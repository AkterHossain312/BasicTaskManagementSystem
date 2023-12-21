using Application.Interface;
using Application.Queries;
using Application.RequestModel;
using Application.ResponseModels;
using Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [AllowAnonymous]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] RoleRequestModel model)
        {
            await _roleService.CreateRole(model);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] BaseQuery model)
        {
            PagedResponse<RoleModel> data = await _roleService.GetRoles(model);
            return Ok(data);
        }

        
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            RoleModel data = await _roleService.GetRoleById(id);
            return Ok(data);
        }

        
        [HttpGet("Update")]
        public async Task<IActionResult> Update([FromQuery] RoleRequestModel model)
        {
            await _roleService.Update(model);
            return Ok();
        }

        
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _roleService.Delete(id);
            return Ok();
        }
    }
}
