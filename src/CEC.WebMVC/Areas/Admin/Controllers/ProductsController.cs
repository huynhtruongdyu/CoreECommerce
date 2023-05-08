using CEC.Application.Abstractions.UnitOfWork;
using CEC.Infrastructure.Contexts;
using CEC.Shared.Models.DTO;
using CEC.WebMVC.Areas.Admin.Controllers.Base;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CEC.WebMVC.Areas.Admin.Controllers
{
    public class ProductsController : BaseAdminController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var rawProducts = await _unitOfWork.ProductRepository.GetAllAsync();
            var productsDto = new List<ProductDto>();
            foreach (var rawProduct in rawProducts)
            {
                productsDto.Add(new ProductDto(rawProduct.Id, rawProduct.Name, rawProduct.Brief, rawProduct.ThumbnailUrl));
            }

            return View(productsDto);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([FromForm] ProductCreateModel model)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.ProductRepository.InsertAsync(model.ToProduct());
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product != null)
            {
                product.IsDeleted = true;
                _unitOfWork.Commit();
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Detail(long id)
        {
            var rawProduct = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            var product = new ProductDetailModel(rawProduct);
            return View(product);
        }
    }
}