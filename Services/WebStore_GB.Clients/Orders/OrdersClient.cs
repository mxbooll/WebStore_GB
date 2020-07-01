using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebStore_GB.Clients.Base;
using WebStore_GB.Domain;
using WebStore_GB.Domain.DTO.Order;
using WebStore_GB.Interfaces.Services;

namespace WebStore_GB.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrderService
    {
        public OrdersClient(IConfiguration configuration) : base(configuration, WebApi.ORDERS) { }

        public async Task<OrderDTO> CreateOrder(string userName, CreateOrderModel orderModel)
        {
            var response = await PostAsync($"{_serviceAddress}/{userName}", orderModel);
            return await response.Content.ReadAsAsync<OrderDTO>();
        }

        public async Task<OrderDTO> GetOrderById(int id) => await GetAsync<OrderDTO>($"{_serviceAddress}/{id}");

        public async Task<IEnumerable<OrderDTO>> GetUserOrders(string userName) => await GetAsync<IEnumerable<OrderDTO>>($"{_serviceAddress}/user/{userName}");
    }
}
