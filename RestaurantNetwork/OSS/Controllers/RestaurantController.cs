using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using OSS.Models.Restaurant;
using RestaurantDao.IServices;
using RestaurantDao.Models;

namespace OSS.Controllers
{
    public class RestaurantController : BaseController
    {
        public RestaurantController(ILogger<RestaurantController> logger, IOssService service) : base(logger, service) { }
        public IActionResult Index()
        {
            ListViewModel model = new ListViewModel();
            string message = Request.Query["message"].ToString();
            model.Message = message;
            model.Restaurants = service.ListRestaurant();
            return View(model);
        }
        [HttpGet]
        public IActionResult Add()
        {
            AddViewModel model = new AddViewModel();
            model.Categories = buildCategoryList();
            return View(model);
        }
        [HttpPost]
        public IActionResult Add(AddViewModel model)
        {
            if (ModelState.IsValid)
            {
                Restaurant restaurant = new Restaurant()
                {
                    Name = model.Name,
                    Email = model.Email,PhoneNo= model.PhoneNo,
                    Street= model.Street,City= model.City,State=model.State,Country=model.Country,
                    Zip=model.Zip,
                    Description=model.Description

                };
                restaurant.Category = new RestCategory { Id = (int)model.CategoryId };
                restaurant.Featured = RestaurantDao.Enums.FeaturedEnum.No;
                if (model.IsFeatured == true)
                    restaurant.Featured = RestaurantDao.Enums.FeaturedEnum.Yes;

                Stream stream = null;
                if (model.UploadLogo != null)
                {
                    restaurant.Logo = model.UploadLogo.FileName;
                    stream = model.UploadLogo.OpenReadStream();
                }
                service.AddRestaurant(restaurant, stream);
                return RedirectToAction("Index", "Restaurant", new { message = "Add the restaurant successfully!" });
            }
            model.Message = "Failed to add the restaurant";
            model.Categories = buildCategoryList();
            return View(model);
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
        public IActionResult Delete(int id)
        {
            DeleteViewModel model = new DeleteViewModel();
            model.Message = "Are you sure to delete the restaurant?";
            model.DeleteRest = service.FindRestaurant(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(DeleteViewModel model)
        {
            if (model.DeleteRest != null && model.DeleteRest.Id > 0)
            {
                service.DeleteRestaurant(model.DeleteRest.Id);
                return RedirectToAction("Index", "Restaurant", new { message = "Delete the restaurant successfully!" });
            }
            model.Message = "Failed to delete the restaurant";
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Restaurant rest = service.FindRestaurant(id);
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
                Country = rest.Country,
                Zip = rest.Zip,
                PhoneNo = rest.PhoneNo,
                Email = rest.Email,
                FullLogoPath = rest.FullLogoPath,
            };
            model.Categories = buildCategoryList();
            model.IsFeatured = false;
            if (rest.Featured == RestaurantDao.Enums.FeaturedEnum.Yes)
                model.IsFeatured = true;
            //model.CategoryId= rest.CategoryId;
            model.CategoryId = rest.Category.Id;
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {

                Restaurant restaurant = service.FindRestaurant(model.Id);

                restaurant.Id = model.Id;
                restaurant.Name = model.Name;
                restaurant.Email = model.Email;
                restaurant.PhoneNo = model.PhoneNo;
                restaurant.Street = model.Street;
                restaurant.City = model.City;
                restaurant.State = model.State;
                restaurant.Country = model.Country;
                restaurant.Zip = model.Zip;
                restaurant.Description = model.Description;


                RestCategory category = new RestCategory { Id = (int)model.CategoryId };
                restaurant.Category = category;
                restaurant.Featured = RestaurantDao.Enums.FeaturedEnum.No;
                if (model.IsFeatured == true)
                    restaurant.Featured = RestaurantDao.Enums.FeaturedEnum.Yes;

                Stream stream = null;
                if (model.UploadLogo != null)
                {
                    restaurant.Logo = model.UploadLogo.FileName;
                    stream = model.UploadLogo.OpenReadStream();
                }
                service.UpdateRestaurant(restaurant, stream);
                return RedirectToAction("Index", "Restaurant", new { message = "Edit the restaurant successfully!" });
            }

            model.Message = "Failed to delete the restaurant";
            return View(model);
        }

    }
}
