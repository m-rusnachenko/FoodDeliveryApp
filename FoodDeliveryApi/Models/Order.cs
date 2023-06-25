using System.ComponentModel.DataAnnotations.Schema;
using FoodDeliveryApi.Models.Enums;

namespace FoodDeliveryApi.Models;

public class Order : BaseClass
{
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public ICollection<OrderProduct> Products { get; set; } = new List<OrderProduct>();
    public User? User { get; set; }
    public Guid? UserId { get; set; }
    public Shop? Shop { get; set; }
    public Guid? ShopId { get; set; }
}