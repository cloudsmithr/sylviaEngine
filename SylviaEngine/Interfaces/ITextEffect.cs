using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SylviaEngine.Interfaces;

public interface ITextEffect
{
    void ApplyToString(SpriteBatch spriteBatch, SpriteFont font, string text, 
        Vector2 position, Color baseColor, float time, Random random);

    public (Vector2 newPosition, Color newColor) ApplyToCharacter(Vector2 position, int characterIndex, Color baseColor,
        float time, Random random);
    
    void Update(GameTime gameTime);
}