using WebStore_GB.Domain.Entities.Base;
using WebStore_GB.Domain.Entities.Base.Interfaces;

namespace WebStore_GB.Domain.Entities
{
    public class Section : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int? ParentId { get; set; }
    }
}
