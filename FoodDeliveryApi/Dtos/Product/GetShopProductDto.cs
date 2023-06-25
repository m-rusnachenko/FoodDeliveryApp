﻿namespace FoodDeliveryApi.Dtos.Product;

public class GetShopProductDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; } = 0.0;
    public string? ImageUrl { get; set; } = string.Empty;
}