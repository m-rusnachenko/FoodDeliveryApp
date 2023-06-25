using AutoMapper;
using FoodDeliveryApi.Data;
using FoodDeliveryApi.Dtos.User;
using FoodDeliveryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryApi.Services.UserService;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly FoodDeliveryDbContext _context;

    public UserService(IMapper mapper, FoodDeliveryDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    
    public async Task<ServiceResponse<List<GetUserDto>>> GetAllUsers()
    {
        var serviceResponse = new ServiceResponse<List<GetUserDto>>();
        serviceResponse.Data = _mapper.
            Map<List<GetUserDto>>(await _context.Users.AsNoTracking().ToListAsync());

        return serviceResponse;
    }

    public async Task<ServiceResponse<GetUserDto>> GetUserById(Guid id)
    {
        var serviceResponse = new ServiceResponse<GetUserDto>();
        serviceResponse.Data = _mapper.
            Map<GetUserDto>(await _context.Users
                .Include(u => u.Orders)
                .ThenInclude(o => o.Products)
                .FirstOrDefaultAsync(u => u.Id == id));
        if (serviceResponse.Data is null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "User not found.";
        }

        return serviceResponse;
    }

    Task<bool> IUserService.UserExists(Guid id)
    {
        return _context.Users.AnyAsync(u => u.Id == id);
    }

    public async Task<ServiceResponse<GetUserDto>> CreateUser(AddUserDto user)
    {
        var serviceResponse = new ServiceResponse<GetUserDto>();
        
        if (await _context.Users.AnyAsync(u => u.Email == user.Email))
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Email already exists.";
            return serviceResponse;
        }
        var newUser = _mapper.Map<User>(user);
        newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();
        serviceResponse.Data = _mapper.Map<GetUserDto>(newUser);

        return serviceResponse;
    }

    public async Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserDto updatedUser)
    {
        var serviceResponse = new ServiceResponse<GetUserDto>();
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == updatedUser.Id);
        if (user is null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "User not found.";
            return serviceResponse;
        }
        user.FirstName = updatedUser.FirstName ?? user.FirstName;
        user.LastName = updatedUser.LastName ?? user.LastName;
        user.Email = updatedUser.Email ?? user.Email;
        user.PhoneNumber = updatedUser.PhoneNumber ?? user.PhoneNumber;
        
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        serviceResponse.Data = _mapper.Map<GetUserDto>(user);
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetUserDto>> DeleteUser(Guid id)
    {
        var serviceResponse = new ServiceResponse<GetUserDto>();
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user is null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "User not found.";
            return serviceResponse;
        }
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        serviceResponse.Data = _mapper.Map<GetUserDto>(user);
        return serviceResponse;
    }

}