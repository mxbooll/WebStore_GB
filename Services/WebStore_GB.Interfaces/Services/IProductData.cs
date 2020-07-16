using System.Collections.Generic;
using WebStore_GB.Domain.DTO.Products;
using WebStore_GB.Domain.Entities;

namespace WebStore_GB.Interfaces.Services
{
    public interface IProductData
    {
        IEnumerable<Section> GetSections();

        Section GetSection(int id);

        IEnumerable<Brand> GetBrands();

        Brand GetBrand(int id);// => GetBrands().FirstOrDefault(b => b.Id == Id);

        IEnumerable<ProductDTO> GetProducts(ProductFilter filter = null);

        ProductDTO GetProductById(int id);
    }
}
