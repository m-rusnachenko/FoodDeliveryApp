namespace FoodDeliveryApi.Dtos.Product;

public class GetOrderProductDto
{
    public int Quantity { get; set; } = 0;
    public GetShopProductDto Product { get; set; } = null!;
}