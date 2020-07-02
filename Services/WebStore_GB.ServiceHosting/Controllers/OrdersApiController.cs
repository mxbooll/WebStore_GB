using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore_GB.Domain;
using WebStore_GB.Domain.DTO.Order;
using WebStore_GB.Interfaces.Services;

namespace WebStore_GB.ServiceHosting.Controllers
{
    [Route(WebApi.ORDERS)]
    [ApiController]
    public class OrdersApiController : ControllerBase, IOrderService
    {
        private readonly IOrderService _orderService;

        public OrdersApiController(IOrderService orderService) => _orderService = orderService;

        [HttpPost("{userName}")]
        public Task<OrderDTO> CreateOrder(string userName, [FromBody] CreateOrderModel orderModel) => _orderService.CreateOrder(userName, orderModel);

        [HttpGet("user/{userName}")]
        public Task<IEnumerable<OrderDTO>> GetUserOrders(string userName) => _orderService.GetUserOrders(userName);

        [HttpGet("{id}")]
        public Task<OrderDTO> GetOrderById(int id) => _orderService.GetOrderById(id);
    }
}
