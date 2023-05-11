using CEC.Application.UnitOfWork;
using CEC.WebMVC.Areas.Admin.Controllers.Base;

using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Index()
        {
            dynamic model = new ExpandoObject();
            return View(model);
        }
    }
}