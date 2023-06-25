using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodDeliveryApi.Dtos.Order;
using FoodDeliveryApi.Dtos.Product;
using FoodDeliveryApi.Dtos.Shop;
using FoodDeliveryApi.Dtos.User;
using FoodDeliveryApi.Models;

namespace FoodDeliveryApi;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, GetUserDto>();
        CreateMap<AddUserDto, User>();
        CreateMap<UpdateUserDto, User>();
        CreateMap<User, GetShopManagerDto>();
        CreateMap<User, GetUserMinimalDto>();

        
        CreateMap<Shop, GetShopDto>();
        CreateMap<AddShopDto, Shop>();
        CreateMap<UpdateShopDto, Shop>();
        CreateMap<Shop, GetShopMinimalDto>();
        
        
        CreateMap<Product, GetProductDto>();
        CreateMap<AddProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
        CreateMap<Product, GetShopProductDto>();
        
        CreateMap<Order, GetOrderDto>();
        CreateMap<AddOrderDto, Order>();
        CreateMap<Order, GetUserOrderDto>();
        CreateMap<Order, GetShopOrderDto>();

        CreateMap<OrderProduct, GetOrderProductDto>();
        CreateMap<AddOrderProductDto, OrderProduct>();
    }
}