using System.Text.Json.Serialization;

namespace SylviaEngine.Tilesets.Importers.Tiled;

public class TiledMapProperty
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("type")]
    public string Type { get; set; }
    
    [JsonPropertyName("value")]
    public object Value { get; set; }
}