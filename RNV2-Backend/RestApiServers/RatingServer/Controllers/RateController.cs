using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantDaoBase.IServices;
using RestaurantDaoBase.Models;

namespace RatingServer.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly ILogger<RateController> logger;
        private readonly IRatingService service;
        public RateController(ILogger<RateController> logger, IRatingService ratingService)
        {
            this.logger = logger;
            this.service = ratingService;
        }

        [HttpPost]
        public async Task<IActionResult> ToRestaurant([FromBody]RestRating rating) 
        {
            if (ModelState.IsValid)
            {
                var result = await service.RateToRest(rating);
                if (result == true)
                    return Ok(new AppResult("", true));
                else
                    return Ok(new AppResult("", false));
            }
            return BadRequest(new AppResult("Some properties are not correct", false));
        }

        [HttpPost]
        public async Task<IActionResult> ToMenuItem([FromBody] MenuItemRating rating)
        {
            if (ModelState.IsValid)
            {
                var result = await service.RateToMenuItem(rating);
                if (result == true)
                    return Ok(new AppResult("", true));
                else
                    return Ok(new AppResult("", false));
            }
            return BadRequest(new AppResult("Some properties are not correct", false));
        }

        [HttpPost]
        public async Task<IActionResult> ToDelivery([FromBody] DeliveryRating rating)
        {
            if (ModelState.IsValid)
            {
                var result = await service.RateToDelivery(rating);
                if (result == true)
                    return Ok(new AppResult("", true));
                else
                    return Ok(new AppResult("", false));
            }
            return BadRequest(new AppResult("Some properties are not correct", false));
        }

        [HttpGet("{restaurantId}")]
        public Task<string[]> Percentages(string restaurantId)
        {
            return service.CalculateRestRatings(restaurantId);
        }

        [HttpGet("{restaurantId}")]
        public Task<int> Total(string restaurantId)
        {
            return service.CalculateRestTotalRatings(restaurantId);
        }
    }
}
