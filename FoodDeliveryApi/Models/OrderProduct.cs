using FoodDeliveryApi.Dtos.Product;

namespace FoodDeliveryApi.Models;

public class OrderProduct
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int Quantity { get; set; } = 0;
    public Product Product { get; set; } = null!;
}