using FoodDeliveryApi.Dtos.Shop;
using FoodDeliveryApi.Models;


namespace FoodDeliveryApi.Services.ShopService;

public class ShopService : IShopService
{
    private FoodDeliveryDbContext _dbContext;
    private IMapper _mapper;

    public ShopService(FoodDeliveryDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<ServiceResponse<List<GetShopDto>>> GetAllShops()
    {
        var serviceResponse = new ServiceResponse<List<GetShopDto>>();
        serviceResponse.Data = await _dbContext.Shops.AsNoTracking()
            .Include(s => s.Products).AsNoTracking()
            .Select(s => _mapper.Map<GetShopDto>(s))
            .ToListAsync();
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetShopDto>> GetShopById(Guid id)
    {
        var serviceResponse = new ServiceResponse<GetShopDto>();
        serviceResponse.Data = _mapper
            .Map<GetShopDto>(await _dbContext.Shops
                .Include(s => s.Products)
                .Include(s => s.Managers)
                .FirstOrDefaultAsync(s => s.Id == id));
        if (serviceResponse.Data is null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Shop not found.";
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<GetShopDto>> CreateShop(AddShopDto shop)
    {
        var serviceResponse = new ServiceResponse<GetShopDto>();
        var newShop = _mapper.Map<Shop>(shop);
        await _dbContext.Shops.AddAsync(newShop);
        await _dbContext.SaveChangesAsync();
        serviceResponse.Data = _mapper.Map<GetShopDto>(newShop);
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetShopDto>> UpdateShop(Guid shopId, UpdateShopDto shop)
    {
        var serviceResponse = new ServiceResponse<GetShopDto>();
        var shopToUpdate = await _dbContext.Shops.FirstOrDefaultAsync(s => s.Id == shopId);
        if (shopToUpdate is null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Shop not found.";
            return serviceResponse;
        }

        shopToUpdate.Name = shop.Name ?? shopToUpdate.Name;
        shopToUpdate.Description = shop.Description ?? shopToUpdate.Description;
        shopToUpdate.ImageUrl = shop.ImageUrl ?? shopToUpdate.ImageUrl;
        shopToUpdate.Address = shop.Address ?? shopToUpdate.Address;
        shopToUpdate.Email = shop.Email ?? shopToUpdate.Email;

        _dbContext.Shops.Update(shopToUpdate);
        await _dbContext.SaveChangesAsync();
        serviceResponse.Data = _mapper.Map<GetShopDto>(shopToUpdate);
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetShopDto>> DeleteShop(Guid id)
    {
        var serviceResponse = new ServiceResponse<GetShopDto>();
        var shopToDelete = await _dbContext.Shops.FirstOrDefaultAsync(s => s.Id == id);
        if (shopToDelete is null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Shop not found.";
            return serviceResponse;
        }

        _dbContext.Shops.Remove(shopToDelete);
        await _dbContext.SaveChangesAsync();
        serviceResponse.Data = _mapper.Map<GetShopDto>(shopToDelete);
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetShopDto>> AddManager(Guid shopId, Guid userId)
    {
        var serviceResponse = new ServiceResponse<GetShopDto>();
        var shop = await _dbContext.Shops.Include(s => s.Managers).FirstOrDefaultAsync(s => s.Id == shopId);
        if (shop is null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Shop not found.";
            return serviceResponse;
        }

        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "User not found.";
            return serviceResponse;
        }

        if (shop.Managers.Any(m => m.Id == userId))
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "User is already a manager.";
            return serviceResponse;
        }
        shop.Managers.Add(user);
        await _dbContext.SaveChangesAsync();
        // serviceResponse.Data = _mapper.Map<GetShopDto>(shop);
        serviceResponse.Data = _mapper
            .Map<GetShopDto>(await _dbContext.Shops
                .Include(s => s.Managers)
                .FirstOrDefaultAsync(s => s.Id == shopId));
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetShopDto>> RemoveManager(Guid shopId, Guid userId)
    {
        var serviceResponse = new ServiceResponse<GetShopDto>();
        var shop = await _dbContext.Shops.Include(s => s.Managers).FirstOrDefaultAsync(s => s.Id == shopId);
        if (shop is null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Shop not found.";
            return serviceResponse;
        }

        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "User not found.";
            return serviceResponse;
        }

        shop.Managers.Remove(user);
        await _dbContext.SaveChangesAsync();
        serviceResponse.Data = _mapper.Map<GetShopDto>(shop);
        return serviceResponse;
    }
}