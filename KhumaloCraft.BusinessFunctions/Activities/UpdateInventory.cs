using KhumaloCraft.Business.Interfaces;
using KhumaloCraft.Shared.DTOs;
using Microsoft.Azure.Functions.Worker;

namespace KhumaloCraft.BusinessFunctions.Activities;

public class UpdateInventoryActivity
{
  private readonly IProductService _productService;

  public UpdateInventoryActivity(IProductService productService)
  {
    _productService = productService;
  }

  [Function("UpdateInventory")]
  public async Task<string> Run([ActivityTrigger] CartDTO cartDTO)
  {
    if (cartDTO == null || cartDTO.Items.Count == 0)
    {
      Console.WriteLine("No items found in cart for CartId: {0}", cartDTO.CartId);
      return "Cart not found or empty.";
    }

    try
    {
      await _productService.UpdateInventory(cartDTO.Items);
      return "Inventory updated successfully.";
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error updating inventory: {ex.Message}");
      throw;
    }
  }
}
