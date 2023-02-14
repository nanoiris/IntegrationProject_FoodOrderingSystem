using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RestaurantDao.IServices;
using RestaurantDao.Models;
using RMS.Models.Item;

namespace RMS.Controllers
{
    public class ItemController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IRmsService service;

        public ItemController(ILogger<HomeController> logger, IRmsService service)
        {
            this.logger = logger;
            this.service = service;
        }

        public IActionResult Index()
        {
            ListViewModel model = new ListViewModel();

            // string message = Request.Query["message"].ToString();
            // model.Message = message;
            var restaurantId = Int32.Parse(HttpContext.Session.GetString("RestaurantId"));
            List<MenuItem> items = service.ListMenu(restaurantId);
            model.Items = items;

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            AddViewModel model = new AddViewModel();
            var categories = buildCategoryList();
            model.Categories = categories;
            if (categories.Count == 0)
            {
                TempData["FailureMessage"] = "No Category exist, Please add category first";
                return RedirectToAction("Index", "Category", new { message = "" });
            }
            return View(model);
        }

        private List<SelectListItem> buildCategoryList()
        {
            // int restaurantId = 6;
            var restaurantId = Int32.Parse(HttpContext.Session.GetString("RestaurantId"));
            List<MenuCategory> categories = service.ListCategry(restaurantId);
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (MenuCategory category in categories)
            {
                var item = new SelectListItem { Text = category.Name, Value = category.Id.ToString() };
                list.Add(item);
            }
            return list;
        }

        [HttpPost]
        public IActionResult Add(AddViewModel model)
        {
            if (ModelState.IsValid)
            {

                // var restaurantId = 6;
                var restaurantId = Int32.Parse(HttpContext.Session.GetString("RestaurantId"));
                MenuItem item = new MenuItem()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Discount = model.Discount,

                };
                item.Category = new MenuCategory { Id = model.CategoryId };
                logger.LogInformation(item.Category.Id.ToString());
                Stream stream = null;
                if (model.UploadLogo != null)
                {
                    item.Logo = model.UploadLogo.FileName;
                    stream = model.UploadLogo.OpenReadStream();
                    logger.LogInformation(item.Logo);
                }
                service.AddMenu(restaurantId, item, stream);
                TempData["SuccessMessage"] = "Added the menu item successfully!";
                return RedirectToAction("Index", "Item", new { message = "Add the menu item successfully!" });
            }
            logger.LogInformation("find Name:" + model.Name);
            var categories = buildCategoryList();
            model.Categories = categories;
            TempData["FailureMessage"] = "Failed to add the food item!";
            // model.Message = "Failed to add the food item!";
            return View("Add", model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            // var restaurantId = 6;
            var restaurantId = Int32.Parse(HttpContext.Session.GetString("RestaurantId"));
            MenuItem? item = service.FindMenu(restaurantId, id);
            if (item == null)
            {
                TempData["FailureMessage"] = "No such item to Edit";
                return RedirectToAction("Index", "Edit");
            }
            EditViewModel model = new EditViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                Discount = item.Discount,
                FullLogoPath = item.FullLogoPath,
            };
            model.Categories = buildCategoryList();
            model.CategoryId = item.Category.Id;
            model.IsFeatured = false;
            if (item.Featured == RestaurantDao.Enums.FeaturedEnum.Yes)
                model.IsFeatured = true;
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var restaurantId = Int32.Parse(HttpContext.Session.GetString("RestaurantId"));
                MenuItem? item = service.FindMenu(restaurantId, model.Id);
                if (item == null)
                {
                    throw new ArgumentException("No such a menu item!");
                }
                item.Name = model.Name;
                item.Description = model.Description;
                item.Price = model.Price;
                item.Discount = model.Discount;
                item.Category = new MenuCategory { Id = model.CategoryId };
                logger.LogInformation(item.Category.Id.ToString());
                Stream stream = null;
                if (model.UploadLogo != null)
                {
                    item.Logo = model.UploadLogo.FileName;
                    stream = model.UploadLogo.OpenReadStream();
                    logger.LogInformation(item.Logo);
                }
                item.Featured = RestaurantDao.Enums.FeaturedEnum.No;
                if (model.IsFeatured == true)
                    item.Featured = RestaurantDao.Enums.FeaturedEnum.Yes;
                service.UpdateMenu(restaurantId, item, stream);
                TempData["SuccessMessage"] = "Edited the menu item successfully!";
                return RedirectToAction("Index", "Item", new { message = "Edit the menu item successfully!" });
            }
            var categories = buildCategoryList();
            model.Categories = categories;

            // model.Message = "Failed to Edit the food item!";
            TempData["FailureMessage"] = "Failed to Edit the food item!";
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            DeleteViewModel model = new DeleteViewModel();


            // var restaurantId = 6;
            var restaurantId = Int32.Parse(HttpContext.Session.GetString("RestaurantId"));
            MenuItem? item = service.FindMenu(restaurantId, id);
            if (item == null)
            {
                throw new ArgumentException("No such a menu item!");
            }
            model.Id = item.Id;
            model.Name = item?.Name;
            TempData["FailureMessage"] = "Are you sure to delete the item?";

            // model.Message = "Are you sure to delete the item?";
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(DeleteViewModel model)
        {
            if (ModelState.IsValid)
            {

                // var restaurantId = 6;
                var restaurantId = Int32.Parse(HttpContext.Session.GetString("RestaurantId"));
                MenuItem? item = service.FindMenu(restaurantId, model.Id);
                if (item == null)
                {
                    throw new ArgumentException("No such a menu item!");
                }
                service.DeleteMenu(restaurantId, model.Id);
                TempData["SuccessMessage"] = "Deleted the menu item successfully!";
                return RedirectToAction("Index", "Item", new { message = "Delete the  menu item successfully!" });
            }
            // model.Message = "Failed to delete the  menu item";
            TempData["FailureMessage"] = "Failed to delete the  menu item";

            return View(model);
        }


    }
}
