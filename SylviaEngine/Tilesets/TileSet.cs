using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SylviaEngine.Tilesets;

public class TileSet
{
    public Texture2D Texture { get; }
    public int TileWidth { get; }
    public int TileHeight { get; }
    public int Width { get; }
    public int Height { get; }

    public TileSet(Texture2D texture, int tileWidth, int tileHeight, int width, int height)
    {
        Texture = texture;
        TileWidth = tileWidth;
        TileHeight = tileHeight;
        Width = width;
        Height = height;
    }

    public Rectangle GetSpriteRectByIndex(int index)
    {
        int heightInTiles = Height / TileHeight;
        
        return new Rectangle(
            (index % heightInTiles) * TileWidth,
            (index / heightInTiles) * TileHeight,
            TileWidth,
            TileHeight);
    }
}