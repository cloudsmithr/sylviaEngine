using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SylviaEngine.Enums;
using SylviaEngine.Interfaces;
using IUpdateable = SylviaEngine.Interfaces.IUpdateable;

namespace SylviaEngine.Components;

public class AnimatedTextRenderer : RenderComponent, IUpdateable
{
    public SpriteFont Font { get; set; }
    private string Text { get; set; } = string.Empty;
    public List<ITextEffect> Effects { get; set; } = new List<ITextEffect>();

    private float _time = 0f;
    private Random _random = new Random();

    public void Update(GameTime gameTime)
    {
        _time += (float)gameTime.ElapsedGameTime.TotalSeconds;
        foreach (ITextEffect effect in Effects)
            effect.Update(gameTime);
    }

    public AnimatedTextRenderer(RenderLayer layer, SpriteFont font, string text, Color color = default, bool enabled = true, float zIndex = 0f) 
        : base(layer, zIndex)
    {
        Enabled = enabled;
        Color = color;
        Font = font;
        Text = text;
    }
    
    public override void Render(SpriteBatch spriteBatch)
    {
        if (Font == null || Owner == null || string.IsNullOrEmpty(Text)) return;
        
        Vector2 position = Owner.Transform.Position - Pivot;

        if (Effects.Count <= 0)
        {
            spriteBatch.DrawString(Font, Text, position, Color);
        }
        else if (Effects.Count == 1)
        {
            Effects[0].ApplyToString(spriteBatch, Font, Text, position, Color, _time, _random);
        }
        else
        {
            for (int i = 0; i < Text.Length; i++)
            {
                Vector2 charPos = position;
                Color charColor = Color;
                string c = Text[i].ToString();
                
                foreach (ITextEffect effect in Effects)
                {
                    (charPos, charColor) = effect.ApplyToCharacter(charPos, i, charColor, _time, _random);
                }
                if (charColor.A > 0)
                    spriteBatch.DrawString(Font, c, charPos, charColor);
                
                position.X += Font.MeasureString(c).X;
            }
        }
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