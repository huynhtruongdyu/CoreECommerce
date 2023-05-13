using CEC.Application.Services;
using CEC.Application.UnitOfWork;

using Microsoft.AspNetCore.Mvc;

namespace CEC.WebMVC.Areas.Admin.Controllers.Base
{
    [Area("Admin")]
    public abstract class BaseAdminController : Controller
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IUserActivityLogService userActivityLogService;

        protected BaseAdminController(IUnitOfWork unitOfWork, IUserActivityLogService userActivityLogService)
        {
            this.unitOfWork = unitOfWork;
            this.userActivityLogService = userActivityLogService;
        }
    }
}