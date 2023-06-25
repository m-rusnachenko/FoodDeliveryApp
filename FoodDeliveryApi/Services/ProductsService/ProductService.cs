using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoodDeliveryApi.Data;
using FoodDeliveryApi.Dtos.Product;
using FoodDeliveryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryApi.Services.ProductsService
{
    public class ProductService : IProductService
    {
        private readonly FoodDeliveryDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductService(FoodDeliveryDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<GetProductDto>> AddProduct(Guid shopId, AddProductDto product)
        {
            var serviceResponse = new ServiceResponse<GetProductDto>();
            var productToAdd = _mapper.Map<Product>(product);
            var shop = await _dbContext.Shops.FindAsync(shopId);
            if (productToAdd.Shop is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Shop not found";
                return serviceResponse;
            }
            productToAdd.Shop = shop!;
            productToAdd.ShopId = shopId;
            await _dbContext.Products.AddAsync(productToAdd);
            await _dbContext.SaveChangesAsync();

            serviceResponse.Data = _mapper.Map<GetProductDto>(productToAdd);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProductDto>> AddProductById(Guid shopId, Guid productId)
        {
            var serviceResponse = new ServiceResponse<GetProductDto>();
            var product = await _dbContext.Products.FindAsync(productId);
            if (product is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Product not found";
                return serviceResponse;
            }
            var shop = await _dbContext.Shops.FindAsync(shopId);
            if (shop is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Shop not found";
                return serviceResponse;
            }
            product.Shop = shop!;
            product.ShopId = shopId;
            await _dbContext.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<GetProductDto>(product);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> GetProducts(Guid shopId)
        {
            var serviceResponse = new ServiceResponse<List<GetProductDto>>();
            var products = await _dbContext.Products.Where(p => p.ShopId == shopId).ToListAsync();
            serviceResponse.Data = _mapper.Map<List<GetProductDto>>(products);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> RemoveProduct(Guid shopId, Guid productId)
        {
            var serviceResponse = new ServiceResponse<List<GetProductDto>>();
            var shop = _dbContext.Shops.Include(s => s.Products).FirstOrDefault(s => s.Id == shopId);
            if (shop is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Shop not found";
                return serviceResponse;
            }
            var product = shop.Products.FirstOrDefault(p => p.Id == productId);
            if (product is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Product not found";
                return serviceResponse;
            }
            shop.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<List<GetProductDto>>(shop.Products);
            return serviceResponse;
        }
    }
}