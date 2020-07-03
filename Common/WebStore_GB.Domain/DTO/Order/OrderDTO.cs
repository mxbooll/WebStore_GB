using System;
using System.Collections.Generic;
using WebStore_GB.Domain.Entities.Base.Interfaces;

namespace WebStore_GB.Domain.DTO.Order
{
    public class OrderDTO : INamedEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Phone { get; set; }

        public string Address { get; set; }

        public DateTime Date { get; set; }

        public IEnumerable<OrderItemDTO> Items { get; set; }
    }
}
