using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using OSS.Models.Category;
using RestaurantDao.IServices;
using RestaurantDao.Models;
using RestaurantDao.Services;

namespace OSS.Controllers
{
    public class CategoryController : BaseController
    {
        public CategoryController(ILogger<RestaurantController> logger,IOssService service): base(logger, service) { }
        
        public IActionResult Index()
        {
            ListViewModel model = new ListViewModel();
            List<RestCategory>? rows = service.ListCategory();
            if(rows == null)
            {
                model.Message = "No records";
            }
            else
            {
                model.Rows = rows;
            }
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
                RestCategory row = new RestCategory
                {
                    Name = model.Name,
                };
                
                Stream stream = null;
                if (model.UploadLogo != null)
                {
                    row.Logo = model.UploadLogo.FileName;
                    stream = model.UploadLogo.OpenReadStream();
                }
                service.AddRestaurantCategory(row, stream);
                return RedirectToAction("Index", "Category",
                    new { message = "Add the category successfully!" });
            }
            model.Message = "Failed to add the category";
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            DeleteViewModel model = new DeleteViewModel();
            model.Message = "Are you sure to delete the category?";
            model.DeleteRow = service.FindCategory(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(DeleteViewModel model)
        {
            if (model.DeleteRow != null && model.DeleteRow.Id > 0)
            {
                service.DeleteCategory(model.DeleteRow.Id);
                return RedirectToAction("Index", "Category", 
                    new { message = "Delete the category successfully!" });
            }
            model.Message = "Failed to delete the category";
            return View(model);
        }
    }
}
