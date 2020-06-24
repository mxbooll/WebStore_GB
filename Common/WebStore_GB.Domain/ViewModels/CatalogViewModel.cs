
using System.Collections.Generic;

namespace WebStore_GB.Domain.ViewModels
{
    public class CatalogViewModel
    {
        public int? BrandId { get; set; }

        public int? SectionId { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
