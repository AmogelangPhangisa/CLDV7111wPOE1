using KhumaloCraft.Data.Entities;

namespace KhumaloCraft.Data.Repositories.Interfaces;

public interface ISubscriptionRepository
{
  Task<bool> AddSubscriptionAsync(Subscription subscriptionEntity);
  Task<bool> UpdateSubscriptionAsync(Subscription subscriptionEntity);
  Task<Subscription> GetSubscriptionByEndpointAsync(string endpoint);
  Task<List<Subscription>> GetAllSubscriptionsAsync();
}
