using WebStore_GB.Domain.Entities.Base.Interfaces;

namespace WebStore_GB.Domain.Entities.Base
{
    public abstract class NamedEntity : BaseEntity, INamedEntity
    {
        public string Name { get; set; }
    }
}
