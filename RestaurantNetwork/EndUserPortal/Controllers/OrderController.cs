using EndUserPortal.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using RestaurantDao.Enums;
using RestaurantDao.IServices;
using RestaurantDao.Models;
using Stripe;
using Stripe.Checkout;

namespace EndUserPortal.Controllers
{

    [Authorize]
    public class OrderController : Controller
    {
        private IRestarantService restarantService;
        private IOrderService orderService;
        private IMenuService menuService;
        private readonly ILogger<OrderController> logger;

        public OrderController(IRestarantService restarantService, IOrderService orderService, IMenuService menuService, ILogger<OrderController> logger)
        {
            this.restarantService = restarantService;
            this.orderService = orderService;
            this.menuService = menuService;
            this.logger = logger;

        }
        [HttpGet]
        public IActionResult Restaurant(int id)
        {
            Restaurant restaurant = restarantService.GetRestById(id);
            List<MenuItem> featuredMenus = restarantService.GetFeaturedMenus(id);
            string email = HttpContext.Session.GetString("UserEmail");
            Order order = orderService.FindCartByRestaurantAndUser(id, email);
            String[] ratings = restarantService.CalculateRatings(id);
            int totalRatings = restarantService.CalculateTotalRatings(id);

            RestaurantViewModel model = new RestaurantViewModel
            {
                Restaurant = restaurant,
                Order = order,
                FeaturedMenus = featuredMenus,
                Ratings = ratings,
                TotalRatings= totalRatings
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult Decrease(int id,int orderId,int orderItemId)
        {
            orderService.DecreaseDishQty(null, orderItemId, orderId);
            return buildCatePartialView(id);
        }
        [HttpGet]
        public IActionResult Increase(int id, int orderId, int orderItemId)
        {
            orderService.IncreaseDishQty(null, orderItemId, orderId);
            return buildCatePartialView(id);
        }

        [HttpGet]
        public IActionResult UpdateCart(int id,int restaurantId)
        {
            var menuItemId = id;
            string email = HttpContext.Session.GetString("UserEmail");
            Order order = orderService.FindCartByRestaurantAndUser(restaurantId, email);
            if (order == null)
            {
                orderService.CreateCart(email, menuItemId, restaurantId);
            }
            else
            {
                orderService.AddDishToCart(order, menuItemId);
            }
            return buildCatePartialView(restaurantId);
            //return RedirectToAction("Restaurant", new { id = restaurantId });
        }

        [HttpPost]
        [HttpGet]
        public ActionResult CreateCheckoutSession(int orderId, DateTime pickupTime, string restaurantName, double payTotal)
        {
            Order order = orderService.GetOrderById(orderId);
            order.DesiredTime = pickupTime;

            HttpContext.Session.SetInt32("orderId",orderId);
            HttpContext.Session.SetInt32("restaurantId", order.Provider.Id);

            StripeConfiguration.ApiKey = "sk_test_51MZ0uECfoDmFMZLdzmF7eLHHLAonoZe5hptwRkvNfg4imaHG8djSCo0Z8dNJF0l1yJdOOfz2ftDlI0O3JnqUJYWU00zdNk6KJr";

            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                      UnitAmount = (long?)( payTotal* 100),
                      Currency = "cad",
                      ProductData = new SessionLineItemPriceDataProductDataOptions
                      {
                        Name = restaurantName,
                        Description = $"Estimate Pick Up Time: {pickupTime.ToString("ddd, dd MMM yyyy  HH':'mm")}"
                      },
                    },
                    Quantity = 1,
                  },
                },
                Mode = "payment",
                SuccessUrl = "https://rnportal1.azurewebsites.net/Order/CheckoutSuccess",
                CancelUrl = "https://rnportal1.azurewebsites.net/Home/Index",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        [HttpGet]
        public IActionResult CheckoutSuccess()
        {
            int orderId = (int)HttpContext.Session.GetInt32("orderId");
            int restaurantId = (int)HttpContext.Session.GetInt32("restaurantId");

            HttpContext.Session.Remove("orderId");
            HttpContext.Session.Remove("restaurantId");

            orderService.UpdateOrderStatus(restaurantId,orderId,StatusEnum.Paid);
            return View();
        }

        [HttpGet]
        public IActionResult MyOrder(string username, int? cancleId)
        {
            string orderCancle = "";
            string email = HttpContext.Session.GetString("UserEmail");
            

            if (cancleId.HasValue)
            {
                orderService.CancelCart((int)cancleId, email);
                orderCancle = "Order Cancled Successfully!";
            }
            List<Order> orders = orderService.getOrdersByUserName(email);
            MyOrderViewModel model = new MyOrderViewModel()
            {   
                ordersCart = orders.Where(x => x.Status == StatusEnum.Cart).ToList(),
                ordersReady = orders.Where(x => x.Status == StatusEnum.Paid).ToList(),
                ordersOnProgress = orders.Where(x => x.Status == StatusEnum.Ready || x.Status == StatusEnum.Accepted).ToList(),
                ordersCompleted = orders.Where(x => x.Status == StatusEnum.Completed).ToList(),
                ordersCancled = orders.Where(x => x.Status == StatusEnum.Canceled).ToList(),
                PickUpTime = DateTime.Now,
            };

            if (orderCancle != "")
            {
                model.cancleOrderSuccess = orderCancle;
            }
            return View(model);
        }

        public IActionResult buildCatePartialView(int id)
        {
            Restaurant restaurant = restarantService.GetRestById(id);
            string email = HttpContext.Session.GetString("UserEmail");
            Order order = orderService.FindCartByRestaurantAndUser(id, email);

            RestaurantViewModel model = new RestaurantViewModel
            {
                Restaurant = restaurant,
                Order = order,
                
            };
            return PartialView("Cart",model);
        }
    }
}
