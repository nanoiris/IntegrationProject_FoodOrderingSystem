using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantDaoBase.IServices;
using RestaurantDaoBase.Models;
using RestaurantServer.Services;
using System.Data;

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
        [AllowAnonymous]
        [HttpGet("{restaurantId}")]
        public Task<List<MenuCategory>> List(string restaurantId)
        {
            return service.ListCategory(restaurantId);
        }
        [AllowAnonymous]
        [HttpGet("{restaurantId}")]
        public Task<List<MenuItem>?> FeaturedList(string restaurantId)
        {
            return service.GetFeaturedMenus(restaurantId);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public Task<MenuCategory?> One(string id)
        {
            return service.FindCategory(id);
        }
        [Authorize(Roles = "Restaurant")]
        [HttpPost]
        public async Task<IActionResult> NewOne([FromBody] MenuCategory category)
        {
            if (ModelState.IsValid)
            {
                var result = await service.AddCategory(category);
                if (result == true)
                    return Ok(new AppResult("", true));
            }
            return BadRequest(new AppResult("Cannot add the new category", false));
        }
        [Authorize(Roles = "Restaurant")]
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
        [Authorize(Roles = "Restaurant")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatedOne(MenuCategory category)
        {
            if (ModelState.IsValid)
            {
                var result = await service.UpdateCategory(category);
                if (result == true)
                    return Ok(new AppResult("", true));
            }
            return BadRequest(new AppResult("Cannot update the category", false));
        }
    }
}
