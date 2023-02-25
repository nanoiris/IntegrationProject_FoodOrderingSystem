using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantDaoBase.Enums;
using RestaurantDaoBase.IServices;
using RestaurantDaoBase.Models;

namespace DeliveryServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly ILogger<DeliveryController> logger;
        private readonly IDeliveryService service;
        public DeliveryController(ILogger<DeliveryController> logger, IDeliveryService service)
        {
            this.logger = logger;
            this.service = service;
        }

        [HttpGet("{id}")]
        public Task<Delivery> One(string id)
        {
            return service.FindDelivery(id);
        }

        [HttpGet("{deliveryMan}/{status}")]
        public Task<List<Delivery>> ByDeliveryManAndStatus(string deliveryMan,DeliveryStatusEnum status)
        {
            return service.ListDeliveriesByStatus(deliveryMan, status);
        }

        [HttpGet("{status}")]
        public Task<List<Delivery>> ByStatus(DeliveryStatusEnum status)
        {
            return service.ListDeliveriesByStatus(status);
        }

        [HttpGet]
        public Task<List<Delivery>> ActiveList()
        {
            return service.ListActiveDeliveries();
        }

        [HttpGet("{restaurantId}/{email}/{status}")]
        public Task<List<Delivery>> Search(string restaurantId, string email, DeliveryStatusEnum status)
        {
            return service.Search(restaurantId,email, status);
        }

        [HttpGet("{restaurantId}/{email}")]
        public Task<List<Delivery>> SearchWithoutStatus(string restaurantId, string email)
        {
            return service.Search(restaurantId, email);
        }

        [HttpPost]
        public async Task<IActionResult> NewOne([FromBody]Delivery model)
        {
            if (ModelState.IsValid)
            {
                var result = await service.AddDelivery(model);
                if (result == true)
                    return Ok(new AppResult("", true));
            }
            return BadRequest(new AppResult("Cannot create the new delivery", false));
        }

        [HttpPut("{id}/{status}")]
        public async Task<IActionResult> DeliveryStatus(string id,DeliveryStatusEnum status)
        {
            var result = await service.UpdateDeliveryStatus(id, status);
            return Ok(new AppResult("", result));
        }

        [HttpPut("{id}/{deliveryMan}")]
        public async Task<IActionResult> Accept(string id, string deliveryMan)
        {
            var result = await service.Accept(id,deliveryMan);
            return Ok(new AppResult("", result));
        }

        [HttpPut]
        public async Task<IActionResult> Assign([FromBody] AssignForm form)
        {
            if (ModelState.IsValid)
            {
                var result = await service.Assign(form);
                return Ok(new AppResult("", result));
            }
            return BadRequest(new AppResult("Cannot create the new delivery", false));
        }

        [HttpPut("{id}/{deliveryMan}")]
        public async Task<IActionResult> Reject(string id, string deliveryMan)
        {
            var result = await service.Reject(id, deliveryMan);
            return Ok(new AppResult("", result));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Complete(string id)
        {
            var result = await service.Complete(id);
            return Ok(new AppResult("", result));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Pending(string id)
        {
            var result = await service.Pending(id);
            return Ok(new AppResult("", result));
        }
    }
}
