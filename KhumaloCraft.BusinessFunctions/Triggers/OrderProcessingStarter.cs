using KhumaloCraft.Shared.DTOs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;


namespace KhumaloCraft.BusinessFunctions.OrderProcessing;

public class OrderProcessingStarter
{
  private readonly ILogger<OrderProcessingStarter> _logger;
  public OrderProcessingStarter(ILogger<OrderProcessingStarter> logger)
  {
    _logger = logger;
  }

  [Function("StartOrderOrchestration")]
  public async Task<HttpResponseData> InitiateOrder([HttpTrigger(AuthorizationLevel.Function, "post", Route = "start-order-orchestration")] HttpRequestData req, [DurableClient] DurableTaskClient client)
  {
    _logger.LogInformation("Starting the Order orchestration.");

    var cartRequestDTO = await req.ReadFromJsonAsync<CartRequestDTO>();
    if (cartRequestDTO == null || string.IsNullOrEmpty(cartRequestDTO.CartId))
    {
      var badResponse = req.CreateResponse(System.Net.HttpStatusCode.BadRequest);
      await badResponse.WriteStringAsync("Invalid or missing cartId.");
      return badResponse;
    }

    string instanceId = await client.ScheduleNewOrchestrationInstanceAsync("OrderProcessingOrchestrator", cartRequestDTO.CartId);

    var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
    await response.WriteStringAsync($"Orchestration started with ID = '{instanceId}'.");

    return response;
  }
}