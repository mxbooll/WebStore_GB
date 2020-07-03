using WebStore_GB.Domain.Entities.Base.Interfaces;

namespace WebStore_GB.Domain.DTO.Products
{
    public class SectionDTO : INamedEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
