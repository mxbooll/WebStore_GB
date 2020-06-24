using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore_GB.Domain.Entities;
using WebStore_GB.Domain.ViewModels;
using WebStore_GB.Infrastructure.Interfaces;
using WebStore_GB.Infrastructure.Mapping;

namespace WebStore_GB.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _productData;

        public CatalogController(IProductData productData) => _productData = productData;

        public IActionResult Shop(int? SectionId, int? BrandId, [FromServices] IMapper mapper)
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
                    .Select(mapper.Map<ProductViewModel>)
                    //.Select(p => Mapper.Map<ProductViewModel>(p))
                    //.ToView()
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