using System.Security.Claims;
using AutoMapper;
using FoodDeliveryApi.Dtos.User;
using FoodDeliveryApi.Models;
using FoodDeliveryApi.Services.UserService;

namespace FoodDeliveryApi.Services.AuthService;

public interface IAuthService
{
    Task<ServiceResponse<GetUserDto>> Register(AddUserDto addUser);
    Task<ServiceResponse<ClaimsPrincipal>> Login(string email, string password);
}