using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore_GB.Domain.DTO.Order;

namespace WebStore_GB.Interfaces.Services
{
    public interface IOrderService
    {
        Task<OrderDTO> CreateOrder(string userName, CreateOrderModel orderModel);

        Task<IEnumerable<OrderDTO>> GetUserOrders(string userName);

        Task<OrderDTO> GetOrderById(int id);
    }
}
