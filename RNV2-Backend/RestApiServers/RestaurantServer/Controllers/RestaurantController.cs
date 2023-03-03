using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantDaoBase.IServices;
using RestaurantDaoBase.Models;
using RestaurantServer.Services;

namespace RestaurantServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly ILogger<RestaurantController> logger;
        private readonly IRestaurantService service;
        private readonly IFileService fileService;
        public RestaurantController(ILogger<RestaurantController> logger, IRestaurantService service, IFileService fileService)
        {
            this.logger = logger;
            this.service = service;
            this.fileService = fileService;
        }

        [HttpGet]
        public Task<List<Restaurant>> List()
        {
            return service.ListRestaurant();
        }

        [HttpGet("{limit}")]
        public Task<List<Restaurant>> ListWithLimit(int limit)
        {
            return service.ListWithLimit(limit);
        }

        [HttpGet]
        public Task<List<Restaurant>> ListWeeklyTrends()
        {
            return service.ListWeeklyTrends();
        }

        [HttpGet]
        public Task<List<Restaurant>> Search([FromBody]RestSearchForm model)
        {
            return service.Search(model.SearchKey,model.Categoryid);
        }

        [HttpPost]
        public async Task<IActionResult> NewOne([FromForm] RestaurantForm form)
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
                    var result = await service.AddRestaurant(form);
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

        [HttpGet("{id}")]
        public Task<Restaurant?> One(string id)
        {
            return service.FindRestaurant(id);
        }
        [HttpPut]
        public async Task<IActionResult> UpdatedOne([FromForm] RestaurantForm form)
        {
            if (ModelState.IsValid)
            {
                Restaurant? oldOne = await service.FindRestaurant(form.Id);
                if (oldOne == null)
                    return BadRequest(new AppResult($"No restaurant(id={form.Id}", false));

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

                var result = await service.UpdateRestaurant(form);
                if (result == true)
                {
                    if(oldOne.Logo != null)
                        fileService.DeleteFile(oldOne.Logo);
                    return form.CategoryId != null ? Ok(new AppResult(form.CategoryId, true))
                        : Ok(new AppResult("", true));
                }
                else
                {
                    logger.LogInformation("service.UpdateRestaurant failed ");
                    if (form.Logo != null)
                        fileService.DeleteFile(form.Logo);
                    return BadRequest(new AppResult("", false));
                }

            }
            return BadRequest(new AppResult("Some properties are not correct", false));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletedOne(string id)
        {
            Restaurant? row = await service.FindRestaurant(id);
            if (row == null)
                return BadRequest(new AppResult($"No restaurant(id={id}", false));
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
            var result = await service.DeleteRestaurant(id);
            if (result == true)
                return Ok(new AppResult("", true));
            else
                return BadRequest(new AppResult("", false));
        }
    }
}
