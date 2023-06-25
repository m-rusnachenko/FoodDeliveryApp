using System.Text.Json.Serialization;

namespace FoodDeliveryApi.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Role
{
    Admin,
    Customer,
    Manager
}