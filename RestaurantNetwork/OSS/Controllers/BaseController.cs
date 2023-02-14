using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantDao.IServices;

namespace OSS.Controllers
{
    [Authorize(Roles ="Operator")]
    public class BaseController : Controller
    {
        internal readonly ILogger<BaseController> logger;
        internal readonly IOssService service;

        public BaseController(ILogger<BaseController> logger, IOssService service)
        {
            this.logger = logger;
            this.service = service;
        }
    }
}
