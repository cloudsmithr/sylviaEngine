using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SylviaEngine.Interfaces;
using SylviaEngine.Utilities;

namespace SylviaEngine.UI.Text.Effects;

public class RainbowWaveTextEffect : ITextEffect
{
    public float Amplitude { get; set; } = 5f;
    public float Frequency { get; set; } = 1f;
    public float Speed { get; set; } = 5f;
    public float ColorCycleSpeed { get; set; } = 1f;
    public float SaturationStrength { get; set; } = 1f;
    public float ValueStrength { get; set; } = 1f;
    
    public RainbowWaveTextEffect(){}

    public RainbowWaveTextEffect(
        float amplitude,
        float frequency,
        float speed,
        float colorCycleSpeed,
        float saturationStrength,
        float valueStrength)
    {
        Amplitude = amplitude;
        Frequency = frequency;
        Speed = speed;
        ColorCycleSpeed = colorCycleSpeed;
        SaturationStrength = saturationStrength;
        ValueStrength = valueStrength;
    }

    public void Update(GameTime gameTime)
    {
    }

    public void Apply(SpriteBatch spriteBatch, SpriteFont font, string text, 
        Vector2 position, Color baseColor, float time, Random random)
    {
        Vector2 currentPos = position;

        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];
            string character = c.ToString();

            // Wave offset
            float offset = (float)Math.Sin(time * Speed - i * Frequency) * Amplitude;
        
            // Rainbow color
            float hue = (time * ColorCycleSpeed - i * 0.1f) % 1f;
            Color charColor = ColorUtils.HSVToRGB(hue, SaturationStrength, ValueStrength);
        
            Vector2 charPos = currentPos + new Vector2(0, offset);
            spriteBatch.DrawString(font, character, charPos, charColor);

            currentPos.X += font.MeasureString(character).X;
        }        
    }
}