using Microsoft.AspNetCore.Mvc;
using RestaurantDao.IServices;
using RestaurantDao.Models;
using RMS.Models.Category;
using Microsoft.AspNetCore.Authorization;


namespace RMS.Controllers
{
    // [Authorize]
    public class CategoryController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IRmsService service;

        public CategoryController(ILogger<HomeController> logger, IRmsService service)
        {
            this.logger = logger;
            this.service = service;
        }

        [Authorize]
        public IActionResult Index()
        {
            ListViewModel model = new ListViewModel();
            // string message = Request.Query["message"].ToString();
            // model.Message = message;
            var restaurantId = Int32.Parse(HttpContext.Session.GetString("RestaurantId"));
            List<MenuCategory> categories = service.ListCategry(restaurantId);
            model.Categories = categories;
            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            AddViewModel model = new AddViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(AddViewModel model)
        {
            if (ModelState.IsValid)
            {
                // var restaurantId = 6;
                var restaurantId = Int32.Parse(HttpContext.Session.GetString("RestaurantId"));
                MenuCategory category = new MenuCategory();
                category.Name = model.Name;
                service.AddCategory(restaurantId, category);
                TempData["SuccessMessage"] = "Added the category successfully!";
                return RedirectToAction("Index", "Category", new { message = "Add the category successfully!" });
            }
            TempData["FailureMessage"] = "Failed to add the category";
            // model.Message = "Failed to add the category";
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            DeleteViewModel model = new DeleteViewModel();
            // var restaurantId = 6;
            var restaurantId = Int32.Parse(HttpContext.Session.GetString("RestaurantId"));
            MenuCategory? category = service.FindCategory(restaurantId, id);
            if (category == null)
            {
                throw new ArgumentException("No such a category!");
            }
            model.Id = category.Id;
            model.Name = category?.Name;
            TempData["FailureMessage"] = "Are you sure to delete the category?";
            // model.Message = "Are you sure to delete the category?";
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(DeleteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var restaurantId = Int32.Parse(HttpContext.Session.GetString("RestaurantId"));
                MenuCategory? category = service.FindCategory(restaurantId, model.Id);
                if (category == null)
                {
                    throw new ArgumentException("No such a category!");
                }
                service.DeleteCategory(restaurantId, model.Id);
                TempData["SuccessMessage"] = "Deleted the category successfully!";
                return RedirectToAction("Index", "Category", new { message = "Delete the category successfully!" });
            }

            TempData["FailureMessage"] = "Failed to delete the category";
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            EditViewModel model = new EditViewModel();
            var restaurantId = Int32.Parse(HttpContext.Session.GetString("RestaurantId"));
            MenuCategory? category = service.FindCategory(restaurantId, id);
            if (category == null)
            {
                throw new ArgumentException("No such a category!");
            }
            model.Name = category.Name;
            model.Id = id;
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                // var restaurantId = 6;
                var restaurantId = Int32.Parse(HttpContext.Session.GetString("RestaurantId"));
                MenuCategory? category = service.FindCategory(restaurantId, model.Id);
                if (category == null)
                {
                    throw new ArgumentException("No such a category!");
                }
                category.Name = model.Name;
                service.UpdateCategory(restaurantId, category);
                TempData["SuccessMessage"] = "Add the category successfully!";
                return RedirectToAction("Index", "Category", new { message = "Edit the category successfully!" });
            }

            TempData["FailureMessage"] = "Failed to edit the category";
            return View(model);
        }
    }
}
