using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SylviaEngine.Interfaces;

namespace SylviaEngine.UI.Text.Effects;

public class WaveTextEffect : ITextEffect
{
    public float Amplitude { get; set; } = 5f;
    public float Frequency { get; set; } = 1f;
    public float Speed { get; set; } = 5f;
    
    public WaveTextEffect()
    {
    }

    public WaveTextEffect(float amplitude, float frequency, float speed)
    {
        Amplitude = amplitude;
        Frequency = frequency;
        Speed = speed;
    }
    
    public void Update(GameTime gameTime) { }

    public void Apply(SpriteBatch spriteBatch, SpriteFont font, string text, 
        Vector2 position, Color baseColor, float time, Random random)
    {
        Vector2 currentPos = position;
        for (int i = 0; i < text.Length; i++)
        {
            string character = text[i].ToString();
            
            // We subtract i*Frequency to make the wave move from left to right. Set Speed to a negative number to make it move from right to left.
            float offset = (float)Math.Sin(time * Speed - i * Frequency) * Amplitude;
            
            Vector2 charPos = currentPos + new Vector2(0, offset);
            spriteBatch.DrawString(font, character, charPos, baseColor);
            currentPos.X += font.MeasureString(character).X;
        }
    }
}