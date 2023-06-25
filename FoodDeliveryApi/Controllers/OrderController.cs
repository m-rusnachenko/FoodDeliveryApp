using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodDeliveryApi.Dtos.Order;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("shop/{shopId}")]
    public async Task<IActionResult> GetShopOrders(Guid shopId)
    {
        var response = await _orderService.GetShopOrders(shopId);
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserOrders(Guid userId)
    {
        var response = await _orderService.GetUserOrders(userId);
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(AddOrderDto order)
    {
        var response = await _orderService.CreateOrder(order);
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateOrderStatus(Guid id)
    {
        var response = await _orderService.UpdateOrderStatus(id);
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPatch("{id}/cancel")]
    public async Task<IActionResult> CancelOrder(Guid id)
    {
        var response = await _orderService.CancelOrder(id);
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        var response = await _orderService.DeleteOrder(id);
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}