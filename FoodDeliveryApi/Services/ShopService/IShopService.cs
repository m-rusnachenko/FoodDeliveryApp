using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodDeliveryApi.Models;
using FoodDeliveryApi.DTOs;


namespace FoodDeliveryApi.Services.ShopService
{
    public interface IShopService
    {
        Task<ServiceResponse<List<GetShopDto>>> GetAllShops();
    }
}