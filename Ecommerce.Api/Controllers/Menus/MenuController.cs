using AutoMapper;
using Ecommerce.BLL.Abstraction.Identity;
using Ecommerce.BLL.Abstraction.Menu;
using Ecommerce.Models.CriteriaDto.Role;
using Ecommerce.Models.Request.Menus;
using Ecommerce.Models.ReturnDto.Menu;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Ecommerce.Api.Controllers.menues
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuManager _menuManager;
        private readonly IRoleMenuManager _roleMenuManger;
        IMapper _mapper;
        //private readonly ICodeGenerationManager _codeGenerationManager;

        private readonly IUserRoleManager _userRoleManager;
        public MenuController(IUserRoleManager userRoleManager, IMenuManager menuManager, IMapper mapper, IRoleMenuManager roleMenuManager)
        {
            _menuManager = menuManager;
            _mapper = mapper;
            _roleMenuManger = roleMenuManager;

            //_codeGenerationManager = codeGenerationManager;

            _userRoleManager = userRoleManager;

        }

        [HttpGet("getPermitedMenu/{role}")]
        public async Task<IActionResult> GetMenuList(string role)
        {
            var result = await _menuManager.GetMenuList(role);
            return Ok(result);
        }

        [HttpGet("getPermitedMenuByRoles")]
        public async Task<IActionResult> GetPermitedMenuByRoles([FromQuery] RoleList roles)
        {
            if (roles != null)
            {
                var result = await _menuManager.GetPermitedMenuByRoles(roles.RoleNames);

                var ff = _mapper.Map<IList<MenuReturnDto>>(result);
                

                return Ok(result);
            }
            else
            {
                return NotFound("Not found");
            }           
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _menuManager.GetAll();
            return Ok(result);
        }

        [HttpPut("menuPermission")]
        public async Task<IActionResult> MenuPermission([FromBody] RoleMenuPermissionDto model)
        {
            if (ModelState.IsValid)
            {
               
               var userIds = await _userRoleManager.GetUserIdsByRole(model.RoleName);
                //var clientId = Request.Headers["ClientId"].ToString();
                var result = await _roleMenuManger.AddOrUpdate(model);
              
                return Ok(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet("getPermitedMenuIds")]
        public async Task<IActionResult> GetMenuIdsAsync(string roleName)
        {
            var request = Request.Headers;
            var result = await _roleMenuManger.GetPermitedMenuIds(roleName);

            return Ok(result);
        }

        [HttpGet("getTopMenu")]
        public async Task<IActionResult> GetTop()
        {
            var result = await _menuManager.GetTopMenu();
            return Ok(result);
        }

        

        

      
    }
}
