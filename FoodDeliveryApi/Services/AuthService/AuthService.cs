using System.Security.Claims;
using AutoMapper;
using FoodDeliveryApi.Data;
using FoodDeliveryApi.Dtos.User;
using FoodDeliveryApi.Models;
using FoodDeliveryApi.Services.UserService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryApi.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;
    private readonly FoodDeliveryDbContext _context;
    private readonly IUserService _userService;

    public AuthService(IConfiguration config, IMapper mapper , FoodDeliveryDbContext context, IUserService userService)
    {
        _config = config;
        _mapper = mapper;
        _context = context;
        _userService = userService;
    }
    
    public async Task<ServiceResponse<GetUserDto>> Register(AddUserDto addUser)
    {
        return await _userService.CreateUser(addUser);
    }

    public async Task<ServiceResponse<ClaimsPrincipal>> Login(string email, string password)
    {
        var serviceResponse = new ServiceResponse<ClaimsPrincipal>();
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
        if (user is null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return new ServiceResponse<ClaimsPrincipal>
            {
                Success = false,
                Message = "Invalid credentials."
            };
        }

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };
        var userIdentity = new ClaimsIdentity(claims, "CookieAuth");
        var userPrincipal = new ClaimsPrincipal(userIdentity);
        serviceResponse.Data = userPrincipal;
        return serviceResponse;
    }
}