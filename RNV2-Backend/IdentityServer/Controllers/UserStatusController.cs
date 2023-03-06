using IdentityServer.Models;
using IdentityServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IdentityServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserStatusController : Controller
    {
        private readonly ITokenService tokenService;
        private readonly IUserService userService;
        private readonly ILogger<UserStatusController> logger;
        public UserStatusController(ILogger<UserStatusController> logger, ITokenService tokenService, IUserService userService)
        {
            this.logger = logger;
            this.tokenService = tokenService;
            this.userService = userService;
        }
        [HttpPut("{userName}")]
        public async Task<IActionResult> OnDuty(string userName)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.UpdateUserStatus(userName, UserStatusEnum.active);
                return result.IsSuccess == true ? (Ok(result)) : BadRequest(result);
            }
            return BadRequest(new AppResult("Some properties are not valid", false));
        }

        [HttpPut("{userName}")]
        public async Task<IActionResult> Terminated(string userName)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.UpdateUserStatus(userName, UserStatusEnum.inactive);
                return result.IsSuccess == true ? (Ok(result)) : BadRequest(result);
            }
            return BadRequest(new AppResult("Some properties are not valid", false));
        }

        [HttpPut("{userName}/{status}")]
        public async Task<IActionResult> UpdatedOne(string userName,UserStatusEnum status)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.UpdateUserStatus(userName,status);
                return result.IsSuccess == true ? (Ok(result)) : BadRequest(result);
            }
            return BadRequest(new AppResult("Some properties are not valid",false));
        }
    }
}
