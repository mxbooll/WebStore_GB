using System;
using System.Collections.Generic;
using System.Text;
using WebStore_GB.Domain.Entities.Base.Interfaces;

namespace WebStore_GB.Domain.DTO.Products
{
    public class ProductDTO : INamedEntity, IOrderedEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public BrandDTO Barnd { get; set; }

        public SectionDTO Section { get; set; }
    }
}
