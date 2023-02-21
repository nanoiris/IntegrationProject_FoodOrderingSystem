using IdentityServer.Models;
using IdentityServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Runtime.CompilerServices;

namespace IdentityServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService tokenService;
        private readonly IFileService fileService;
        private readonly IUserService userService;
        private readonly ILogger<AuthController> logger;
        private readonly IConfiguration configuration;
        public AuthController(ILogger<AuthController> logger, ITokenService tokenService,IUserService userService,IFileService fileService,IConfiguration configuration) 
        {
            this.logger = logger;
            this.tokenService = tokenService;
            this.userService = userService;
            this.fileService = fileService;
            this.configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> LoginCookie([FromForm] LoginViewModel model)
        {
            IActionResult response = Unauthorized();

            if (ModelState.IsValid)
            {
                var appUser = await userService.Login(model);
                if (appUser != null)
                {
                    string token = tokenService.GenerateToken(appUser, configuration);
                    Response.Cookies.Append("Authorization", token);
                    response = Ok();
                }
            }
            return response;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            IActionResult response = Unauthorized();
            if (ModelState.IsValid)
            {
                var appUser = await userService.Login(model);
                if (appUser != null)
                {
                    string token = tokenService.GenerateToken(appUser, configuration);
                    response = Ok(new { token = token });
                }
            }
            return response;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromForm]RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.UploadImg != null)
                    model.Logo = fileService.SaveFile(model.UploadImg, configuration);
                var result = await userService.RegisterUser(model);
                if (!result.IsSuccess && model.Logo != null)
                   fileService.DeleteFile(model.Logo, configuration);
                return Ok(result);
            }
            return BadRequest("Some properties are not valid");
        }
        
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await userService.Logout();
            return Ok();
        }
    }
}
