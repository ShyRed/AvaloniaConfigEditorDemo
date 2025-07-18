using System.Text.Json.Serialization;

namespace AvaloniaConfigEditorDemo.Models;

public class ServerConfig
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("language")]
    public string Language { get; set; } = string.Empty;

    [JsonPropertyName("httpPort")]
    public int HttpPort { get; set; }

    [JsonPropertyName("httpsPort")]
    public int HttpsPort { get; set; }

    [JsonPropertyName("forceSsl")]
    public bool ForceSsl { get; set; }
}