using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDeliveryApi.Dtos.Product;

public class AddOrderProductDto
{
    public int Quantity { get; set; } = 0;
    public Guid ProductId { get; set; }
}