using CEC.Application.Services;
using CEC.Application.UnitOfWork;
using CEC.Infrastructure.Repositories;
using CEC.WebMVC.Areas.Admin.Controllers.Base;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

using System.Dynamic;

namespace CEC.WebMVC.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        public HomeController(IUnitOfWork unitOfWork, IUserActivityLogService userActivityLogService) : base(unitOfWork, userActivityLogService)
        {
        }

        [ResponseCache(Duration = 5, Location = ResponseCacheLocation.Any)]
        public IActionResult Index()
        {
            dynamic model = new ExpandoObject();
            model.TotalProducts = unitOfWork.ProductRepository.Get(x => x.Status == Domain.Enums.EnumProductStatus.Active).Count();
            model.UserActivities = userActivityLogService.GetLog();
            return View(model);
        }
    }
}