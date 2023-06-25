using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodDeliveryApi.Dtos.Product;
using FoodDeliveryApi.Dtos.User;
using FoodDeliveryApi.Models.Enums;

namespace FoodDeliveryApi.Dtos.Order;

public class GetShopOrderDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public ICollection<GetOrderProductDto> Products { get; set; } = new List<GetOrderProductDto>();
    public GetUserMinimalDto User { get; set; } = new GetUserMinimalDto();
    public double TotalPrice => Math.Round(Products.Sum(x => x.Product.Price * x.Quantity), 2);
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}