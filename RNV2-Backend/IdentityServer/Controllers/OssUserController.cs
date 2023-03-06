using IdentityServer.Models;
using IdentityServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace IdentityServer.Controllers
{
    [Authorize(Roles = "Operator")]
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class OssUserController : Controller
    {
        private readonly IFileService fileService;
        private readonly IUserService userService;
        private readonly ILogger<OssUserController> logger;
        private readonly IConfiguration configuration;
        public OssUserController(ILogger<OssUserController> logger, IUserService userService, IFileService fileService, IConfiguration configuration)
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
        [HttpPost]
        public async Task<IActionResult> NewOne([FromForm] AddUserViewModel model)
        {
            logger.LogInformation("Enter add user");
            if (ModelState.IsValid)
            {
                AppResult result = await userService.AddUser(model);
                return result.IsSuccess == true ? Ok(result) : BadRequest(result);
            }
            return BadRequest(new AppResult("Some properties are not valid", false));
        }

        [HttpPut]
        public async Task<IActionResult> UpdatedOneMain([FromForm] AddUserViewModel model)
        {
            logger.LogInformation("Enter update user main");
            if (ModelState.IsValid)
            {
                var result = await userService.UpdateUserMain(model);
                return result.IsSuccess == true ? Ok(result) : BadRequest(result);
            }
            return BadRequest(new AppResult("Some properties are not valid", false));
        }

        [HttpPut]
        public async Task<IActionResult> UpdatedOneAddress([FromForm] UserAddressViewModel model)
        {
            logger.LogInformation("Enter update user's adress");
            if (ModelState.IsValid)
            {
                var result = await userService.UpdateUserAddress(model);
                return result.IsSuccess == true ? Ok(result) : BadRequest(result);
            }
            return BadRequest(new AppResult("Some properties are not valid", false));
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> ByEmail(string email)
        {
            logger.LogInformation("Enter Delete ByEmail");
            AppUser user = await userService.FindUserByEmail(email);
            if (user == null)
                return Ok(new AppResult("No such a user with the email", false));

            if (!user.Logo.IsNullOrEmpty())
            {
                var appResult = fileService.DeleteFile(user.Logo);
                if (!appResult.IsSuccess)
                    return Ok(appResult);
            }

            var result = await userService.DeleteUserByEmail(email);
            return Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Logo(string id, IFormFile file)
        {
            logger.LogInformation($"Enter OssUserController.Logo id={id}");
            AppUser oldOne = await userService.FindUserById(id);
            if (oldOne == null)
                return BadRequest(new AppResult($"No user(id={id}", false));
            if (oldOne.Logo != null)
            {
                fileService.DeleteFile(oldOne.Logo);
            }

            if (file != null)
            {
                try
                {
                    oldOne.Logo = fileService.SaveFile(file);
                }
                catch (Exception ex)
                {
                    logger.LogInformation(ex.Message);
                    return BadRequest(new AppResult("User Logo : The file cannot be saved", false));
                }
            }

            var result = await userService.UpdateUserLogo(oldOne.Email,oldOne.Logo);
            if (result.IsSuccess)
            {
                return Ok(new AppResult(oldOne.Logo, true));
            }
            logger.LogInformation("OssUserController.Logo failed ");
            if (oldOne.Logo != null)
                fileService.DeleteFile(oldOne.Logo);
            return BadRequest(new AppResult("", false));
        }

        /*
        [HttpGet]
        public Task<List<AppUser>>? AvaliableDeliveryMan()
        {
            return userService.AvaliableDeliveryManList();
        }
        */
    }
}
