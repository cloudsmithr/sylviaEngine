using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SylviaEngine.Interfaces;

namespace SylviaEngine.UI.Text.Effects;

public class JitterTextEffect : ITextEffect
{
    public float JitterStrength { get; set; } = 1;
    private Random _random = new Random();
 
    public JitterTextEffect(){}

    public void Update(GameTime gameTime) { }
    
    public JitterTextEffect(float jitterStrength)
    {
        JitterStrength = jitterStrength;
    }

    public void Apply(SpriteBatch spriteBatch, SpriteFont font, string text, 
        Vector2 position, Color baseColor, float time, Random random)
    {
        Vector2 currentPos = position;

        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];
            string character = c.ToString();

            // Random jitter for each character
            float jitterX = ((float)_random.NextDouble() - 0.5f) * 2f * JitterStrength;
            float jitterY = ((float)_random.NextDouble() - 0.5f) * 2f * JitterStrength;
            Vector2 charPos = currentPos + new Vector2(jitterX, jitterY);

            spriteBatch.DrawString(font, character, charPos, baseColor);

            // Move to next character position (use base position for consistency)
            currentPos.X += font.MeasureString(character).X;
        }
    }
    
}