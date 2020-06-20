using System;
using System.Collections.Generic;
using System.Linq;
using WebStore_GB.Domain.Entities;
using WebStore_GB.ViewModels;

namespace WebStore_GB.Infrastructure.Mapping
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
        };

        public static IEnumerable<ProductViewModel> ToView(this IEnumerable<Product> p) => p.Select(ToView);

        internal static Func<Product, int, object> ToView()
        {
            throw new NotImplementedException();
        }
    }
}
