using KhumaloCraft.Business.Interfaces;
using KhumaloCraft.Data.Entities;
using KhumaloCraft.Data.Repositories.Interfaces;
using KhumaloCraft.Shared.DTOs;
using Microsoft.Extensions.DependencyInjection;
using Lib.Net.Http.WebPush;
using Lib.Net.Http.WebPush.Authentication;
using Microsoft.Extensions.Configuration;

namespace KhumaloCraft.Business.Services;

public class NotificationsService : INotificationsService
{
  private readonly INotificationsRepository _notificationsRepo;
  private readonly IUserRepository _userRepository;
  private readonly IServiceScopeFactory _scopeFactory;
  private readonly ISubscriptionService _subscriptionService;
  private readonly PushServiceClient _pushClient;

  public NotificationsService(INotificationsRepository notificationsRepo, IUserRepository userRepo, IServiceScopeFactory scopeFactory, ISubscriptionService subscriptionService, IConfiguration configuration)
  {
    _notificationsRepo = notificationsRepo;
    _userRepository = userRepo;
    _scopeFactory = scopeFactory;
    _subscriptionService = subscriptionService;
    _pushClient = new PushServiceClient
    {
      DefaultAuthentication = new VapidAuthentication(
            configuration["VapidKeys:PublicKey"],
            configuration["VapidKeys:PrivateKey"])
    };
  }

  public async Task AddNotificationAsync(string userId, string message)
  {
    var notification = new Notification
    {
      UserId = userId,
      Message = message,
      CreatedAt = DateTime.UtcNow,
      IsRead = false
    };

    await _notificationsRepo.AddNotificationAsync(notification);
  }

  public async Task AddNotificationToAllUsersAsync(string message)
  {
    List<string> allUserIds = await _userRepository.GetAllUsersIdsAsync();

    var tasks = allUserIds.Select(userId =>
        Task.Run(async () =>
        {
          using (var scope = _scopeFactory.CreateScope())
          {
            var scopedService = scope.ServiceProvider.GetRequiredService<INotificationsService>();
            await scopedService.AddNotificationAsync(userId, message);
          }
        })
    ).ToList();

    // Wait for all notification tasks to complete
    await Task.WhenAll(tasks);
  }

  public async Task<IEnumerable<NotificationDTO>> GetNotificationsForUserAsync(string userId)
  {
    var notifications = await _notificationsRepo.GetNotificationsForUserAsync(userId);
    return notifications.Select(n => new NotificationDTO
    {
      Id = n.Id,
      Message = n.Message,
      CreatedAt = n.CreatedAt,
      IsRead = n.IsRead
    }).ToList();
  }

  public async Task MarkNotificationAsReadAsync(int notificationId)
  {
    await _notificationsRepo.MarkAsReadAsync(notificationId);
  }

  public async Task<IEnumerable<NotificationDTO>> GetUnreadNotificationsForUserAsync(string userId)
  {
    var notifications = await _notificationsRepo.GetUnreadNotificationsForUserAsync(userId);
    return notifications.Select(n => new NotificationDTO
    {
      Id = n.Id,
      Message = n.Message,
      CreatedAt = n.CreatedAt,
      IsRead = n.IsRead
    }).ToList();
  }

  /*   public async Task SendPushNotificationAsync(string title, string body)
    {
      var subscriptions = await _subscriptionService.GetAllSubscriptionsAsync();

      foreach (var sub in subscriptions)
      {
        var pushSubscription = new PushSubscriptionDTO
        {
          Endpoint = sub.Endpoint,
          P256dh = sub.P256dh,
          Auth = sub.Auth
        };

        var notification = new PushMessage
        {
          Title = title,
          Body = body
        };

        await _pushClient.RequestPushMessageDeliveryAsync(pushSubscription, notification);
      }
    } */
}
