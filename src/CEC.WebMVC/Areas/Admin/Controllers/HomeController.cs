﻿using CEC.Application.Abstractions.UnitOfWork;
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
            var newestProducts = _unitOfWork.ProductRepository.Get(null, x => x.OrderByDescending(x => x.CreatedAt)).Take(10);
            model.NewestProducts = newestProducts;
            return View(model);
        }
    }
}