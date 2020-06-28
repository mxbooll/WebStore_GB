using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore_GB.Domain.DTO.Order;
using WebStore_GB.Domain.Entities.Orders;

namespace WebStore_GB.Interfaces.Services
{
    public interface IOrderService
    {
        Task<OrderDTO> CreateOrder(string UserName, CreateOrderModel orderModel);

        Task<IEnumerable<OrderDTO>> GetUserOrders(string UserName);

        Task<OrderDTO> GetOrderById(int id);
    }
}
