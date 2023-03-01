using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantDaoBase.IServices;
using RestaurantDaoBase.Models;

namespace OrderServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> logger;
        private readonly IOrderService service;
        public OrderController(ILogger<OrderController> logger, IOrderService service)
        {
            this.logger = logger;
            this.service = service;
        }

        [HttpGet("{orderId}")]
        public Task<Order> One(string orderId)
        {
            return service.FindOrderById(orderId);
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> CanceledOne(string orderId)
        {
            if (ModelState.IsValid)
            {
                var result = await service.CancelOrder(orderId);
                if (result == true)
                    return Ok(new AppResult("", true));
            }
            return BadRequest(new AppResult("Cannot cancel the order", false));
        }

        [HttpGet]
        public Task<List<Order>> ActiveList()
        {
            return service.ListActiveOrder();
        }

        [HttpPost]
        public Task<List<Order>> ListByUserAndStatus([FromBody]UserAndStatusViewModel model)
        {
            return service.ListOrderByUserAndStatus(model.UserName, model.Status);
        }
        [HttpGet("{email}")]
        public Task<Dictionary<string, List<Order>>> ListByUser(string email)
        {
            return service.ListOrderByUser(email);
        }
        [HttpPut]
        public async Task<IActionResult> OrderStatus([FromBody]OrderAndStatusViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await service.UpdateOrderStatus(model.OrderId, model.Status);
                if (result == true)
                    return Ok(new AppResult("", true));
            }
            return BadRequest(new AppResult("Cannot create the new cart", false));
        }
        [HttpPost]
        public Task<List<Order>>? ListByRestaurantAndStatus([FromBody]RestAndStatusViewModel model)
        {
            if (ModelState.IsValid)
            {
                return service.ListOrderByStatus(model.RestaurantId, model.Status);
            }
            return null;
        }

        [HttpPost]
        public Task<List<Order>>? Search([FromBody] RestEmailAndStatusViewModel model)
        {
            if (ModelState.IsValid)
            {
                return service.SearchOrder(model.RestaurantId, model.Email,model.Status);
            }
            return null;
        }
        
        [HttpPost]
        public Task<List<Order>>? SearchWithoutStatus([FromBody] RestAndEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                return service.SearchOrder(model.RestaurantId, model.Email);
            }
            return null;
        }
        
    }
}
