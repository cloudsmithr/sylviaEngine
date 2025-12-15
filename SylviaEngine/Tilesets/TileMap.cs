namespace SylviaEngine.Tilesets;

public class TileMap
{
    public int Width { get; set; }
    public int Height { get; set; }
    public List<int> Data { get; set; }
    
    public TileSet TileSet { get; set; }

    public TileMap(int width, int height, List<int> data, TileSet tileSet)
    {
        Width = width;
        Height = height;
        Data = data;
        TileSet = tileSet;
    }
}