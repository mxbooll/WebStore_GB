using System.Collections.Generic;
using System.Linq;
using WebStore_GB.Domain.Entities;
using WebStore_GB.Interfaces.Services;
using WebStore_GB.Services.Data;

namespace WebStore_GB.Services.Products.InMemory
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Section> GetSections() => TestData.Sections;
        public IEnumerable<Brand> GetBrands() => TestData.Brands;
        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            var query = TestData.Products;

            if (Filter?.SectionId != null)
                query = query.Where(product => product.SectionId == Filter.SectionId);

            if (Filter?.BrandId != null)
                query = query.Where(product => product.BrandId == Filter.BrandId);

            return query;
        }

        public Product GetProductById(int id) => TestData.Products.FirstOrDefault(p => p.Id == id);
    }
}
