using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantDaoBase.IServices;
using RestaurantDaoBase.Models;
using RestaurantServer.Services;

namespace RestaurantServer.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LogoController : Controller
    {
        private readonly ILogger<RestaurantController> logger;
        private readonly IRestaurantService restService;
        private readonly IMenuService menuService;
        private readonly IFileService fileService;
        public LogoController(ILogger<RestaurantController> logger,
            IRestaurantService restService,
            IMenuService menuService,
             IFileService fileService)
        {
            this.logger = logger;
            this.restService = restService;
            this.menuService = menuService;
            this.fileService = fileService;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> RestCategory(string id, IFormFile file)
        {
            logger.LogInformation($"Enter LogoController.RestCategory id={id}");
            RestCategory? oldOne = await restService.FindCategory(id);
            if (oldOne == null)
                return BadRequest(new AppResult($"No rest category(id={id}", false));
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
                    return BadRequest(new AppResult("RestCategoryLogo : The file cannot be saved", false));
                }
            }

            var result = await restService.UpdateCategory(oldOne);
            if (result == true)
            {
                return Ok(new AppResult(oldOne.Logo, true));
            }
            logger.LogInformation("service.RestCategoryLogo failed ");
            if (oldOne.Logo != null)
                fileService.DeleteFile(oldOne.Logo);
            return BadRequest(new AppResult("", false));
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Restaurant(string id, IFormFile file)
        {
            logger.LogInformation($"Enter LogoController.Restaurant id={id}");
            Restaurant? oldOne = await restService.FindRestaurant(id);
            if (oldOne == null)
                return BadRequest(new AppResult($"No restaurant(id={id}", false));
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
                    return BadRequest(new AppResult("Restaurant Logo : The file cannot be saved", false));
                }
            }
            if (oldOne.Logo == null)
            {
                logger.LogInformation("Restaurant Logo : The logo file name is null");
                return BadRequest(new AppResult("Restaurant Logo : The logo file name is null", false));
            }
             var result = await restService.UpdateRestaurantLogo(oldOne);
            if (result == true)
            {
                return Ok(new AppResult(oldOne.Logo, true));
            }
            logger.LogInformation("service.Restaurant failed ");
            if (oldOne.Logo != null)
                fileService.DeleteFile(oldOne.Logo);
            return BadRequest(new AppResult("", false));
        }
    }
}
