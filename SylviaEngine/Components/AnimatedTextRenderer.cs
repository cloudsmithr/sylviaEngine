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
    public Color Color { get; set; } = Color.White;
    public Vector2 Origin { get; set; }
    public ITextEffect? Effect { get; set; }
    public int Width { get; set; }
    public int MaxLines { get; set; }
    
    private float _time = 0f;
    private Random _random = new Random();

    public void Update(GameTime gameTime)
    {
        _time += (float)gameTime.ElapsedGameTime.TotalSeconds;
        Effect?.Update(gameTime);
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
        
        Vector2 position = Owner.Transform.Position - Origin;
        
        if (Effect != null)
            Effect.Apply(spriteBatch, Font, Text, position, Color, _time, _random);
        else
            spriteBatch.DrawString(Font, Text, position, Color);
    }
}