using KhumaloCraft.Business.Interfaces;
using KhumaloCraft.Business.Services;
using KhumaloCraft.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KhumaloCraft.BusinessAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class OrdersController : ControllerBase
  {
    private readonly IOrderService _orderService;
    private readonly IFunctionTriggerService _functionTriggerService;

    public OrdersController(IOrderService orderService, IFunctionTriggerService functionTriggerService)
    {
      _orderService = orderService;
      _functionTriggerService = functionTriggerService;
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserOrders(string userId)
    {
      var orders = await _orderService.GetOrdersByUserIdAsync(userId);

      if (orders == null || !orders.Any())
      {
        return NotFound("No orders found for this user.");
      }

      return Ok(orders);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllOrders()
    {
      var orders = await _orderService.GetAllOrders();
      return Ok(orders);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateOrder([FromBody] CartRequestDTO cartRequestDTO)
    {
      Console.WriteLine($"Received CartId: {cartRequestDTO.CartId}");
      try
      {
        var response = await _functionTriggerService.StartOrderProcessingOrchestratorAsync(cartRequestDTO);

        return Ok(new { message = "Orchestrator started", details = response });
      }
      catch (Exception ex)
      {
        return StatusCode(500, new { error = ex.Message });
      }
    }

    [HttpPut("{orderId}/status")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStatus(int orderId, [FromBody] StatusDTO statusDTO)
    {
      await _orderService.UpdateOrderStatusAsync(orderId, statusDTO.StatusId);
      return Ok($"Order status updated successfully.");
    }
  }
}
