using IdentityServer.Models;
using IdentityServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace IdentityServer.Controllers
{
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

        [HttpPost]
        public IActionResult AddOne([FromForm] AddRoleViewModel model)
        {
            logger.LogInformation("Enter add role");
            if (ModelState.IsValid)
            {
                return Ok(roleService.AddRole(model.Name));
            }
            return BadRequest("Some properties are not valid");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(string id)
        {
            logger.LogInformation("Enter Delete Role ById");
            var result = roleService.DeleteRole(id);
            return Ok(result);
        }
    }    
}
