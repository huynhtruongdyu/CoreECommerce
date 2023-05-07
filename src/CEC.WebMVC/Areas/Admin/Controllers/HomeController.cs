using CEC.WebMVC.Areas.Admin.Controllers.Base;

using Microsoft.AspNetCore.Mvc;

namespace CEC.WebMVC.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}