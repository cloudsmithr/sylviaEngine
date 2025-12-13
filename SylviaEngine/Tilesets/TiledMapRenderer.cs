using Microsoft.Xna.Framework.Graphics;
using SylviaEngine.Components;
using SylviaEngine.Enums;
using SylviaEngine.Tilesets.Tiled;

namespace SylviaEngine.Tilesets;

public class TiledMapRenderer : RenderComponent
{
    private readonly TiledMap _tileMap;
    private readonly List<Texture2D> _tilesets;
    
    public TiledMapRenderer(TiledMap tileMap, List<Texture2D> tilesets, int zIndex = 0) 
        : base(RenderLayer.Background, zIndex)
    {
        _tileMap = tileMap;
        _tilesets = tilesets;
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        // Iterate through tile map data and draw using the textures
        foreach (var layer in _tileMap.Layers)
        {
            for (int y = 0; y < _tileMap.Height; y++)
            {
                for (int x = 0; x < _tileMap.Width; x++)
                {
                    int tileId = layer.Data[x + y * _tileMap.Width];
                    if (tileId == 0) continue; // empty tile
                    
                    DrawTile(spriteBatch, tileId, x, y);
                }
            }
        }
    }
    
    private void DrawTile(SpriteBatch sb, int tileId, int x, int y)
    {
        // Figure out which tileset and source rectangle
        // Draw the tile
    }
    
}