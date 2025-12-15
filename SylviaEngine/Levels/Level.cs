using System.Text.Json.Serialization;

namespace SylviaEngine.Levels;

public class Level
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("tileMapPath")]
    public string TileMapPath { get; set; }
    [JsonPropertyName("backgroundMusic")]
    public string BackgroundMusic { get; set; }
    [JsonPropertyName("transitionEffect")]
    public LevelTransitionEffect TransitionEffect { get; set; } = LevelTransitionEffect.None;
}