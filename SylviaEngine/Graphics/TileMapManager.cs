using SylviaEngine.Interfaces;
using System.Text.Json;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SylviaEngine.Graphics;

public class TileMapManager : ITileMapManager
{
    public ContentManager ContentManager { get; }
    public ITileMap TileMap { get; private set; }
    private List<Texture2D> _tileMapTexture = new List<Texture2D>();

    public TileMapManager(ContentManager contentManager)
    {
        ContentManager = contentManager;
    }
    
    public void LoadMap<T>(string path) where T : ITileMap, new()
    {
        string json = File.ReadAllText("Content/" + path);
        TileMap = JsonSerializer.Deserialize<T>(json) ?? new T();
    }
}