using WebStore_GB.Domain.Entities.Base.Interfaces;

namespace WebStore_GB.Domain.DTO.Order
{
    public class OrderItemDTO : IBaseEntity
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
