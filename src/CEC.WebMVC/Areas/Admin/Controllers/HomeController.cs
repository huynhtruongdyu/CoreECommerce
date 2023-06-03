using CEC.Application.Services;
using CEC.Application.UnitOfWork;
using CEC.WebMVC.Areas.Admin.Controllers.Base;

using Microsoft.AspNetCore.Mvc;

using System.Dynamic;

namespace CEC.WebMVC.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        public HomeController(IUnitOfWork unitOfWork, IServiceManagement serviceManagement) : base(unitOfWork, serviceManagement)
        {
        }

        [ResponseCache(Duration = 5, Location = ResponseCacheLocation.Any)]
        public IActionResult Index()
        {
            dynamic model = new ExpandoObject();
            model.TotalProducts = unitOfWork.ProductRepository.Get(x => x.Status == Domain.Enums.EnumProductStatus.Active).Count();
            model.UserActivities = serviceManagement.UserActivityLogService.GetLog();
            return View(model);
        }
    }
}