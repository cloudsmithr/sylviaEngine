using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SylviaEngine.Interfaces;

namespace SylviaEngine.UI.Text.Effects;

public class ShakeTextEffect : ITextEffect
{
    public float ShakeStrength { get; set; } = 1f;

    private float _shakeX = 0;
    private float _shakeY = 0;
    
    public ShakeTextEffect(){}

    public ShakeTextEffect(float shakeStrength)
    {
        ShakeStrength = shakeStrength;
    }
    
    public void Update(GameTime gameTime) { }
    
    public void ApplyToString(SpriteBatch spriteBatch, SpriteFont font, string text, 
        Vector2 position, Color baseColor, float time, Random random)
    {
        _shakeX = ((float)random.NextDouble() - 0.5f) * 2f * ShakeStrength;
        _shakeY = ((float)random.NextDouble() - 0.5f) * 2f * ShakeStrength;
        Vector2 shakenPos = position + new Vector2(_shakeX, _shakeY);

        spriteBatch.DrawString(font, text, shakenPos, baseColor);
    }
    
    public (Vector2 newPosition, Color newColor) ApplyToCharacter(Vector2 position, int characterIndex, Color baseColor, float time, Random random)
    {
        if (characterIndex == 0)
        {
            _shakeX = ((float)random.NextDouble() - 0.5f) * 2f * ShakeStrength;
            _shakeY = ((float)random.NextDouble() - 0.5f) * 2f * ShakeStrength;
        }
        return (position + new Vector2(_shakeX, _shakeY), baseColor);
    }
}