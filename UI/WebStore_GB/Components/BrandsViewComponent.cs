using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore_GB.Domain.ViewModels;
using WebStore_GB.Interfaces.Services;

namespace WebStore_GB.Components
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductData _productData;

        public BrandsViewComponent(IProductData productData) => _productData = productData;

        public IViewComponentResult Invoke(string brandId) => View(new SelectableBrandsViewModel
        {
            Brands = GetBrands(),
            CurrentBrandId = int.TryParse(brandId, out var id) ? id : (int?)null
        });

        //public async Task<IViewComponentResult> Invoke() => View();

        private IEnumerable<BrandViewModel> GetBrands()
        {
            return _productData.GetBrands()
                    .Select(brand => new BrandViewModel
                    {
                        Id = brand.Id,
                        Name = brand.Name,
                        Order = brand.Order
                    })
                    .OrderBy(brand => brand.Order);
        }
    }
}
