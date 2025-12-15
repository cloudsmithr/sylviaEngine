using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SylviaEngine.Components;
using SylviaEngine.Enums;

namespace SylviaEngine.Tilesets;

public class TileMapRenderer : RenderComponent
{
    private readonly TileMap[] _tileMap;
    
    public TileMapRenderer(TileMap[] tileMap, int zIndex = 0) 
        : base(RenderLayer.Background, zIndex)
    {
        _tileMap = tileMap;
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        // Iterate through tile map data and draw
        for (int i = 0; i < _tileMap.Length - 1; i++)
        {
            for (int y = 0; y < _tileMap[i].Height; y++)
            {
                for (int x = 0; x < _tileMap[i].Width; x++)
                {
                    int tileId = (_tileMap[i].Data[x + (y * _tileMap[i].Width)]);
                    if (tileId == 0) continue; // empty tile
                    spriteBatch.Draw(
                        _tileMap[i].TileSet.Texture,
                        new Vector2(x * _tileMap[i].TileSet.TileWidth, y * _tileMap[i].TileSet.TileHeight),
                        // Zero tildId is considered no tile, so the array starts at 1. For calculation the position in the spritesheet however, it starts at 0, so we want to decrease this by 1 here
                        _tileMap[i].TileSet.GetSpriteRectByIndex(tileId - 1), 
                        Color.White);
                }
            }
            
        }
    }
}