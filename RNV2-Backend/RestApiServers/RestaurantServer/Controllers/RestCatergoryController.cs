using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantDaoBase.IServices;
using RestaurantDaoBase.Models;

namespace RestaurantServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RestCatergoryController : ControllerBase
    {
        private readonly ILogger<RestCatergoryController> logger;
        private readonly IRestaurantService service;
        public RestCatergoryController(ILogger<RestCatergoryController> logger, IRestaurantService service)
        {
            this.logger = logger;
            this.service = service;
        }

        [HttpGet]
        public Task<List<RestCategory>> List()
        {
             return service.ListCategory();
        }

        [HttpPost]
        public async Task<IActionResult> NewOne([FromBody] RestCategory category)
        {
            if (ModelState.IsValid)
            {
                var result = await service.AddCategory(category);
                if (result == true)
                    return Ok(new AppResult("", true));
                else
                    return Ok(new AppResult("", false));
            }
            return BadRequest(new AppResult("Some properties are not correct", false));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> One(string id)
        {
            if (ModelState.IsValid)
            {
                var result = await service.FindCategory(id);
                if (result == true)
                    return Ok(new AppResult("", true));
                else
                    return Ok(new AppResult("", false));
            }
            return BadRequest(new AppResult("Some properties are not correct", false));
        }

        [HttpPut]
        public async Task<IActionResult> UpdatedOne([FromBody] RestCategory category)
        {
            if (ModelState.IsValid)
            {
                var result = await service.AddCategory(category);
                if (result == true)
                    return Ok(new AppResult("", true));
                else
                    return Ok(new AppResult("", false));
            }
            return BadRequest(new AppResult("Some properties are not correct", false));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletedOne(string id)
        {
            if (ModelState.IsValid)
            {
                var result = await service.AddCategory(category);
                if (result == true)
                    return Ok(new AppResult("", true));
                else
                    return Ok(new AppResult("", false));
            }
            return BadRequest(new AppResult("Some properties are not correct", false));
        }
    }
}