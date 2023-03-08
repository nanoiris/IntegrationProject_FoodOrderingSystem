using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantDaoBase.Enums;
using RestaurantDaoBase.IServices;
using RestaurantDaoBase.Models;

namespace DeliveryServer.Controllers
{
    [Authorize]
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

        [HttpGet("{id}")]
        public Task<Delivery?> OneByOrderId(string id)
        {
            return service.FindByOrderId(id);
        }

        [HttpGet("{deliveryMan}/{status}")]
        public Task<List<Delivery>> ByDeliveryManAndStatus(string deliveryMan, DeliveryStatusEnum status)
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
            logger.LogInformation("Enter ActiveList");
            return service.ListActiveDeliveries();
        }

        [HttpGet("{restaurantId}/{email}/{status}")]
        public Task<List<Delivery>> Search(string restaurantId, string email, DeliveryStatusEnum status)
        {
            return service.Search(restaurantId, email, status);
        }

        [HttpGet("{restaurantId}/{email}")]
        public Task<List<Delivery>> SearchWithoutStatus(string restaurantId, string email)
        {
            return service.Search(restaurantId, email);
        }

        [HttpPost]
        public async Task<IActionResult> NewOne([FromBody] Delivery model)
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
        public async Task<IActionResult> DeliveryStatus(string id, DeliveryStatusEnum status)
        {
            var result = await service.UpdateDeliveryStatus(id, status);
            if (result == true)
                return Ok(new AppResult(id, true));
            return BadRequest(new AppResult("Cannot change the delivery status", false));
        }

        [HttpPut("{id}/{deliveryMan}")]
        public async Task<IActionResult> Accept(string id, string deliveryMan)
        {
            var result = await service.Accept(id, deliveryMan);
            if (result == true)
                return Ok(new AppResult(id, true));
            return BadRequest(new AppResult("Cannot accept the delivery", false));
        }

        [HttpPut]
        public async Task<IActionResult> Assign([FromBody] AssignForm form)
        {
            logger.LogInformation($"Enter Assign {form.Id}");
            if (ModelState.IsValid)
            {
                var result = await service.Assign(form);
                if (result == true)
                {
                    logger.LogInformation($"Assign {form.Id} successfully");
                    return Ok(new AppResult(form.Id, true));
                }
            }
            return BadRequest(new AppResult("Cannot assign the delivery", false));
        }

        [HttpPut("{id}/{deliveryMan}")]
        public async Task<IActionResult> Reject(string id, string deliveryMan)
        {
            var result = await service.Reject(id, deliveryMan);
            if (result == true)
                return Ok(new AppResult(id, true));
            return BadRequest(new AppResult("Cannot reject the delivery", false));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Pickup(string id)
        {
            var result = await service.Pickup(id);
            if (result == true)
                return Ok(new AppResult(id, true));
            return BadRequest(new AppResult("Cannot pickup the delivery", false));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Complete(string id)
        {
            var result = await service.Complete(id);
            if (result == true)
                return Ok(new AppResult(id, true));
            return BadRequest(new AppResult("Cannot complet the delivery", false));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Pending(string id)
        {
            var result = await service.Pending(id);
            if (result == true)
                return Ok(new AppResult(id, true));
            return BadRequest(new AppResult("Cannot pending the delivery", false));
        }
    }
}
