using System.Text.Json;
using Microsoft.Xna.Framework.Content;

namespace SylviaEngine.Levels;

public class LevelLoader
{
    private readonly ContentManager _content;
    
    public LevelLoader(ContentManager content)
    {
        _content = content;
    }
    
    public Level LoadLevel(string levelPath)
    {
        string fullPath = Path.Combine(_content.RootDirectory, levelPath);
        string json = File.ReadAllText(fullPath);
        return JsonSerializer.Deserialize<Level>(json) 
               ?? throw new FileLoadException($"Could not load level: {levelPath}");
    }
}