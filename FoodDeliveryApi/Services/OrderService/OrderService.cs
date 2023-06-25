using System.Collections.ObjectModel;
using FoodDeliveryApi.Dtos.Order;
using FoodDeliveryApi.Models;
using FoodDeliveryApi.Models.Enums;

namespace FoodDeliveryApi.Services.OrderService;

public class OrderService : IOrderService
{
    private readonly FoodDeliveryDbContext _dbContext;
    private readonly IMapper _mapper;

    public OrderService(FoodDeliveryDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<List<GetShopOrderDto>>> GetShopOrders(Guid shopId)
    {
        var serviceResponse = new ServiceResponse<List<GetShopOrderDto>>();

        var orders = await _dbContext.Orders
            .Include(o => o.Products)
            .ThenInclude(op => op.Product)
            .IgnoreAutoIncludes()
            .Include(o => o.User)
            .IgnoreAutoIncludes()
            .Where(o => o.ShopId == shopId)
            .ToListAsync();

        serviceResponse.Data = _mapper.Map<List<GetShopOrderDto>>(orders);
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetUserOrderDto>>> GetUserOrders(Guid userId)
    {
        var serviceResponse = new ServiceResponse<List<GetUserOrderDto>>();

        var orders = await _dbContext.Orders
            .Include(o => o.Products)
            .ThenInclude(op => op.Product)
            .IgnoreAutoIncludes()
            .Include(o => o.Shop)
            .IgnoreAutoIncludes()
            .Where(o => o.UserId == userId)
            .ToListAsync();

        serviceResponse.Data = _mapper.Map<List<GetUserOrderDto>>(orders);
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetOrderDto>> CreateOrder(AddOrderDto order)
    {
        var serviceResponse = new ServiceResponse<GetOrderDto>();
        var shop = await _dbContext.Shops.FirstOrDefaultAsync(s => s.Id == order.ShopId);
        if (shop is null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Shop not found.";
            return serviceResponse;
        }

        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == order.UserId);
        if (user is null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "User not found.";
            return serviceResponse;
        }
        // Dont select product.shop
        var products = await _dbContext.Products
            .Where(p => order.Products.AsEnumerable().Select(op => op.ProductId).Contains(p.Id))
            .ToListAsync();

        var orderProducts = new Collection<OrderProduct>();
        foreach (var product in products)
        {
            var orderProduct = new OrderProduct
            {
                Product = product,
                Quantity = order.Products.First(p => p.ProductId == product.Id).Quantity
            };
            orderProducts.Add(orderProduct);
        }

        var newOrder = new Order
        {
            Shop = shop,
            User = user,
            Products = orderProducts,
        };

        await _dbContext.Orders.AddAsync(newOrder);
        await _dbContext.SaveChangesAsync();
        serviceResponse.Data = _mapper.Map<GetOrderDto>(newOrder);
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetOrderDto>> DeleteOrder(Guid id)
    {
        var serviceResponse = new ServiceResponse<GetOrderDto>();
        var order = await _dbContext.Orders
            .Include(o => o.Products)
            .ThenInclude(op => op.Product)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order is null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Order not found.";
            return serviceResponse;
        }

        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == order.UserId);
        user?.Orders.Remove(order);

        var shop = await _dbContext.Shops.FirstOrDefaultAsync(s => s.Id == order.ShopId);
        shop?.Orders.Remove(order);
        _dbContext.Orders.Remove(order);
        await _dbContext.SaveChangesAsync();
        serviceResponse.Data = _mapper.Map<GetOrderDto>(order);
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetOrderDto>> UpdateOrderStatus(Guid id)
    {
        var serviceResponse = new ServiceResponse<GetOrderDto>();
        var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);

        if (order is null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Order not found.";
            return serviceResponse;
        }

        switch (order.Status)
        {
            case OrderStatus.Pending:
                order.Status = OrderStatus.InProgress;
                break;
            case OrderStatus.InProgress:
                order.Status = OrderStatus.Completed;
                break;
        }

        await _dbContext.SaveChangesAsync();
        serviceResponse.Data = _mapper.Map<GetOrderDto>(order);
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetOrderDto>> CancelOrder(Guid id)
    {
        var serviceResponse = new ServiceResponse<GetOrderDto>();
        var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);

        if (order is null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Order not found.";
            return serviceResponse;
        }

        order.Status = OrderStatus.Cancelled;
        await _dbContext.SaveChangesAsync();
        serviceResponse.Data = _mapper.Map<GetOrderDto>(order);
        return serviceResponse;
    }
}
