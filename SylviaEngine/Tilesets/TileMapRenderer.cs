using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SylviaEngine.Components;
using SylviaEngine.Enums;

namespace SylviaEngine.Tilesets;

public class TileSetRenderer : RenderComponent
{
    private readonly TileMap[] _tileMap;
    private readonly TileSet[] _tileSet;
    
    public TileSetRenderer(TileMap[] tileMap, TileSet[] tileSet, int zIndex = 0) 
        : base(RenderLayer.Background, zIndex)
    {
        _tileMap = tileMap;
        _tileSet = tileSet;
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        // Iterate through tile map data and draw using the textures
        for ()
        for (int y = 0; y < _tileMap.Height; y++)
        {
            for (int x = 0; x < _tileMap.Width; x++)
            {
                int tileId = _tileMap.Data[x + y * _tileMap.Width];
                if (tileId == 0) continue; // empty tile
                spriteBatch.Draw(
                    _tileSet.Texture,
                    _tileSet.GetSpriteRectByIndex(tileId),
                    Color.White);
            }
        }
    }
}