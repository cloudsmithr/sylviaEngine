using System.Xml.Serialization;

namespace SylviaEngine.Tilesets.Tiled;

[XmlRoot("tileset")]
public class TSXFile
{
    [XmlAttribute("version")]
    public string Version { get; set; }

    [XmlAttribute("tiledversion")]
    public string TiledVersion { get; set; }

    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlAttribute("tilewidth")]
    public int TileWidth { get; set; }

    [XmlAttribute("tileheight")]
    public int TileHeight { get; set; }

    [XmlAttribute("tilecount")]
    public int TileCount { get; set; }

    [XmlAttribute("columns")]
    public int Columns { get; set; }

    [XmlElement("image")]
    public TSXImage Image { get; set; }
}