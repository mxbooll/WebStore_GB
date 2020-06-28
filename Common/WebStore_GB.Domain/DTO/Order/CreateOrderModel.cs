using System.Collections.Generic;
using WebStore_GB.Domain.ViewModels;

namespace WebStore_GB.Domain.DTO.Order
{
    public class CreateOrderModel
    {
        public OrderViewModel Order { get; set; }

        public List<OrderItemDTO> Items { get; set; }
    }
}
