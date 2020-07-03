﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore_GB.Domain.DTO.Order;
using WebStore_GB.Domain.Entities.Identity;
using WebStore_GB.Domain.Entities.Orders;
using WebStore_GB.Interfaces.Services;
using WebStore_GB.Services.Mapping;
using WevStore_GB.DAL.Context;

namespace WebStore_GB.Services.Products.InSQL
{
    public class SqlOrderService : IOrderService
    {
        private readonly WebStoreDB _db;
        private readonly UserManager<User> _userManager;

        public SqlOrderService(WebStoreDB db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<OrderDTO> CreateOrder(string userName, CreateOrderModel orderModel)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null) throw new InvalidOperationException($"Пользователь {userName} не найден в БД");

            await using var transaction = await _db.Database.BeginTransactionAsync();

            var order = new Order
            {
                Name = orderModel.Order.Name,
                Address = orderModel.Order.Address,
                Phone = orderModel.Order.Phone,
                User = user,
                Date = DateTime.Now,
                Items = new List<OrderItem>()
            };

            foreach (var item in orderModel.Items)
            {
                var product = await _db.Products.FindAsync(item.Id);
                if (product is null) throw new InvalidOperationException($"Товар id:{item.Id} не найден в БД");

                var order_item = new OrderItem
                {
                    Order = order,
                    Price = product.Price,
                    Quantity = item.Quantity,
                    Product = product
                };
                order.Items.Add(order_item);
            }

            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();
            await transaction.CommitAsync();

            return order.ToDTO();
        }

        public async Task<IEnumerable<OrderDTO>> GetUserOrders(string UserName) => (await _db.Orders
           .Include(order => order.User)
           .Include(order => order.Items)
           .Where(order => order.User.UserName == UserName)
           .ToArrayAsync())
           .Select(p => p.ToDTO());

        public async Task<OrderDTO> GetOrderById(int id) => (await _db.Orders
           .Include(order => order.Items)
           .FirstOrDefaultAsync(order => order.Id == id))
           .ToDTO();
    }
}
