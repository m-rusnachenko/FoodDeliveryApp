using System.Security.Claims;
using System.Threading.Tasks;
using FoodDeliveryApi.Dtos.User;
using FoodDeliveryApi.Services.AuthService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<ActionResult> Login([FromForm] string email, [FromForm] string password)
    {
        var response = await _authService.Login(email, password);
        if (!response.Success || response.Principal is null)
        {
            return BadRequest(response);
        }
        // HttpContext.SignInAsync("CookieAuth", response.Data);
        // Call SignInAsync and recive the response
        await HttpContext.SignInAsync(
            "CookieAuth",
            response.Principal,
            new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(60)
            });

        return Ok(response.User);
    }
    
    [Authorize(Policy = "Manager")]
    [HttpGet("user")]
    public IActionResult GetUser()
    {
        var user = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
        return Ok(user ?? "User not found");
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Ok();
    }
}