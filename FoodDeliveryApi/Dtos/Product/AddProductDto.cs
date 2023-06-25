namespace FoodDeliveryApi.Dtos.Product;

public class AddProductDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public double Price { get; set; }
}