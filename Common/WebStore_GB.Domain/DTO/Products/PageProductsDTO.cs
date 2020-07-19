using System.Collections.Generic;

namespace WebStore_GB.Domain.DTO.Products
{
    public class PageProductsDTO
    {
        public IEnumerable<ProductDTO> Products { get; set; }

        public int TotalCount { get; set; }
    }
}
