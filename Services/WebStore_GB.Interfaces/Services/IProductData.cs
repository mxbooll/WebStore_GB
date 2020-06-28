using System.Collections.Generic;
using WebStore_GB.Domain.DTO.Products;
using WebStore_GB.Domain.Entities;

namespace WebStore_GB.Interfaces.Services
{
    public interface IProductData
    {
        IEnumerable<Section> GetSections();
        IEnumerable<Brand> GetBrands();
        IEnumerable<ProductDTO> GetProducts(ProductFilter Filter = null);
        ProductDTO GetProductById(int id);
    }
}
