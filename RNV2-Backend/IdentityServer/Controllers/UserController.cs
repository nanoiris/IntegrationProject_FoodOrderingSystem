﻿using IdentityServer.Models;
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
                    var appResult = fileService.DeleteFile(user.Logo);
                    if (!appResult.IsSuccess)
                        return Ok(appResult);
                }
                if (model.UploadImg != null)
                    model.Logo = fileService.SaveFile(model.UploadImg);
                
                var result = await userService.UpdateUser(model);
                if (!result.IsSuccess && model.Logo != null)
                    fileService.DeleteFile(model.Logo);
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
                return result.IsSuccess == true ? Ok(result) : BadRequest(result);
            }
            return BadRequest("Some properties are not valid");
        }
    }    
}
