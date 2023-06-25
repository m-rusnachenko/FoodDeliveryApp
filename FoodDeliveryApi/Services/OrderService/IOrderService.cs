using FoodDeliveryApi.Dtos.Order;
using FoodDeliveryApi.Models;

namespace FoodDeliveryApi.Services.OrderService;

public interface IOrderService
{
    Task<ServiceResponse<List<GetShopOrderDto>>> GetShopOrders(Guid shopId);
    Task<ServiceResponse<List<GetUserOrderDto>>> GetUserOrders(Guid userId);
    Task<ServiceResponse<GetOrderDto>> CreateOrder(AddOrderDto order);
    Task<ServiceResponse<GetOrderDto>> UpdateOrderStatus(Guid id);
    Task<ServiceResponse<GetOrderDto>> CancelOrder(Guid id);
    Task<ServiceResponse<GetOrderDto>> DeleteOrder(Guid id);
}