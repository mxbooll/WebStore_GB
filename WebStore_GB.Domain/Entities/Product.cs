using WebStore_GB.Domain.Entities.Base;
using WebStore_GB.Domain.Entities.Base.Interfaces;

namespace WebStore_GB.Domain.Entities
{
    public class Product : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int SectionId { get; set; }

        public int? BrandId { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }
    }
}
