using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryApi.Models;

public class Shop : BaseClass
{
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(5000)]
    public string Description { get; set; } = string.Empty;
    [MaxLength(500)]
    public string Address { get; set; } = string.Empty;
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public ICollection<Product> Products { get; set; } = new List<Product>();
    public ICollection<User> Managers { get; set; } = new List<User>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}