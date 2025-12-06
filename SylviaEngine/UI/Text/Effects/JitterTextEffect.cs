using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SylviaEngine.Interfaces;

namespace SylviaEngine.UI.Text.Effects;

public class JitterTextEffect : ITextEffect
{
    public float JitterStrength { get; set; } = 1;
 
    public JitterTextEffect(){}

    public void Update(GameTime gameTime) { }
    
    public JitterTextEffect(float jitterStrength)
    {
        JitterStrength = jitterStrength;
    }

    public void ApplyToString(SpriteBatch spriteBatch, SpriteFont font, string text, 
        Vector2 position, Color baseColor, float time, Random random)
    {
        Vector2 currentPos = position;

        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];
            string character = c.ToString();

            // Random jitter for each character
            (Vector2 charPos, Color charColor) = ApplyToCharacter(currentPos, i, baseColor, time, random);

            spriteBatch.DrawString(font, character, charPos, baseColor);

            // Move to next character position (use base position for consistency)
            currentPos.X += font.MeasureString(character).X;
        }
    }

    public (Vector2 newPosition, Color newColor) ApplyToCharacter(Vector2 position, int characterIndex, Color baseColor, float time, Random random)
    {
        float jitterX = ((float)random.NextDouble() - 0.5f) * 2f * JitterStrength;
        float jitterY = ((float)random.NextDouble() - 0.5f) * 2f * JitterStrength;

        return (position + new Vector2(jitterX, jitterY), baseColor);
    }
}