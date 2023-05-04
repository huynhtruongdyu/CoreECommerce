using CEC.WebMVC.Areas.Marketing.Controllers.Base;

using Microsoft.AspNetCore.Mvc;

namespace CEC.WebMVC.Areas.Marketing.Controllers
{
    public class HomeController : BaseMarketingController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}