using KhumaloCraft.Data.Entities;
using KhumaloCraft.Shared.DTOs;

namespace KhumaloCraft.Business.Interfaces;

public interface ISubscriptionService
{
  Task<bool> SaveSubscriptionAsync(PushSubscriptionDTO subscription);
  Task<List<Subscription>> GetAllSubscriptionsAsync();
}
