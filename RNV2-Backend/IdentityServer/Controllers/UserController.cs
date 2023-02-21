using IdentityServer.Models;
using IdentityServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace IdentityServer.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IFileService fileService;
        private readonly IUserService userService;
        private readonly ILogger<UserController> logger;
        private readonly IConfiguration configuration;
        public UserController(ILogger<UserController> logger, IUserService userService, IFileService fileService, IConfiguration configuration)
        {
            this.logger = logger;
            this.userService = userService;
            this.fileService = fileService;
            this.configuration = configuration;
        }

        [HttpGet("{restaurantId}")]
        public Task<List<AppUser>>? ByRestaurant(string restaurantId)
        {
            return userService.ListRestaurantUsers(restaurantId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> OneById(string id)
        {
            var result = await userService.FindUserById(id);
            return Ok(result);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> OneByEmail(string email)
        {
            logger.LogInformation("Enter get user ByEmail");
            var result = await userService.FindUserByEmail(email);
            return Ok(result);
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> ByEmail(string email)
        {
            logger.LogInformation("Enter Delete ByEmail");
            AppUser user = await userService.FindUserByEmail(email);
            if (user == null)
                return Ok(new AppResult("No such a user with the email", false));
            
            if (!user.Logo.IsNullOrEmpty() )
            {
                var appResult = fileService.DeleteFile(user.Logo, configuration);
                if (!appResult.IsSuccess)
                    return Ok(appResult);
            }                

            var result = await userService.DeleteUserByEmail(email);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatedOne([FromForm]UpdateUserViewModel model)
        {
            logger.LogInformation("Enter UpdatedOne");
            if (ModelState.IsValid)
            {
                AppUser user = await userService.FindUserByEmail(model.Email);
                if(user == null) 
                    return Ok(new AppResult("No such a user with the email", false));
                if (!user.Logo.IsNullOrEmpty())
                {
                    var appResult = fileService.DeleteFile(user.Logo, configuration);
                    if (!appResult.IsSuccess)
                        return Ok(appResult);
                }
                if (model.UploadImg != null)
                    model.Logo = fileService.SaveFile(model.UploadImg, configuration);
                
                var result = await userService.UpdateUser(model);
                if (!result.IsSuccess && model.Logo != null)
                    fileService.DeleteFile(model.Logo, configuration);
                return Ok(result);
            }
            return BadRequest("Some properties are not valid");
        }

        [HttpPut]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.ResetPassword(model.Email, model.Password, model.ConfirmPassword);
                return Ok(result);
            }
            return BadRequest("Some properties are not valid");
        }
    }    
}
