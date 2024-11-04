using KhumaloCraft.Business.Interfaces;
using KhumaloCraft.Data.Entities;
using KhumaloCraft.Data.Repositories.Interfaces;
using KhumaloCraft.Shared.DTOs;

namespace KhumaloCraft.Business.Services;

public class SubscriptionService : ISubscriptionService
{
  private readonly ISubscriptionRepository _subscriptionRepository;

  public SubscriptionService(ISubscriptionRepository subscriptionRepository)
  {
    _subscriptionRepository = subscriptionRepository;
  }

  public async Task<bool> SaveSubscriptionAsync(PushSubscriptionDTO subscription)
  {
    // Check if the subscription already exists by endpoint
    var existingSubscription = await _subscriptionRepository.GetSubscriptionByEndpointAsync(subscription.Endpoint);

    // Create a new SubscriptionEntity to represent the subscription
    var subscriptionEntity = new Subscription
    {
      UserId = subscription.UserId,
      Endpoint = subscription.Endpoint,
      P256dh = subscription.Keys.P256dh,
      Auth = subscription.Keys.Auth
    };

    // Add or update the subscription in the database
    if (existingSubscription == null)
    {
      return await _subscriptionRepository.AddSubscriptionAsync(subscriptionEntity);
    }
    else
    {
      // Update existing subscription keys in case they changed
      existingSubscription.P256dh = subscription.Keys.P256dh;
      existingSubscription.Auth = subscription.Keys.Auth;
      return await _subscriptionRepository.UpdateSubscriptionAsync(existingSubscription);
    }
  }

  public async Task<List<Subscription>> GetAllSubscriptionsAsync()
  {
    return await _subscriptionRepository.GetAllSubscriptionsAsync();
  }
}

