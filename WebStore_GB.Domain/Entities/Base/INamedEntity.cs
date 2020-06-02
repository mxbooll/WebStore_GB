namespace WebStore_GB.Domain.Entities.Base
{
    public interface INamedEntity : IBaseEntity
    {
        string Name { get; set; }
    }
}
