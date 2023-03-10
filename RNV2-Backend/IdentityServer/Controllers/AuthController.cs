using IdentityServer.Models;
using IdentityServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [AllowAnonymous]
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
                    var result = new
                    {
                        token = token,
                        userName = appUser.Email,
                        logo = appUser.Logo,
                        role = appUser.Role,
                        restaurantId = appUser.RestaurantId,
                        restaurantName = appUser.RestaurantName
                    };
                    response = Ok(result);
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
                    model.Logo = fileService.SaveFile(model.UploadImg);
                var result = await userService.RegisterUser(model);
                if (!result.IsSuccess && model.Logo != null)
                   fileService.DeleteFile(model.Logo);
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
