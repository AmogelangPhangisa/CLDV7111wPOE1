using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;

namespace KhumaloCraft.BusinessFunctions;

public static class NotificationsOrchestrator
{
  [Function("NotificationsOrchestrator")]
  public static async Task Run([OrchestrationTrigger] TaskOrchestrationContext context)
  {
    await context.CallActivityAsync<string>("Notify", "Sending Notification");
  }
}