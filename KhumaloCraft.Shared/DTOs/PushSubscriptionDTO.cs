namespace KhumaloCraft.Shared.DTOs;

public class PushSubscriptionDTO
{
  public string? UserId { get; set; }
  public string Endpoint { get; set; }
  public PushSubscriptionKeys Keys { get; set; }
}

public class PushSubscriptionKeys
{
  public string P256dh { get; set; }
  public string Auth { get; set; }
}