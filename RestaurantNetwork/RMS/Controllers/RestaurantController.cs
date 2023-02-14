using Microsoft.AspNetCore.Mvc;
using RestaurantDao.IServices;
using RestaurantDao.Models;
using RMS.Models.Restaurant;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RMS.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IRmsService service;

        public RestaurantController(ILogger<HomeController> logger, IRmsService service)
        {
            this.logger = logger;
            this.service = service;
        }
        private List<SelectListItem> buildCategoryList()
        {
            List<RestCategory> categories = service.ListCategory();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (RestCategory category in categories)
            {
                var item = new SelectListItem { Text = category.Name, Value = category.Id.ToString() };
                list.Add(item);
            }
            return list;
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var restaurantId = Int32.Parse(HttpContext.Session.GetString("RestaurantId"));
            Restaurant rest = service.FindRestaurant(restaurantId);
            if (rest == null)
            {
                return RedirectToAction("Index", "Restaurant", new { message = "No such a restaurant to edit!" });
            }
            EditViewModel model = new EditViewModel
            {
                Id = rest.Id,
                Name = rest.Name,
                Description = rest.Description,
                Street = rest.Street,
                City = rest.City,
                State = rest.State,
                // Country = rest.Country,
                Zip = rest.Zip,
                PhoneNo = rest.PhoneNo,
                Email = rest.Email,
                FullLogoPath = rest.FullLogoPath,
            };
            model.Categories = buildCategoryList();
            // model.IsFeatured = false;
            // if (rest.Featured == RestaurantDao.Enums.FeaturedEnum.Yes)
            //     model.IsFeatured = true;
            //model.CategoryId = rest.CategoryId;
            model.CategoryId = rest.Category.Id;

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditViewModel model)
        {
            logger.LogInformation("enter Edit : " + model.FullLogoPath + "------");

            if (ModelState.IsValid)
            {
                logger.LogInformation("---- enter Edit ModelState.IsValid :");
                var restaurantId = Int32.Parse(HttpContext.Session.GetString("RestaurantId"));
                Restaurant rest = service.FindRestaurant(restaurantId);
                rest.Id = model.Id;
                rest.Name = model.Name;
                rest.Email = model.Email;
                rest.PhoneNo = model.PhoneNo;
                rest.Street = model.Street;
                rest.City = model.City;
                rest.State = model.State;
                // Country = model.Country; 
                rest.Zip = model.Zip;
                //rest.CategoryId = model.CategoryId;
                RestCategory category = new RestCategory { Id = (int)model.CategoryId };

                Stream stream = null;
                if (model.UploadLogo != null)
                {
                    rest.Logo = model.UploadLogo.FileName;
                    stream = model.UploadLogo.OpenReadStream();
                }
                logger.LogInformation("---- Edit :" + rest.Id);
                logger.LogInformation("---- Edit :" + rest.Logo);
                service.UpdateRestaurant(rest, stream);
                logger.LogInformation("---- Edit UpdateRestaurant :");
                TempData["SuccessMessage"] = "Updated successfully!";
                return RedirectToAction("Edit", "Restaurant", new { message = "Updated successfully!" });
            };

            List<string> Keys = ModelState.Keys.ToList();
            foreach (var key in Keys)
            {
                var errors = ModelState[key].Errors.ToList();
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }


            var categories = buildCategoryList();
            model.Categories = categories;
            // model.CategoryId = restaurant.CategoryId;
            TempData["FailureMessage"] = "Failed to update profile information";
            // model.Message = "Failed to update profile information";
            return View(model);


        }

    }
}

