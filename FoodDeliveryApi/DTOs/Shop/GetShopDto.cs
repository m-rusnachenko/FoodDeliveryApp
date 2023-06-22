using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodDeliveryApi.Models;
using FoodDeliveryApi.DTOs;

namespace FoodDeliveryApi.DTOs;

public class GetShopDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "Shop Name";
    public string Description { get; set; } = "Shop Description";
    public string ImageUrl { get; set; } = "https://via.placeholder.com/150";
    public string Address { get; set; } = "Shop Address";
    public ICollection<GetShopProductDto> Products { get; set; } = new List<GetShopProductDto>();

}