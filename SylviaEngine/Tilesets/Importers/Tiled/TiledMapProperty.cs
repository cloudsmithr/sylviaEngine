using System.Text.Json.Serialization;

namespace SylviaEngine.Tilesets.Tiled.Importer;

public class TiledMapProperty
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("type")]
    public string Type { get; set; }
    
    [JsonPropertyName("value")]
    public object Value { get; set; }
}