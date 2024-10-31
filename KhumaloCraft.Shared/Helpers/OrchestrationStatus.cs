using System.Text.Json.Serialization;

public class OrchestrationStatus
{
  [JsonPropertyName("name")]
  public string Name { get; set; }

  [JsonPropertyName("instanceId")]
  public string InstanceId { get; set; }

  [JsonPropertyName("runtimeStatus")]
  public string RuntimeStatus { get; set; }

  [JsonPropertyName("input")]
  public string Input { get; set; }

  [JsonPropertyName("customStatus")]
  public object CustomStatus { get; set; }

  [JsonPropertyName("output")]
  public Response<string> Output { get; set; }

  [JsonPropertyName("createdTime")]
  public DateTime CreatedTime { get; set; }

  [JsonPropertyName("lastUpdatedTime")]
  public DateTime LastUpdatedTime { get; set; }
}