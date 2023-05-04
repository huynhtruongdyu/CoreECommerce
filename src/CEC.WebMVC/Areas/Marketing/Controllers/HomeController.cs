using CEC.Application.Abstractions.Services;
using CEC.Shared.Options;
using CEC.WebMVC.Areas.Marketing.Controllers.Base;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CEC.WebMVC.Areas.Marketing.Controllers
{
    public class HomeController : BaseMarketingController
    {
        private readonly IDatabaseProviderService _databaseProviderService;

        public HomeController(IDatabaseProviderService databaseProviderService)
        {
            _databaseProviderService = databaseProviderService;
        }

        public IActionResult Index()
        {
            var provider = _databaseProviderService.GetProvider();
            var connectionString = _databaseProviderService.GetConnectionString();
            return View();
        }
    }
}