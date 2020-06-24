using WebStore_GB.Domain.Entities.Base.Interfaces;

namespace WebStore_GB.ViewModels
{
    public class BrandViewModel : INamedEntity, IOrderedEntity
    {
        public int Id { get; set; }

        public int Order { get; set; }

        public string Name { get; set; }
    }
}
