using Microsoft.AspNetCore.Mvc;
using OSS.Models.Order;
using RestaurantDao.IServices;
using RestaurantDao.Models;

namespace OSS.Controllers
{
    public class OrderController : BaseController
    {
        public OrderController(ILogger<OrderController> logger, IOssService service) : base(logger, service) { }

        public IActionResult Index()
        {
            ListViewModel model = new ListViewModel();
            string message = Request.Query["message"].ToString();
            model.Message = message;
            model.Rows = service.ListActiveOrder();
            return View(model);
        }
        public IActionResult Search(ListViewModel? model)
        {
            model.Rows = service.SearchOrderByEmail(model.SearchKey);
            return View("Index", model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            DeleteViewModel model = new DeleteViewModel();
            model.Message = "Are you sure to cancel the order?";
            model.DeleteRow = service.FindOrderById(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(DeleteViewModel model)
        {
            service.CancelOrder(model.DeleteRow.Id);
            return RedirectToAction("Index", "Order",
                new { message = "delete the order successfully" });
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            Order order = service.FindOrderById(id);
            DetailViewModel model = new DetailViewModel();
            model.Row = order;
            if(order == null)
            {
                model.Message = "There are no items in the order!";
            }
            return View(model);
        }
    }
 }
