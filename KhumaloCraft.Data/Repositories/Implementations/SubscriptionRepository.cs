using KhumaloCraft.Data.Data;
using KhumaloCraft.Data.Entities;
using KhumaloCraft.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KhumaloCraft.Data.Repositories.Implementations;

public class SubscriptionRepository : ISubscriptionRepository
{
  private readonly ApplicationDbContext _dbContext;

  public SubscriptionRepository(ApplicationDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<bool> AddSubscriptionAsync(Subscription subscriptionEntity)
  {
    _dbContext.Subscriptions.Add(subscriptionEntity);
    return await SaveChangesAsync();
  }

  public async Task<bool> UpdateSubscriptionAsync(Subscription subscriptionEntity)
  {
    _dbContext.Subscriptions.Update(subscriptionEntity);
    return await SaveChangesAsync();
  }

  public async Task<Subscription> GetSubscriptionByEndpointAsync(string endpoint)
  {
    return await _dbContext.Subscriptions
        .FirstOrDefaultAsync(s => s.Endpoint == endpoint);
  }

  public async Task<List<Subscription>> GetAllSubscriptionsAsync()
  {
    return await _dbContext.Subscriptions.ToListAsync();
  }

  private async Task<bool> SaveChangesAsync()
  {
    try
    {
      await _dbContext.SaveChangesAsync();
      return true;
    }
    catch
    {
      return false;
    }
  }
}

