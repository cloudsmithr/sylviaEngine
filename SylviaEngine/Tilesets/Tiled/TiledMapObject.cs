using System.Text.Json.Serialization;

namespace SylviaEngine.Tilesets;

public class TiledMapObject
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("type")]
    public string Type { get; set; }
    
    [JsonPropertyName("x")]
    public float X { get; set; }
    
    [JsonPropertyName("y")]
    public float Y { get; set; }
    
    [JsonPropertyName("width")]
    public float Width { get; set; }
    
    [JsonPropertyName("height")]
    public float Height { get; set; }
    
    [JsonPropertyName("rotation")]
    public float Rotation { get; set; }
    
    [JsonPropertyName("visible")]
    public bool Visible { get; set; }
    
    [JsonPropertyName("properties")]
    public List<TiledMapProperty> Properties { get; set; }
}