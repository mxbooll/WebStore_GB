using System.Collections.Generic;
using System.Linq;
using WebStore_GB.Domain.DTO.Products;
using WebStore_GB.Domain.Entities;
using WebStore_GB.Domain.ViewModels;

namespace WebStore_GB.Services.Mapping
{
    public static class ProductMapper
    {
        public static ProductViewModel ToView(this Product p) => new ProductViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Order = p.Order,
            Price = p.Price,
            ImageUrl = p.ImageUrl,
            Brand = p.Brand?.Name,
        };

        public static IEnumerable<ProductViewModel> ToView(this IEnumerable<Product> p) => p.Select(ToView);

        public static ProductDTO ToDTO(this Product p) => p is null
            ? null
            : new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Order = p.Order,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Barnd = p.Brand.ToDTO(),
                Section = p.Section.ToDTO()
            };

        public static Product FromDTO(this ProductDTO p) => p is null
            ? null
            : new Product
            {
                Id = p.Id,
                Name = p.Name,
                Order = p.Order,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                BrandId = p.Barnd?.Id,
                Brand = p.Barnd.FromDTO(),
                Section = p.Section.FromDTO()
            };
    }
}
