using WebStore_GB.Domain.Entities.Base.Interfaces;

namespace WebStore_GB.Domain.Entities.Base
{
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
    }
}
