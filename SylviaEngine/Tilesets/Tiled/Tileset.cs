using System.Text.Json.Serialization;

namespace SylviaEngine.Tilesets.Tiled;

public class Tileset
{
    [JsonPropertyName("firstgid")]
    public int FirstGid { get; set; }
    
    [JsonPropertyName("source")]
    public string Source { get; set; }
}