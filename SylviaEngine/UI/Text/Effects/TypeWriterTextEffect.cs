using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SylviaEngine.Interfaces;

namespace SylviaEngine.UI.Text.Effects;

public class TypeWriterTextEffect : ITextEffect
{
    public float TypewriterSpeed { get; set; } = 0.2f;  // Seconds per character

    private int _visibleCharacters = 0;
    private float _typewriterTimer = 0f;
    private string _text = "";

    public TypeWriterTextEffect() {}

    public TypeWriterTextEffect(float typewriterSpeed)
    {
        TypewriterSpeed = typewriterSpeed;
    }
    
    // Check if typewriter is done
    public bool IsTypewriterComplete => _visibleCharacters >= _text.Length;
    
    public void Update(GameTime gameTime)
    {
        // Update typewriter effect
        if (_visibleCharacters < _text.Length || _text.Length == 0)
        {
            _typewriterTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_typewriterTimer >= TypewriterSpeed)
            {
                _typewriterTimer = 0f;
                _visibleCharacters++;
            }
        }
    }
    
    public void ApplyToString(SpriteBatch spriteBatch, SpriteFont font, string text, 
        Vector2 position, Color baseColor, float time, Random random)
    {
        _text = text;
        Vector2 currentPos = position;
        
        for (int i = 0; i < Math.Min(_visibleCharacters, text.Length); i++)
        {
            string character = text[i].ToString();
            spriteBatch.DrawString(font, character, currentPos, baseColor);
            currentPos.X += font.MeasureString(character).X;
        }
    }
    
    public (Vector2 newPosition, Color newColor) ApplyToCharacter(Vector2 position, int characterIndex, Color baseColor, float time, Random random)
    {
        Color newColor = baseColor;
        if (characterIndex > _visibleCharacters)
            newColor.A = 0;
        
        return (position, newColor);
    }
}