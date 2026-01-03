using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SylviaEngine.Engine;
using SylviaEngine.Enums;
using SylviaEngine.Graphics;
using SylviaEngine.Input;
using SylviaEngine.Interfaces;
using SylviaEngine.UI.Text.Effects;
using IUpdateable = SylviaEngine.Interfaces.IUpdateable;

namespace SylviaEngine.Components;

public class FormattedTextRenderer : AnimatedTextRenderer, IUpdateable
{
    public int Width { get; set; }
    private string Text { get; set; }
    private List<string> textLines { get; set; } = new List<string>();
    
    private float _time = 0f;
    private Random _random = new Random();
    private int _yPadding = 0;
    private int _startingLineIndex = 0;
    public int _maxLines = 0;

    public void Update(GameTime gameTime)
    {
        _time += (float)gameTime.ElapsedGameTime.TotalSeconds;
        bool affirmPressed = Core.Input.InputPressed(InputAction.Affirm);
        bool affirmReleased = Core.Input.InputReleased(InputAction.Affirm);
        
        foreach (ITextEffect effect in Effects)
        {
            if (effect is TypeWriterTextEffect typeWriterTextEffect)
            {
                if (affirmPressed)
                {
                    if (typeWriterTextEffect.IsDoneRendering(CalculateCurrentLineCharacters()))
                    {
                        _startingLineIndex += _maxLines;
                        typeWriterTextEffect.Reset();
                    }
                    else
                    {
                        typeWriterTextEffect.IgnoreSpeed = true;
                    }
                }
                else if (affirmReleased)
                {
                    typeWriterTextEffect.IgnoreSpeed = false;
                }
            }
            
            effect.Update(gameTime);
        }
    }
    
    public bool IsDoneRendering() => _startingLineIndex > _maxLines;
    
    public FormattedTextRenderer(RenderLayer layer, SpriteFont font, string text, Color color = default, bool enabled = true, float zIndex = 0f, int width = 0, int yPadding = 0, int maxLines = 0)
        : base(layer, font, text, color, enabled, zIndex)
    {
        if (width > 0)
            Width = width;
        else
            Width = WindowManager.Instance.RenderWidth;
        
        _yPadding = yPadding;
        _maxLines = maxLines;
        SetText(text);
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        if (Font == null || Owner == null || string.IsNullOrEmpty(Text)) return;
        
        Vector2 position = Owner.Transform.Position - Pivot;

        if (Effects.Count <= 0)
        {
            foreach (string textLine in textLines)
            {
                spriteBatch.DrawString(Font, textLine, position, Color);
                position.Y += Font.MeasureString(textLine).Y;
            }
        }
        else if (Effects.Count == 1 && Effects[0] is not TypeWriterTextEffect typeWriterTextEffect) // we can't use the typewriter effect here or it will render both lines at the same time
        {
            foreach (string textLine in textLines)
            {
                Effects[0].ApplyToString(spriteBatch, Font, textLine, position, Color, _time, _random);
                position.Y += Font.MeasureString(textLine).Y;
            }
        }
        else
        {
            int jCounter = 0;
            float originalXPos = position.X;
            for (int k = _startingLineIndex; k < _startingLineIndex + _maxLines; k++)
            {
                if (k > textLines.Count - 1)
                    break;

                string textLine = textLines[k];
                for (int i = 0; i < textLine.Length; i++)
                {
                    Vector2 charPos = position;
                    Color charColor = Color;
                    string c = textLine[i].ToString();
                
                    foreach (ITextEffect effect in Effects)
                    {
                        (charPos, charColor) = effect.ApplyToCharacter(charPos, jCounter, charColor, _time, _random);
                    }
                    if (charColor.A > 0)
                        spriteBatch.DrawString(Font, c, charPos, charColor);
                
                    position.X += Font.MeasureString(c).X;
                    jCounter++;
                }

                position.X = originalXPos;
                position.Y += Font.MeasureString(textLine).Y + _yPadding;
            }
        }
    }
    
    private void SetText(string text)
    {
        Text = text;
        if (Font.MeasureString(text).X <= Width)
        {
            textLines = new List<string>() { text };
        }
        else
        {
            textLines = CalculateLines(text);
        }
    }
    
    private List<string> CalculateLines(string text)
    {
        var result = new List<string>();
        int startIndex = 0;
        int lastBreakPoint = 0;
    
        for (int i = 0; i < text.Length; i++)
        {
            // Track potential break points
            if (text[i] == ' ' || text[i] == '-' || text[i] == '\n')
            {
                lastBreakPoint = i;
            }
        
            // Check if we've exceeded width
            string currentSubstring = text.Substring(startIndex, i - startIndex + 1);
            if (Font.MeasureString(currentSubstring).X > Width || text[i] == '\n')
            {
                // Break at last valid point (or force break if none exists)
                int breakAt = lastBreakPoint > startIndex ? lastBreakPoint : i - 1;
            
                result.Add(text.Substring(startIndex, breakAt - startIndex));
                startIndex = breakAt + 1;
                lastBreakPoint = startIndex; // Reset break point
            }
        }
    
        // Add remaining text
        if (startIndex < text.Length)
        {
            result.Add(text.Substring(startIndex));
        }
    
        return result;
    }

    private int CalculateCurrentLineCharacters()
    {
        int count = 0;
        for (int i = _startingLineIndex; i < _startingLineIndex + _maxLines; i++)
        {
            if (i > textLines.Count - 1)
                break;
            count += textLines[i].Length;
        }

        return count;
    }
}