using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDeliveryApi.DTOs;

public class GetShopProductDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "Product Name";
    public string Description { get; set; } = "Product Description";
    public string ImageUrl { get; set; } = "https://via.placeholder.com/150";
    public double Price { get; set; } = 0.0;
}