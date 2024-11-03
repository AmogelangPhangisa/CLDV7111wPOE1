using System;

namespace KhumaloCraft.Data.Entities;

public class Subscription
{
  public int Id { get; set; }
  public string UserId { get; set; }
  public string Endpoint { get; set; }
  public string P256dh { get; set; }
  public string Auth { get; set; }
}
