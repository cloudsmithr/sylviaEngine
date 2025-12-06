using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SylviaEngine.Interfaces;

namespace SylviaEngine.UI.Text.Effects;

public class ShakeTextEffect : ITextEffect
{
    public float ShakeStrength { get; set; } = 1f;
    private Random _random = new Random();
 
    public ShakeTextEffect(){}

    public ShakeTextEffect(float shakeStrength)
    {
        ShakeStrength = shakeStrength;
    }
    
    public void Update(GameTime gameTime) { }
    
    public void Apply(SpriteBatch spriteBatch, SpriteFont font, string text, 
        Vector2 position, Color baseColor, float time, Random random)
    {
        float shakeX = ((float)_random.NextDouble() - 0.5f) * 2f * ShakeStrength;
        float shakeY = ((float)_random.NextDouble() - 0.5f) * 2f * ShakeStrength;
        Vector2 shakenPos = position + new Vector2(shakeX, shakeY);

        spriteBatch.DrawString(font, text, shakenPos, baseColor);
    }
}