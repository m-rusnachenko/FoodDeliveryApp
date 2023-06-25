using FoodDeliveryApi.Dtos.User;
using FoodDeliveryApi.Models;

namespace FoodDeliveryApi.Services.UserService;

public interface IUserService
{
    Task<ServiceResponse<GetUserDto>> GetUserById(Guid id);
    Task<bool> UserExists(Guid id);
    
    Task<ServiceResponse<GetUserDto>> CreateUser(AddUserDto user);
    Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserDto updatedUser);
    Task<ServiceResponse<GetUserDto>> DeleteUser(Guid id);
}