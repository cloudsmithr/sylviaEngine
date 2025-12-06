namespace SylviaEngine.Components;

public class deleteme
{
 /*       // AnimatedTextRenderer parameters
    public SpriteFont Font { get; set; }
    private string Text { get; set; } = string.Empty;
    public Color Color { get; set; } = Color.White;
    public Vector2 Origin { get; set; }
    public TextEffect Effect { get; set; } = TextEffect.None;
    public int Width { get; set; }
    public int MaxLines { get; set; }

    // Effect parameters
    public float WaveAmplitude { get; set; } = 5f;      // How high the wave
    public float WaveFrequency { get; set; } = 1f;      // How tight the wave
    public float WaveSpeed { get; set; } = 10f;          // How fast it moves
    public float JitterAmount { get; set; } = 2f;       // Max jitter distance
    public float TypewriterSpeed { get; set; } = 0.05f;  // Seconds per character
    
    public IInputManager? InputManager { get; set; }
    
    private float _time = 0f;
    private Random _random = new Random();

    // Typewriter state
    private int _visibleCharacters = 0;
    private float _typewriterTimer = 0f;
    private List<string> textLines = new List<string>();
    private int currentLine = 0;
    
    public AnimatedTextRenderer(RenderLayer layer, SpriteFont font, int width = 0, bool enabled = true, float zIndex = 0f) 
        : base(layer, zIndex)
    {
        Enabled = enabled;
        Font = font;
        if (width == 0)
            Width = WindowManager.Instance.RenderWidth;
    }

    public void Update(GameTime gameTime)
    {
        _time += (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        // Update typewriter effect
        if (Effect == TextEffect.TypeWriter && _visibleCharacters < Text.Length)
        {
            _typewriterTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_typewriterTimer >= TypewriterSpeed || (InputManager is not null && InputManager.InputDown(InputAction.Action)))
            {
                _typewriterTimer = 0f;
                _visibleCharacters++;
            }
        }
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        if (Font == null || Owner == null || string.IsNullOrEmpty(Text)) return;

        Vector2 position = Owner.Transform.Position - Origin;

        switch (Effect)
        {
            case TextEffect.Wave:
                DrawWaveText(spriteBatch, position);
                break;
            case TextEffect.Jitter:
                DrawJitterText(spriteBatch, position);
                break;
            case TextEffect.Shake:
                DrawShakeText(spriteBatch, position);
                break;
            case TextEffect.RainbowText:
                DrawRainbowText(spriteBatch, position);
                break;
            case TextEffect.RainbowTextWave:
                DrawWaveRainbowText(spriteBatch, position);
                break;
            case TextEffect.TypeWriter:
                DrawTypewriter(spriteBatch, position);
                break;
            default:
                // Normal text
                spriteBatch.DrawString(Font, Text, position, Color);
                break;
        }
    }
    // Helper to reset typewriter effect
    public void ResetTypewriter()
    {
        _visibleCharacters = 0;
        _typewriterTimer = 0f;
    }
    
    // Helper to instantly show all text
    public void CompleteTypewriter()
    {
        _visibleCharacters = Text.Length;
    }
    
    // Check if typewriter is done
    public bool IsTypewriterComplete => _visibleCharacters >= Text.Length;
    
    private void DrawTypewriter(SpriteBatch spriteBatch, Vector2 basePosition)
    {
        Vector2 currentPos = basePosition;
        
        for (int i = 0; i < Math.Min(_visibleCharacters, Text.Length); i++)
        {
            string character = Text[i].ToString();
            spriteBatch.DrawString(Font, character, currentPos, Color);
            currentPos.X += Font.MeasureString(character).X;
        }
    }
    
    private void DrawWaveText(SpriteBatch spriteBatch, Vector2 basePosition)
    {
        Vector2 currentPos = basePosition;

        for (int i = 0; i < Text.Length; i++)
        {
            char c = Text[i];
            string character = c.ToString();

            // Calculate wave offset
            float offset = (float)Math.Sin(_time * WaveSpeed + i * WaveFrequency) * WaveAmplitude;
               
            Vector2 charPos = currentPos + new Vector2(0, offset);
            spriteBatch.DrawString(Font, character, charPos, Color);

            // Move to next character position
            currentPos.X += Font.MeasureString(character).X;
        }
    }

    private void DrawJitterText(SpriteBatch spriteBatch, Vector2 basePosition)
    {
        Vector2 currentPos = basePosition;

        for (int i = 0; i < Text.Length; i++)
        {
            char c = Text[i];
            string character = c.ToString();

            // Random jitter for each character
            float jitterX = ((float)_random.NextDouble() - 0.5f) * 2f * JitterAmount;
            float jitterY = ((float)_random.NextDouble() - 0.5f) * 2f * JitterAmount;
            Vector2 charPos = currentPos + new Vector2(jitterX, jitterY);

            spriteBatch.DrawString(Font, character, charPos, Color);

            // Move to next character position (use base position for consistency)
            currentPos.X += Font.MeasureString(character).X;
        }
    }

    private void DrawShakeText(SpriteBatch spriteBatch, Vector2 basePosition)
    {
        // Shake entire text as one unit (Undertale damage style)
        float shakeX = ((float)_random.NextDouble() - 0.5f) * 2f * JitterAmount;
        float shakeY = ((float)_random.NextDouble() - 0.5f) * 2f * JitterAmount;
        Vector2 shakenPos = basePosition + new Vector2(shakeX, shakeY);

        spriteBatch.DrawString(Font, Text, shakenPos, Color);
    }
    
    private void DrawRainbowText(SpriteBatch spriteBatch, Vector2 basePosition)
    {
        Vector2 currentPos = basePosition;

        for (int i = 0; i < Text.Length; i++)
        {
            char c = Text[i];
            string character = c.ToString();

            // Calculate rainbow color
            float hue = (_time * 2f + i * 0.1f) % 1f;
            Color charColor = HSVtoRGB(hue, 1f, 1f);

            spriteBatch.DrawString(Font, character, currentPos, charColor);

            currentPos.X += Font.MeasureString(character).X;
        }
    }

    // Helper method for rainbow colors
    private Color HSVtoRGB(float h, float s, float v)
    {
        int hi = (int)(h * 6) % 6;
        float f = h * 6 - hi;
        float p = v * (1 - s);
        float q = v * (1 - f * s);
        float t = v * (1 - (1 - f) * s);

        return hi switch
        {
            0 => new Color(v, t, p),
            1 => new Color(q, v, p),
            2 => new Color(p, v, t),
            3 => new Color(p, q, v),
            4 => new Color(t, p, v),
            _ => new Color(v, p, q),
        };
    }
    
    private void DrawWaveRainbowText(SpriteBatch spriteBatch, Vector2 basePosition)
    {
        Vector2 currentPos = basePosition;

        for (int i = 0; i < Text.Length; i++)
        {
            char c = Text[i];
            string character = c.ToString();

            // Wave offset
            float offset = (float)Math.Sin(_time * WaveSpeed + i * WaveFrequency) * WaveAmplitude;
        
            // Rainbow color
            float hue = (_time * 2f + i * 0.1f) % 1f;
            Color charColor = HSVtoRGB(hue, 1f, 1f);
        
            Vector2 charPos = currentPos + new Vector2(0, offset);
            spriteBatch.DrawString(Font, character, charPos, charColor);

            currentPos.X += Font.MeasureString(character).X;
        }
    }

    public void SetText(string text)
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
    
    public void SetPivotToCenter()
    {
        if (Font != null && !string.IsNullOrEmpty(Text))
        {
            Vector2 textSize = Font.MeasureString(Text);
            Origin = textSize / 2f;
        }
    }

    private List<string> CalculateLines(string text)
    {
        string substring = "";
        List<string> result = new List<string>();
        int prevSplitIndex = 0;
        int splitCounter = 0;
        
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == ' ' || text[i] == '-')
            {
                prevSplitIndex = splitCounter;
            }
            
            substring += text[i];
            if (Font.MeasureString(substring).X > Width)
            {
                result.Add(substring.Substring(0, prevSplitIndex+1));
                substring = substring.Substring(prevSplitIndex+1);
                splitCounter = 0;
            }
            splitCounter++;
        }

        if (!string.IsNullOrEmpty(substring))
        {
            result.Add(substring);
        }

        foreach (string line in result)
        {
            Console.WriteLine("-" + line);
        }
        return result;
    } */
}