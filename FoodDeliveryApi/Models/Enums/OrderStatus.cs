using System.Text.Json.Serialization;

namespace FoodDeliveryApi.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderStatus
{
    Pending,
    InProgress,
    Completed,
    Cancelled
}