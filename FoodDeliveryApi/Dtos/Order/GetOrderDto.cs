using FoodDeliveryApi.Dtos.Product;
using FoodDeliveryApi.Dtos.Shop;
using FoodDeliveryApi.Dtos.User;
using FoodDeliveryApi.Models;
using FoodDeliveryApi.Models.Enums;

namespace FoodDeliveryApi.Dtos.Order;

public class GetOrderDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public ICollection<GetOrderProductDto> Products { get; set; } = new List<GetOrderProductDto>();
    public GetUserMinimalDto User { get; set; } = null!;
    public GetShopMinimalDto Shop { get; set; } = null!;
    public double TotalPrice => Math.Round(Products.Sum(x => x.Product.Price * x.Quantity), 2);
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}