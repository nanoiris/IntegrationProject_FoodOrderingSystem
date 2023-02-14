using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using OSS.Models.User;
using RestaurantDao.IServices;
using RestaurantDao.Models;
using RestaurantDao.Services;

namespace OSS.Controllers
{
    public class UserController : BaseController
    {
        public UserController(ILogger<RestaurantController> logger,IOssService service): base(logger, service) { }
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Restaurant", new { message = "Please choose the restaurent first!" });
        }
        public IActionResult List(int id)
        {
            ListViewModel model = new ListViewModel();
            model.RestaurantId = id;
            model.Rows = service.ListRestaurantUsers(id);
            return View(model);
        }

        [HttpGet]
        public IActionResult Add(int id)
        {
            logger.LogInformation(id.ToString());
            AddViewModel model = new AddViewModel();
            model.RestaurantId = id;
            return View(model);
        }
        [HttpPost]
        public IActionResult Add(AddViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    Name = model.Name,
                    PhoneNo = model.Phone,
                    Email = model.Email,
                    Password = model.Password,
                    
                };
                user.Restaurant = new Restaurant { Id = (int)model.RestaurantId };
                IdentityUser idUser = new IdentityUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                    PhoneNumber = model.Phone,
                };
                user.IdUser = idUser;
                
                Stream stream = null;
                if (model.UploadLogo != null)
                {
                    user.Logo = model.UploadLogo.FileName;
                    stream = model.UploadLogo.OpenReadStream();
                }
                service.AddRestaurantUser(user, stream);
                return RedirectToAction("List", "User", new {id = model.RestaurantId, message = "Add the user successfully!" });
            }
            model.Message = "Failed to add the user";
            return View(model);
        }
        
        [HttpGet]
        public IActionResult Delete(int id)
        {
            DeleteViewModel model = new DeleteViewModel();
            model.Message = "Are you sure to delete the user?";
            model.DeleteRow = service.FindRestaurantUser(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(DeleteViewModel model)
        {
            if (model.DeleteRow != null && model.DeleteRow.Id > 0)
            {
                service.DeleteRestaurantUser(model.DeleteRow.Id);
                return RedirectToAction("List", "User", new { Id=model.DeleteRow.Restaurant.Id,message = "Delete the user successfully!" });
            }
            model.Message = "Failed to delete the user";
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            EditViewModel model = new EditViewModel();
            AppUser user = service.FindRestaurantUser(id);
            model.Avatar = user.Avatar;
            model.RestaurantId = id;
            model.Name = user.Name;
            model.Email = user.Email;
            model.Id = user.Id;
            model.idUserId = user.IdUser.Id;
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    Id = model.Id,
                    Name = model.Name,
                    Email = model.Email,
                    PhoneNo = model.Phone,
                    Password = model.Password,
                };
                user.Restaurant = new Restaurant { Id = (int)model.RestaurantId };

                IdentityUser idUser = new IdentityUser
                {
                    Id = model.idUserId
                };
                user.IdUser= idUser;

                Stream stream = null;
                if (model.UploadLogo != null)
                {
                    user.Logo = model.UploadLogo.FileName;
                    stream = model.UploadLogo.OpenReadStream();
                }
                service.UpdateRestaurantUser(user, stream);
                return RedirectToAction("List", "User", new { Id = model.RestaurantId,message = "Edit the user successfully!" });
            }

            model.Message = "Failed to edit the user";
            return View(model);
        }

    }
}
