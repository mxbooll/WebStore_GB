using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebStore_GB.Domain;
using WebStore_GB.Domain.DTO.Products;
using WebStore_GB.Domain.Entities;
using WebStore_GB.Interfaces.Services;

namespace WebStore_GB.ServiceHosting.Controllers
{
    [Route(WebApi.PRODUCTS)]
    [ApiController]
    public class ProductsApiController : ControllerBase, IProductData
    {
        private readonly IProductData _productData;

        public ProductsApiController(IProductData productData) => _productData = productData;

        [HttpGet("brands")] // api/products/brands
        public IEnumerable<Brand> GetBrands() => _productData.GetBrands();

        [HttpGet("sections")] // api/products/sections
        public IEnumerable<Section> GetSections() => _productData.GetSections();

        [HttpPost]
        public IEnumerable<ProductDTO> GetProducts([FromBody] ProductFilter Filter = null) => _productData.GetProducts(Filter);

        [HttpGet("{id}")]
        public ProductDTO GetProductById(int id) => _productData.GetProductById(id);
    }
}
