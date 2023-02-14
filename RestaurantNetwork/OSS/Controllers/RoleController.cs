using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using OSS.Models.Role;
using RestaurantDao.IServices;
using RestaurantDao.Models;
using RestaurantDao.Services;

namespace OSS.Controllers
{
    public class RoleController : BaseController
    {
        public RoleController(ILogger<RestaurantController> logger,IOssService service): base(logger, service) { }
        
        public IActionResult Index()
        {
            ListViewModel model = new ListViewModel();
            var roles = service.ListRole();
            if(roles == null)
            {
                model.Message = "No records";
            }
            else
            {
               model.Rows = roles;
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Add(int id)
        {
            AddViewModel model = new AddViewModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult Add(AddViewModel model)
        {
            if (ModelState.IsValid)
            {
                service.AddRole(model.Name);
                return RedirectToAction("Index", "Role", new {message = "Add the role successfully!" });
            }
            model.Message = "Failed to add the role";
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(string id)
        {
            service.DeleteRole(id);
            return RedirectToAction("Index", "Role", new { message = "Delete the role successfully" });
        }
    }
}
