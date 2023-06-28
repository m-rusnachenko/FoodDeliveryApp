using System.Text.Json.Serialization;

namespace FoodDeliveryApi.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Role
{
    Customer = 1,
    Manager = 2,
    Admin = 3
}