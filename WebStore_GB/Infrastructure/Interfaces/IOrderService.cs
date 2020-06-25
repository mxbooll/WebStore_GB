using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore_GB.Domain.Entities.Orders;
using WebStore_GB.ViewModels;

namespace WebStore_GB.Infrastructure.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(string UserName, CartViewModel Cart, OrderViewModel OrderModel);

        Task<IEnumerable<Order>> GetUserOrders(string UserName);

        Task<Order> GetOrderById(int id);
    }
}
