using FoodDeliveryApi.Dtos.Product;

namespace FoodDeliveryApi.Dtos.Order;

public class AddOrderDto
{
    public ICollection<AddOrderProductDto> Products { get; set; } = new List<AddOrderProductDto>();
    public Guid? UserId { get; set; }
    public Guid ShopId { get; set; }
    
}