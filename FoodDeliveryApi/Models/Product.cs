namespace FoodDeliveryApi.Models;

public class Product : BaseClass
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0;
    public string ImageUrl { get; set; } = string.Empty;
    public Guid ShopId { get; set; } = Guid.Empty;
}