using System.Xml.Serialization;

namespace SylviaEngine.Tilesets.Importers.Tiled;

public class TSXImage
{
    [XmlAttribute("source")]
    public string Source { get; set; }

    [XmlAttribute("width")]
    public int Width { get; set; }

    [XmlAttribute("height")]
    public int Height { get; set; }
}