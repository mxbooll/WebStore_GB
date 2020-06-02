namespace WebStore_GB.Domain.Entities.Base
{
    public interface IOrderedEntity : IBaseEntity
    {
        int Order { get; set; }
    }
}
