using FoodDeliveryApi.Dtos.Product;
using FoodDeliveryApi.Dtos.User;

namespace FoodDeliveryApi.Dtos.Shop;

public class GetShopDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public ICollection<GetShopProductDto> Products { get; set; } = new List<GetShopProductDto>();
    public ICollection<GetShopManagerDto> Managers { get; set; } = new List<GetShopManagerDto>();
}