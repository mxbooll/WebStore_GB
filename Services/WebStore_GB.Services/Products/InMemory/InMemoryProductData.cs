using System.Collections.Generic;
using System.Linq;
using WebStore_GB.Domain.DTO.Products;
using WebStore_GB.Domain.Entities;
using WebStore_GB.Interfaces.Services;
using WebStore_GB.Services.Data;
using WebStore_GB.Services.Mapping;

namespace WebStore_GB.Services.Products.InMemory
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Section> GetSections() => TestData.Sections;
        public IEnumerable<Brand> GetBrands() => TestData.Brands;
        public PageProductsDTO GetProducts(ProductFilter Filter = null)
        {
            var query = TestData.Products;

            if (Filter?.SectionId != null)
                query = query.Where(product => product.SectionId == Filter.SectionId);

            if (Filter?.BrandId != null)
                query = query.Where(product => product.BrandId == Filter.BrandId);

            var totalCount = query.Count();

            if (Filter?.PageSize > 0)
            {
                query = query
                    .Skip((Filter.Page - 1) * (int)Filter.PageSize)
                    .Take((int)Filter.PageSize);
            }

            return new PageProductsDTO
            {
                Products = query.Select(p => p.ToDTO()),
                TotalCount = totalCount
            };
        }

        public ProductDTO GetProductById(int id) => TestData.Products.FirstOrDefault(p => p.Id == id).ToDTO();

        public Section GetSection(int id) => TestData.Sections.FirstOrDefault(s => s.Id == id);

        public Brand GetBrand(int id) => TestData.Brands.FirstOrDefault(b => b.Id == id);
    }
}
