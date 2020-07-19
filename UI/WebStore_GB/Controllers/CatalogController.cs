using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore_GB.Domain.Entities;
using WebStore_GB.Domain.ViewModels;
using WebStore_GB.Interfaces.Services;
using WebStore_GB.Services.Mapping;

namespace WebStore_GB.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _productData;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

        public CatalogController(IProductData productData, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _productData = productData;
            _configuration = configuration;
        }

        public IActionResult Shop(int? SectionId, int? BrandId, [FromServices] IMapper mapper, int page = 1)
        {
            var pageSize = int.TryParse(_configuration["PageSize"], out var size)
                ? size
                : (int?)null;

            var filter = new ProductFilter
            {
                SectionId = SectionId,
                BrandId = BrandId,
                Page = page,
                PageSize = pageSize
            };

            var products = _productData.GetProducts(filter);

            return View(new CatalogViewModel
            {
                SectionId = SectionId,
                BrandId = BrandId,
                Products = products
                    .Products
                    .Select(p => p.FromDTO())
                    .Select(mapper.Map<ProductViewModel>)
                    .OrderBy(p => p.Order),
                PageViewModel = new PageViewModel 
                { 
                    PageSize = pageSize ?? 0,
                    PageNumber = page,
                    TotalItems = products.TotalCount
                }
            });
        }

        public IActionResult Details(int id)
        {
            var product = _productData.GetProductById(id);

            if (product is null)
                return NotFound();

            return View(product.FromDTO().ToView());
        }
    }
}