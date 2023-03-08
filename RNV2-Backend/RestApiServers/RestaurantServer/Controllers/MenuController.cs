using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantDaoBase.IServices;
using RestaurantDaoBase.Models;
using RestaurantServer.Services;
using System.Data;

namespace RestaurantServer.Controllers
{
    [Authorize(Roles = "Restaurant")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly ILogger<MenuController> logger;
        private readonly IMenuService service;
        private readonly IFileService fileService;
        public MenuController(ILogger<MenuController> logger, IMenuService service, IFileService fileService)
        {
            this.logger = logger;
            this.service = service;
            this.fileService = fileService;
        }

        [HttpGet("{restaurantId}/{categoryId}")]
        public Task<MenuCategory> List(string restaurantId, string categoryId)
        {
            return service.ListMenu(restaurantId,categoryId);
        }

        [HttpGet("{categoryId}/{menuId}")]
        public Task<MenuItem?> One(string categoryId, string menuId)
        {
            return service.FindMenu(categoryId, menuId);
        }

        [HttpGet("{restaurantId}")]
        public Task<List<MenuItem>?> SearchByName(string restaurantId, string name)
        {
            return service.SearchMenu(restaurantId, name);
        }

        [HttpPost("{restaurantId}")]
        public async Task<IActionResult> NewOne(string restaurantId,[FromForm] MenuItem form)
        {
            if (ModelState.IsValid)
            {
                if (form.UploadImg != null)
                {
                    try
                    {
                        form.Logo = fileService.SaveFile(form.UploadImg);
                        form.UploadImg = null;
                    }
                    catch (Exception ex)
                    {
                        logger.LogInformation(ex.Message);
                        return BadRequest(new AppResult("NewOne : The file cannot be saved", false));
                    }
                }

                try
                {
                    var result = await service.AddMenu(restaurantId,form);
                    if (result == true)
                        return Ok(new AppResult("", true));
                    else
                    {
                        if (form.Logo != null)
                            fileService.DeleteFile(form.Logo);
                        return BadRequest(new AppResult("", false));
                    }
                }
                catch (Exception ex)
                {
                    logger.LogInformation(ex.Message);
                    if (form.Logo != null)
                        fileService.DeleteFile(form.Logo);
                    return BadRequest(new AppResult(ex.Message, false));
                }
            }
            return BadRequest(new AppResult("Some properties are not correct", false));
        }

        [HttpPut]
        public async Task<IActionResult> UpdatedOne([FromForm] MenuItem form)
        {
            if (ModelState.IsValid)
            {
                MenuItem? oldOne = await service.FindMenu(form.CategoryId,form.Id);
                if (oldOne == null)
                    return BadRequest(new AppResult($"No menu item (id={form.Id},categoryId={form.CategoryId})", false));

                if (form.UploadImg != null)
                {
                    try
                    {
                        form.Logo = fileService.SaveFile(form.UploadImg);
                        form.UploadImg = null;
                    }
                    catch (Exception ex)
                    {
                        logger.LogInformation(ex.Message);
                        return BadRequest(new AppResult("UpdatedOne : The file cannot be saved", false));
                    }
                }

                var result = await service.UpdateMenu(form);
                if (result == true)
                {
                    fileService.DeleteFile(oldOne.Logo);
                    return Ok(new AppResult("", true));
                }
                else
                {
                    logger.LogInformation("service.Update Menu failed ");
                    if (form.Logo != null)
                        fileService.DeleteFile(form.Logo);
                    return BadRequest(new AppResult("", false));
                }

            }
            return BadRequest(new AppResult("Some properties are not correct", false));
        }

        [HttpDelete("{categoryId}/{id}")]
        public async Task<IActionResult> DeletedOne(string categoryId,string id)
        {
            MenuItem? row = await service.FindMenu(categoryId,id);
            if (row == null)
                return BadRequest(new AppResult($"No menu item(id={id}", false));
            if (row.Logo != null)
            {
                try
                {
                    fileService.DeleteFile(row.Logo);
                }
                catch (Exception ex)
                {
                    logger.LogInformation(ex.Message);
                    return BadRequest(new AppResult(ex.Message, false));
                }
            }
            var result = await service.DeleteMenu(categoryId,id);
            if (result == true)
                return Ok(new AppResult("", true));
            else
                return BadRequest(new AppResult("", false));
        }
    }
}
