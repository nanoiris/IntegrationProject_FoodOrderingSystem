using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantDaoBase.IServices;
using RestaurantDaoBase.Models;
using RestaurantServer.Services;

namespace RestaurantServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RestCategoryController : ControllerBase
    {
        private readonly ILogger<RestCategoryController> logger;
        private readonly IRestaurantService service;
        private readonly IFileService fileService;
        public RestCategoryController(ILogger<RestCategoryController> logger, IRestaurantService service,IFileService fileService)
        {
            this.logger = logger;
            this.service = service;
            this.fileService = fileService;
        }

        [HttpGet]
        public Task<List<RestCategory>> List()
        {
             return service.ListCategory();
        }

        [HttpPost]
        public async Task<IActionResult> NewOne([FromForm] RestCategory category)
        {
            if (ModelState.IsValid)
            {
                if(category.UploadImg != null)
                {
                    try
                    {
                        category.Logo = fileService.SaveFile(category.UploadImg);
                        category.UploadImg = null;
                    }
                    catch(Exception ex) 
                    {
                        logger.LogInformation(ex.Message);
                        return BadRequest(new AppResult("NewOne : The file cannot be saved", false));
                    }
                }

                try
                {
                    var result = await service.AddCategory(category);
                    if (result == true)
                        return Ok(new AppResult("", true));
                    else
                    {
                        if (category.Logo != null)
                            fileService.DeleteFile(category.Logo);
                        return BadRequest(new AppResult("", false));
                    }
                }
                catch(Exception ex)
                {
                    logger.LogInformation(ex.Message);
                    if (category.Logo != null)
                        fileService.DeleteFile(category.Logo);
                    return BadRequest(new AppResult(ex.Message, false));
                }
            }
            return BadRequest(new AppResult("Some properties are not correct", false));
        }

        [HttpGet("{id}")]
        public Task<RestCategory?> One(string id)
        {
            return service.FindCategory(id);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatedOne([FromForm] RestCategory category)
        {
            if (ModelState.IsValid)
            {
                RestCategory? oldOne = await service.FindCategory(category.Id);
                if (oldOne == null)
                    return BadRequest(new AppResult($"No rest category(id={category.Id}", false));

                if (category.UploadImg != null)
                {
                    try
                    {
                        category.Logo = fileService.SaveFile(category.UploadImg);
                        category.UploadImg = null;
                    }
                    catch (Exception ex)
                    {
                        logger.LogInformation(ex.Message);
                        return BadRequest(new AppResult("UpdatedOne : The file cannot be saved", false));
                    }
                }

                var result = await service.UpdateCategory(category);
                if (result == true)
                {
                    fileService.DeleteFile(oldOne.Logo);
                    return Ok(new AppResult("", true));
                }
                else
                {
                    logger.LogInformation("service.UpdateCategory failed ");
                    if (category.Logo != null)
                        fileService.DeleteFile(category.Logo);
                    return BadRequest(new AppResult("", false));
                }
                    
            }
            return BadRequest(new AppResult("Some properties are not correct", false));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletedOne(string id)
        {
            RestCategory? category = await service.FindCategory(id);
            if (category == null)
                return BadRequest(new AppResult($"No rest category(id={id}", false));
            if(category.Logo != null)
            {
                try
                {
                    fileService.DeleteFile(category.Logo);
                }
                catch(Exception ex) 
                {
                    logger.LogInformation(ex.Message);
                    return BadRequest(new AppResult(ex.Message, false));
                }
            }
            var result = await service.DeleteCategory(id);
            if (result == true)
                return Ok(new AppResult("", true));
            else
                return BadRequest(new AppResult("", false));
        }
    }
}