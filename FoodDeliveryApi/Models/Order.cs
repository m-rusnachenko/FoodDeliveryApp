using FoodDeliveryApi.DTOs;

namespace FoodDeliveryApi.Models;

public class Order : BaseClass
{
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public Guid UserId { get; set; } = Guid.Empty;
    public Guid ShopId { get; set; } = Guid.Empty;
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
}