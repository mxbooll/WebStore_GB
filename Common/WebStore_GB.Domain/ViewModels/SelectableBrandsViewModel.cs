using System.Collections.Generic;

namespace WebStore_GB.Domain.ViewModels
{
    public class SelectableBrandsViewModel
    {
        public IEnumerable<BrandViewModel> Brands { get; set; }

        public int? CurrentBrandId { get; set; }
    }
}
