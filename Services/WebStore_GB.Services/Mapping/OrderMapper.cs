using System.Linq;
using WebStore_GB.Domain.DTO.Order;
using WebStore_GB.Domain.Entities.Orders;

namespace WebStore_GB.Services.Mapping
{
    public static class OrderMapper
    {
        public static OrderDTO ToDTO(this Order order) => order is null ? null : new OrderDTO
        {
            Id = order.Id,
            Phone = order.Phone,
            Address = order.Address,
            Date = order.Date,
            Items = order.Items.Select(ToDTO)
        };

        public static OrderItemDTO ToDTO(this OrderItem item) => item is null ? null : new OrderItemDTO
        {
            Id = item.Id,
            Price = item.Price,
            Quantity = item.Quantity
        };

        public static Order FromDTO(this OrderDTO order) => order is null ? null : new Order
        {
            Id = order.Id,
            Phone = order.Phone,
            Address = order.Address,
            Date = order.Date,
            Items = order.Items.Select(FromDTO).ToArray()
        };

        public static OrderItem FromDTO(this OrderItemDTO item) => item is null ? null : new OrderItem
        {
            Id = item.Id,
            Price = item.Price,
            Quantity = item.Quantity
        };
    }
}
