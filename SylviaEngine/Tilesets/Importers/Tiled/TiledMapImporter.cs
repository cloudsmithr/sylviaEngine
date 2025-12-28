using System.Text.Json;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SylviaEngine.Levels;

namespace SylviaEngine.Tilesets.Importers.Tiled;

public class TiledMapImporter
{
    public List<TileMap> ImportedMap = new List<TileMap>();
    public List<TiledMapObject> ImportedObjects = new List<TiledMapObject>();
    
    public void LoadMap(string mapPath, ContentManager content)
    {
        ImportedMap.Clear();
        ImportedObjects.Clear();
        
        string tiledMapJSON = File.ReadAllText("Content/" + mapPath);
        TiledMap tiledMap = JsonSerializer.Deserialize<TiledMap>(tiledMapJSON) ?? throw new FileLoadException("Could not load TiledMap");
        
        string tilesetXML = File.ReadAllText("Content/" + tiledMap.Tilesets[0].Source);
        
        
        var serializer = new XmlSerializer(typeof(TSXFile));
        using var stream = File.OpenRead("Content/" + tiledMap.Tilesets[0].Source);
        TSXFile tsx = (TSXFile)serializer.Deserialize(stream) ?? throw new  FileLoadException("Could not load TSX File");

        TileSet tileSet = new TileSet(
            content.Load<Texture2D>(tsx.Image.Source),
            tsx.TileWidth,
            tsx.TileHeight,
            tsx.Image.Width,
            tsx.Image.Height
        );
        
        foreach (TiledMapLayer layer in tiledMap.Layers)
        {
            if (string.Equals(layer.Type, "tilelayer", StringComparison.OrdinalIgnoreCase))
            {
                TileMap tileMap = new TileMap(layer.Width, layer.Height, layer.Data, tileSet);
                ImportedMap.Add(tileMap);
            }
            else if (string.Equals(layer.Type, "objectlayer", StringComparison.OrdinalIgnoreCase))
            {
                ImportedObjects.AddRange(layer.Objects);
            }
        }
    }
}