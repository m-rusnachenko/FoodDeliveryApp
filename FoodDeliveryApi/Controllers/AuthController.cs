using System.Threading.Tasks;
using FoodDeliveryApi.Dtos.User;
using FoodDeliveryApi.Services.AuthService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
        
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] AddUserDto addUser)
    {
        var response = await _authService.Register(addUser);
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] string email, [FromForm] string password)
    {
        var response = await _authService.Login(email, password);
        if (!response.Success || response.Data is null)
        {
            return BadRequest(response);
        }
        await HttpContext.SignInAsync("CookieAuth", response.Data);
        return Ok(response);
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Ok();
    }
}