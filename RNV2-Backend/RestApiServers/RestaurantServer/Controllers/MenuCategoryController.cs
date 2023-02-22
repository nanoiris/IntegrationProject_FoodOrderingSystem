using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantDaoBase.IServices;
using RestaurantDaoBase.Models;
using RestaurantServer.Services;

namespace RestaurantServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuCategoryController : ControllerBase
    {
        private readonly ILogger<MenuCategoryController> logger;
        private readonly IMenuService service;
        private readonly IFileService fileService;
        public MenuCategoryController(ILogger<MenuCategoryController> logger, IMenuService service, IFileService fileService)
        {
            this.logger = logger;
            this.service = service;
            this.fileService = fileService;
        }

        [HttpGet("{restaurantId}")]
        public Task<List<MenuCategory>> List(string restaurantId)
        {
            return service.ListCategory(restaurantId);
        }

        [HttpGet("{restaurantId}")]
        public Task<List<MenuItem>?> FeaturedList(string restaurantId)
        {
            return service.GetFeaturedMenus(restaurantId);
        }

        [HttpGet("{id}")]
        public Task<MenuCategory?> One(string id)
        {
            return service.FindCategory(id);
        }

        [HttpPost]
        public async Task<IActionResult> NewOne([FromBody] MenuCategory category)
        {
            if (ModelState.IsValid)
            {
                var result = await service.AddCategory(category);
                if (result == true)
                    return Ok(new AppResult("", true));
            }
            return BadRequest(new AppResult("Cannot add the new category",false));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletedOne(string id)
        {
            if (ModelState.IsValid)
            {
                var result = await service.DeleteCategory(id);
                if (result == true)
                    return Ok(new AppResult("", true));
            }
            return BadRequest(new AppResult("Cannot delete the category", false));
        }

        /*
         *
         public Task<bool> UpdateCategory(MenuCategory category);
         public Task<bool> DeleteCategory(string categoryId);
        */

    }
}
