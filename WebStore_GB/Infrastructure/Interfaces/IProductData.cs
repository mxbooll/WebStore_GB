using System.Collections.Generic;
using WebStore_GB.Domain.Entities;

namespace WebStore_GB.Infrastructure.Interfaces
{
    public interface IProductData
    {
        IEnumerable<Section> GetSections();
        IEnumerable<Brand> GetBrands();
        IEnumerable<Product> GetProducts(ProductFilter Filter = null);
        Product GetProductById(int id);
    }
}
