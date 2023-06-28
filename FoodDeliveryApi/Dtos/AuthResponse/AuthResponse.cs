using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FoodDeliveryApi.Dtos.User;

namespace FoodDeliveryApi.Dtos.AuthResponse
{
    public class AuthResponse
    {
        public ClaimsPrincipal? Principal { get; set; }
        public GetUserDto? User { get; set; }
        public string? Message { get; set; }
        public bool Success { get; set; } = true;
    }
}