using FoodDeliveryApi.Dtos.Product;
using FoodDeliveryApi.Models;

namespace FoodDeliveryApi.Services.ProductService;

public interface ProductService
{
    Task<ServiceResponse<List<GetProductDto>>> GetAllProducts();
    Task<ServiceResponse<GetProductDto>> GetProductById(Guid id);
    Task<ServiceResponse<GetProductDto>> UpdateProduct(Guid id, UpdateProductDto product);
    Task<ServiceResponse<GetProductDto>> DeleteProduct(Guid id);
    Task<ServiceResponse<GetProductDto>> AddProduct(Guid shopId, AddProductDto product);
    Task<ServiceResponse<GetProductDto>> RemoveProduct(Guid shopId, Guid productId);
}