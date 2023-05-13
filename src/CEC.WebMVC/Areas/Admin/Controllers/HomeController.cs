using CEC.Application.UnitOfWork;
using CEC.WebMVC.Areas.Admin.Controllers.Base;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

using System.Dynamic;

namespace CEC.WebMVC.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]
        public IActionResult Index()
        {
            dynamic model = new ExpandoObject();
            model.TotalProducts = _unitOfWork.ProductRepository.Get(x => x.Status == Domain.Enums.EnumProductStatus.Active).Count();
            return View(model);
        }
    }
}