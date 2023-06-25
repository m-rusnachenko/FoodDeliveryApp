using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodDeliveryApi.Dtos.Product;
using FoodDeliveryApi.Dtos.Shop;
using FoodDeliveryApi.Models;

namespace FoodDeliveryApi.Services.ProductsService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<GetProductDto>>> GetProducts(Guid shopId);
        Task<ServiceResponse<GetProductDto>> AddProduct(Guid shopId, AddProductDto product);
        Task<ServiceResponse<GetProductDto>> AddProductById(Guid shopId, Guid productId);
        Task<ServiceResponse<List<GetProductDto>>> RemoveProduct(Guid shopId, Guid productId);
    }
}