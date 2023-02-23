using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantDaoBase.IServices;
using RestaurantDaoBase.Models;

namespace OrderServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> logger;
        private readonly IOrderService service;
        public CartController(ILogger<CartController> logger, IOrderService service)
        {
            this.logger = logger;
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> NewOne([FromBody]CreateCartViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await service.CreateCart(model.Email,model.MenuItem,model.RestaurantId,model.RestaurantName);
                if (result == true)
                    return Ok(new AppResult("", true));
            }
            return BadRequest(new AppResult("Cannot create the new cart", false));
        }

        [HttpPut("{orderId}")]
        public async Task<IActionResult> DishAddedToCart(string orderId,[FromBody]MenuItem menuItem)
        {
            if (ModelState.IsValid)
            {
                var result = await service.AddDishToCart(orderId, menuItem);
                if (result == true)
                    return Ok(new AppResult("", true));
            }
            return BadRequest(new AppResult("Cannot create the new cart", false));
        }

        [HttpPut("{orderId}/{orderItemId}")]
        public async Task<IActionResult> DecreaseQty(string orderId, string orderItemId)
        {
            if (ModelState.IsValid)
            {
                var result = await service.DecreaseDishQty(orderItemId, orderId);
                if (result == true)
                    return Ok(new AppResult("", true));
            }
            return BadRequest(new AppResult("Cannot create the new cart", false));
        }

        [HttpPut("{orderId}/{orderItemId}")]
        public async Task<IActionResult> IncreaseQty(string orderId, string orderItemId)
        {
            if (ModelState.IsValid)
            {
                var result = await service.IncreaseDishQty(orderItemId, orderId);
                if (result == true)
                    return Ok(new AppResult("", true));
            }
            return BadRequest(new AppResult("Cannot create the new cart", false));
        }

        [HttpPut("{orderId}")]
        public async Task<IActionResult> Payment(string orderId, [FromBody]PayCard card)
        {
            if (ModelState.IsValid)
            {
                var result = await service.PayCart(orderId, card);
                if (result == true)
                    return Ok(new AppResult("", true));
            }
            return BadRequest(new AppResult("Cannot create the new cart", false));
        }
    }
}
