using System.Text.Json.Serialization;

namespace SylviaEngine.Tilesets.Importers.Tiled;

public class TiledMapLayer
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("type")]
    public string Type { get; set; }
    
    [JsonPropertyName("visible")]
    public bool Visible { get; set; }
    
    [JsonPropertyName("opacity")]
    public float Opacity { get; set; }
    
    [JsonPropertyName("x")]
    public int X { get; set; }
    
    [JsonPropertyName("y")]
    public int Y { get; set; }
    
    // For tile layers
    [JsonPropertyName("data")]
    public List<int> Data { get; set; }
    
    [JsonPropertyName("height")]
    public int Height { get; set; }
    
    [JsonPropertyName("width")]
    public int Width { get; set; }
    
    // For object layers
    [JsonPropertyName("draworder")]
    public string DrawOrder { get; set; }
    
    [JsonPropertyName("objects")]
    public List<TiledMapObject> Objects { get; set; }
}