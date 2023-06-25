using FoodDeliveryApi.Dtos.Order;
using FoodDeliveryApi.Models;

namespace FoodDeliveryApi.Dtos.User;

public class GetUserDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public ICollection<GetUserOrderDto> Orders { get; set; } = new List<GetUserOrderDto>();
}