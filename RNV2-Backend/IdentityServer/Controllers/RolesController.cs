using IdentityServer.Models;
using IdentityServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace IdentityServer.Controllers
{
    [Authorize(Roles ="Operator")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ILogger<RolesController> logger;
        private readonly IRoleService roleService;
        public RolesController(ILogger<RolesController> logger, IRoleService roleService)
        {
            this.logger = logger;
            this.roleService = roleService;
        }

        [HttpGet]
        public List<IdentityRole> List()
        {
            return roleService.ListRole();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var result = roleService.FindById(id);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult AddOne([FromForm] AddRoleViewModel model)
        {
            logger.LogInformation("Enter add role");
            if (ModelState.IsValid)
            {
               AppResult result = roleService.AddRole(model.Name);
                if (result.IsSuccess)
                    return Ok(result);
                else
                    return BadRequest(result);   
            }
            return BadRequest(new AppResult("Some properties are not valid", false));
        }

        [HttpPut]
        public IActionResult UpdateOne([FromForm] UpdateRoleViewModel model)
        {
            logger.LogInformation("Enter update role");
            if (ModelState.IsValid)
            {
                var result = roleService.UpdateRole(model.Id, model.Name);

                return Ok(roleService.UpdateRole(model.Id,model.Name));
            }
            return BadRequest(new AppResult("Some properties are not valid",false));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(string id)
        {
            logger.LogInformation("Enter Delete Role ById");
            var result = roleService.DeleteRole(id);
            if(result.IsSuccess == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }    
}
