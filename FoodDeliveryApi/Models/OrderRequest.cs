using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDeliveryApi.Models
{
    public class OrderRequest
    {
        public Dictionary<Guid, int> Items { get; set; } = new Dictionary<Guid, int>();
    }
}