using Microsoft.Xna.Framework.Graphics;

namespace SylviaEngine.UI.Text;

public class FontManager
{
    private readonly SpriteFont[] _fonts;
    public SpriteFont this[FontStyle style] => _fonts[(int)style];
    
    public FontManager()
    {
        _fonts = new SpriteFont[Enum.GetValues<FontStyle>().Length];
    }
    
    public void AddFont(SpriteFont font, FontStyle fontStyle)
    {
        _fonts[(int)fontStyle] = font;
    }
}