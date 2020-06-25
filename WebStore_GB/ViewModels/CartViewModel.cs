﻿using System.Collections.Generic;
using System.Linq;

namespace WebStore_GB.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<(ProductViewModel Product, int Quantity)> Items { get; set; }

        public int ItemsCount => Items?.Sum(item => item.Quantity) ?? 0;
    }
}
