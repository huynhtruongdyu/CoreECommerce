using CEC.Application.Services;
using CEC.Application.UnitOfWork;

using Microsoft.AspNetCore.Mvc;

namespace CEC.WebMVC.Areas.Admin.Controllers.Base
{
    [Area("Admin")]
    public abstract class BaseAdminController : Controller
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IServiceManagement serviceManagement;

        protected BaseAdminController(IUnitOfWork unitOfWork, IServiceManagement serviceManagement)
        {
            this.unitOfWork = unitOfWork;
            this.serviceManagement = serviceManagement;
        }
    }
}