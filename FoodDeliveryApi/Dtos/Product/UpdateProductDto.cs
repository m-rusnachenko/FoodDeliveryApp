namespace FoodDeliveryApi.Dtos.Product;

public class UpdateProductDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public double? Price { get; set; }
}