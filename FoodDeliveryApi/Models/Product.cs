using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryApi.Models;

public class Product : BaseClass
{
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(5000)]
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public Shop Shop { get; set; } = null!;
    [ForeignKey("Shop")]
    public Guid ShopId { get; set; }
}