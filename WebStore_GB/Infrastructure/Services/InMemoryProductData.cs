using System.Collections.Generic;
using WebStore_GB.Data;
using WebStore_GB.Domain.Entities;
using WebStore_GB.Infrastructure.Interfaces;

namespace WebStore_GB.Infrastructure.Services
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Section> GetSections() => TestData.Sections;
        public IEnumerable<Brand> GetBrands() => TestData.Brands;
    }
}
