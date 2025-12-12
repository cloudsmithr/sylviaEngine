using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SylviaEngine.Enums;

namespace SylviaEngine.Components;

public class TextRenderer : RenderComponent
{
    public SpriteFont Font;
    public string Text { get; set; }
    
    public TextRenderer(RenderLayer layer, SpriteFont font, string text = "", float zIndex = 0f) 
        : base(layer, zIndex)
    {
        Text = text;
        Font = font;
    }
    
    public override void Render(SpriteBatch spriteBatch)
    {
        if (string.IsNullOrEmpty(Text) || Owner == null) return;

        //Pivot = Font.MeasureString(Text) / 2f;

        spriteBatch.DrawString(
            Font, 
            Text,
            Owner.Transform.Position,
            Color,
            Owner.Transform.Rotation,
            Pivot,
            Owner.Transform.Scale,
            SpriteEffects,
            0f);
    }
    
    public void SetPivotToCenter()
    {
        if (Font != null && !string.IsNullOrEmpty(Text))
        {
            Vector2 textSize = Font.MeasureString(Text);
            Pivot = textSize / 2f;
        }
    }
}