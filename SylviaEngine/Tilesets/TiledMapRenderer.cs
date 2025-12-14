using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SylviaEngine.Components;
using SylviaEngine.Enums;
using SylviaEngine.Tilesets.Tiled;

namespace SylviaEngine.Tilesets;

public class TiledMapRenderer : RenderComponent
{
    private readonly TiledMap _tileMap;
    private readonly List<TileSet> _tilesets;
    
    public TiledMapRenderer(TiledMap tileMap, int zIndex = 0) 
        : base(RenderLayer.Background, zIndex)
    {
        _tileMap = tileMap;
        // populate tilesets from tilemap here
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        // Iterate through tile map data and draw using the textures
        foreach (TiledMapLayer layer in _tileMap.Layers)
        {
            for (int y = 0; y < _tileMap.Height; y++)
            {
                for (int x = 0; x < _tileMap.Width; x++)
                {
                    int tileId = layer.Data[x + y * _tileMap.Width];
                    if (tileId == 0) continue; // empty tile
                    spriteBatch.Draw(
                        _tilesets[0].Texture,
                        _tilesets[0].GetSpriteRectByIndex(tileId),
                        Color.White);
                }
            }
        }
    }
}