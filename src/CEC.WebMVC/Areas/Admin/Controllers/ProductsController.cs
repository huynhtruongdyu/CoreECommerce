using CEC.Application.Services;
using CEC.Application.UnitOfWork;
using CEC.Infrastructure.Contexts;
using CEC.Shared.Models;
using CEC.Shared.Models.DTO;
using CEC.WebMVC.Areas.Admin.Controllers.Base;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CEC.WebMVC.Areas.Admin.Controllers
{
    public class ProductsController : BaseAdminController
    {
        public ProductsController(IUnitOfWork unitOfWork, IServiceManagement serviceManagement) : base(unitOfWork, serviceManagement)
        {
        }

        public async Task<IActionResult> Index()
        {
            var rawProducts = await unitOfWork.ProductRepository.GetAllAsync();
            rawProducts = rawProducts.OrderByDescending(c => c.CreatedAt);
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
                await unitOfWork.ProductRepository.InsertAsync(model.ToProduct());
                unitOfWork.Commit();
                serviceManagement.UserActivityLogService.Log(new UserAcionLog("Admin", EnumUserAcion.Add, model.ToProduct()));
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            var product = await unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product != null)
            {
                product.IsDeleted = true;
                serviceManagement.UserActivityLogService.Log(new UserAcionLog("Admin", EnumUserAcion.Remove, product));
                unitOfWork.Commit();
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Detail(long id)
        {
            var rawProduct = await unitOfWork.ProductRepository.GetByIdAsync(id);
            var product = new ProductDetailModel(rawProduct);
            return View(product);
        }
    }
}