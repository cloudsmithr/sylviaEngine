using Microsoft.Xna.Framework.Content;
using SylviaEngine.Interfaces;

namespace SylviaEngine.Graphics;

public interface ITileMapManager
{
    ContentManager ContentManager { get; }
    public void LoadMap<T>(string path) where T : ITileMap, new();
}