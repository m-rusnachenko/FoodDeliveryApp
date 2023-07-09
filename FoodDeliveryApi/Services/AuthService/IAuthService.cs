using System.Security.Claims;
using AutoMapper;
using FoodDeliveryApi.Dtos.AuthResponse;
using FoodDeliveryApi.Dtos.User;
using FoodDeliveryApi.Models;
using FoodDeliveryApi.Services.UserService;

namespace FoodDeliveryApi.Services.AuthService;

public interface IAuthService
{
    Task<ServiceResponse<GetUserDto>> Register(AddUserDto addUser);
    Task<AuthResponse> Login(string email, string password);
    Task<ServiceResponse<GetUserDto>> GetUser(ClaimsPrincipal user);
}