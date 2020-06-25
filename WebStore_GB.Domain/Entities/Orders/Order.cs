using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebStore_GB.Domain.Entities.Base;
using WebStore_GB.Domain.Entities.Identity;

namespace WebStore_GB.Domain.Entities.Orders
{
    public class Order : NamedEntity
    {
        [Required]
        public virtual User User { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<OrderItem> Items { get; set; }
    }
}
