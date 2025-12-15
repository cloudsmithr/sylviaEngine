using System.Text.Json.Serialization;

namespace SylviaEngine.Tilesets.Importers.Tiled;

public class TiledMapTileSet
{
    [JsonPropertyName("firstgid")]
    public int FirstGid { get; set; }
    
    [JsonPropertyName("source")]
    public string Source { get; set; }
}