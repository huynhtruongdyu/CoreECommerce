using CEC.Infrastructure.Contexts;
using CEC.Shared.Models.DTO;
using CEC.WebMVC.Areas.Admin.Controllers.Base;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CEC.WebMVC.Areas.Admin.Controllers
{
    public class ProductsController : BaseAdminController
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var rawProducts = await _context.Products.ToListAsync();
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
        public async Task<IActionResult> Add([FromForm] ProductCreateModel model)
        {
            if (ModelState.IsValid)
            {
                await _context.Products.AddAsync(model.ToProduct());
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                product.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}