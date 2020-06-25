using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore_GB.Domain.Entities;
using WebStore_GB.Infrastructure.Interfaces;
using WebStore_GB.Infrastructure.Mapping;
using WebStore_GB.ViewModels;

namespace WebStore_GB.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _productData;

        public CatalogController(IProductData productData) => _productData = productData;

        public IActionResult Shop(int? SectionId, int? BrandId)
        {
            var filter = new ProductFilter
            {
                SectionId = SectionId,
                BrandId = BrandId
            };

            var products = _productData.GetProducts(filter);

            return View(new CatalogViewModel
            {
                SectionId = SectionId,
                BrandId = BrandId,
                Products = products
                    .ToView()
                    .OrderBy(p => p.Order)
            });
        }

        public IActionResult Details(int id)
        {
            var product = _productData.GetProductById(id);

            if (product is null)
                return NotFound();

            return View(product.ToView());
        }
    }
}