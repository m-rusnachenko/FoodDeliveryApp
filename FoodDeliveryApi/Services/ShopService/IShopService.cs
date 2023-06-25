using FoodDeliveryApi.Dtos.Order;
using FoodDeliveryApi.Dtos.Product;
using FoodDeliveryApi.Dtos.Shop;
using FoodDeliveryApi.Models;

namespace FoodDeliveryApi.Services.ShopService;

public interface IShopService
{
    Task<ServiceResponse<List<GetShopDto>>> GetAllShops();
    Task<ServiceResponse<GetShopDto>> GetShopById(Guid id);
    Task<ServiceResponse<GetShopDto>> CreateShop(AddShopDto shop);
    Task<ServiceResponse<GetShopDto>> UpdateShop(Guid shopId, UpdateShopDto shop);
    Task<ServiceResponse<GetShopDto>> DeleteShop(Guid id);
    Task<ServiceResponse<GetShopDto>> AddManager(Guid shopId, Guid userId);
    Task<ServiceResponse<GetShopDto>> RemoveManager(Guid shopId, Guid userId);
}