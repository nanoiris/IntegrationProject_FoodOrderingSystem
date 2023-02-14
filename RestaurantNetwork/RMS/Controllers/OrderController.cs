using Microsoft.AspNetCore.Mvc;
using RMS.Models.Order;
using RestaurantDao.IServices;
using RestaurantDao.Models;
using RestaurantDao.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using RMS.Models;
using Microsoft.Extensions.Logging;

namespace RMS.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> logger;
        private readonly IRmsService service;

        public OrderController(ILogger<OrderController> logger, IRmsService service)
        {
            this.logger = logger;
            this.service = service;
        }

        public IActionResult Index()
        {
            ListViewModel model = new ListViewModel();
            var restaurantId = Int32.Parse(HttpContext.Session.GetString("RestaurantId"));
            string message = Request.Query["message"].ToString();
            model.Message = message;
            model.EditBtns = new[] {
                new RoutePath { ControllerName = "Order", ActionName = "Accept", Titile = "Accept" },
                new RoutePath { ControllerName = "Order", ActionName = "Cancel", Titile = "Cancel" }
            };
            model.Rows = service.ListOrderByStatus(restaurantId, StatusEnum.Paid);
            return View(model);
        }

        public IActionResult Search(ListViewModel? model)
        {
            var restaurantId = Int32.Parse(HttpContext.Session.GetString("RestaurantId"));
            switch (model.SearchStatus)
            {
                case StatusEnum.Paid:
                    model.EditBtns = new[] {
                       new RoutePath { ControllerName = "Order", ActionName = "Accept", Titile = "Accept" },
                       new RoutePath { ControllerName = "Order", ActionName = "Cancel", Titile = "Cancel" }
                    };
                    break;
                case StatusEnum.Accepted:
                    model.EditBtns = new[] {
                       new RoutePath { ControllerName = "Order", ActionName = "Ready", Titile = "Ready" },
                       new RoutePath { ControllerName = "Order", ActionName = "Cancel", Titile = "Cancel" }
                    };
                    break;
                case StatusEnum.Ready:
                    model.EditBtns = new[] {
                       new RoutePath { ControllerName = "Order", ActionName = "Complete", Titile = "Complete" },
                       new RoutePath { ControllerName = "Order", ActionName = "Cancel", Titile = "Cancel" }
                    };
                    break;
                case StatusEnum.Completed:
                case StatusEnum.Canceled:
                    model.EditBtns = null;
                    break;
            }
            model.Rows = service.SearchOrder(restaurantId, model.SearchKey, model.SearchStatus);
            return View("Index", model);
        }

        [HttpGet]
        public IActionResult Cancel(int id)
        {
            ListViewModel model = new ListViewModel();
            model.Message = $"The order({id}) is canceled!";
            var restaurantId = Int32.Parse(HttpContext.Session.GetString("RestaurantId"));
            service.CancelOrder(restaurantId, id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Accept(int id)
        {
            var restaurantId = Int32.Parse(HttpContext.Session.GetString("RestaurantId"));
            service.UpdateOrderStatus(restaurantId, id, StatusEnum.Accepted);
            TempData["SuccessMessage"] = "Accept the order successfully!";
            return RedirectToAction("Index", "Order");

        }

        [HttpGet]
        public IActionResult Ready(int id)
        {
            var restaurantId = Int32.Parse(HttpContext.Session.GetString("RestaurantId"));
            service.UpdateOrderStatus(restaurantId, id, StatusEnum.Ready);
            TempData["SuccessMessage"] = "Order is ready!";
            return RedirectToAction("Index", "Order");
        }

        [HttpGet]
        public IActionResult Complete(int id)
        {
            var restaurantId = Int32.Parse(HttpContext.Session.GetString("RestaurantId"));
            service.UpdateOrderStatus(restaurantId, id, StatusEnum.Completed);
            TempData["SuccessMessage"] = "Order is complete successfully!";
            return RedirectToAction("Index", "Order");
        }
        [HttpGet]
        public IActionResult Detail(int id,int restId)
        {
            logger.LogInformation(id.ToString());
            logger.LogInformation($"{restId}");

            Order order = service.FindOrderById(restId,id);
            DetailViewModel model = new DetailViewModel();
            model.Row = order;
            if (order == null)
            {
                model.Message = "There are no items in the order!";
            }
            return View(model);
        }
    }
}
