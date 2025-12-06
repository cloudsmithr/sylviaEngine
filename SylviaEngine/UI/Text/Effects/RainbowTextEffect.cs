using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SylviaEngine.Interfaces;
using SylviaEngine.Utilities;

namespace SylviaEngine.UI.Text.Effects;

public class RainbowTextEffect : ITextEffect
{

    public float ColorCycleSpeed { get; set; } = 1f;
    public float SaturationStrength { get; set; } = 1f;
    public float ValueStrength { get; set; } = 1f;
    
    public RainbowTextEffect() {}

    public RainbowTextEffect(float colorCycleSpeed, float saturationStrength, float valueStrength)
    {
        ColorCycleSpeed = colorCycleSpeed;
        SaturationStrength = saturationStrength;
        ValueStrength = valueStrength;
    }
    
    public void Update(GameTime gameTime) { }

    public void Apply(SpriteBatch spriteBatch, SpriteFont font, string text, 
        Vector2 position, Color baseColor, float time, Random random)
    {
        Vector2 currentPos = position;

        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];
            string character = c.ToString();

            // Calculate rainbow color
            float hue = (time * ColorCycleSpeed - i * 0.1f) % 1f;
            Color charColor = ColorUtils.HSVToRGB(hue, SaturationStrength, ValueStrength);

            spriteBatch.DrawString(font, character, currentPos, charColor);

            currentPos.X += font.MeasureString(character).X;
        }
    }
}