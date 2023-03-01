using Microsoft.AspNetCore.Mvc;
using RestaurantDaoBase.IServices;
using RestaurantDaoBase.Models;
using RestaurantServer.Services;

namespace RestaurantServer.Controllers
{
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
            if(oldOne.Logo != null)
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
    }
}
