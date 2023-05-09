using CEC.Application.Abstractions.UnitOfWork;
using CEC.Shared.Models.DTO;

using Microsoft.AspNetCore.Mvc;

namespace CEC.WebMVC.Areas.MicroViews.Controllers
{
    [Area("MicroViews")]
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult NewProductsAsync()
        {
            var rawProducts = _unitOfWork.ProductRepository.Get(null, x => x.OrderByDescending(x => x.CreatedAt)).Take(3);
            var productsDto = new List<ProductDto>();
            foreach (var rawProduct in rawProducts)
            {
                productsDto.Add(new ProductDto(
                    rawProduct.Id,
                    rawProduct.Name,
                    rawProduct.Brief,
                    rawProduct.ThumbnailUrl,
                    rawProduct.Status));
            }

            return View(productsDto);
        }
    }
}