using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDeliveryApi.DTOs
{
    public class OrderItem
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public int Quantity { get; set; } = 0;
    }
}