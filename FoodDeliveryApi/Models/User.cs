using System.ComponentModel.DataAnnotations;
using FoodDeliveryApi.Models.Enums;

namespace FoodDeliveryApi.Models;

public class User : BaseClass
{
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;
    [MaxLength(100)]
    public string FullName => $"{FirstName} {LastName}";
    
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;
    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public Role Role { get; set; } = Role.Customer;
    
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}