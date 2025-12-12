using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SylviaEngine.Graphics;
using SylviaEngine.Input;

namespace SylviaEngine;

public class Core : Game
{
    internal static Core s_instance;
    public static Core Instance => s_instance;
    public static GraphicsDeviceManager Graphics { get; private set; }
    public static new GraphicsDevice GraphicsDevice { get; private set; }
    public static new ContentManager Content { get; private set; }
    public static IInputManager Input { get; private set; }

    private string _title;
    private int _width;
    private int _height;
    private bool _fullscreen;
    private int _windowScale;


    public Core(string title, int width, int height, bool fullscreen, int windowScale = 1)
    {
        // Ensure that multiple cores are not created.
        if (s_instance != null)
        {
            throw new InvalidOperationException($"Only a single Core instance can be created");
        }
        
        s_instance = this;
        Graphics = new GraphicsDeviceManager(this);

        _title = title;
        _width = width;
        _height = height;
        _fullscreen = fullscreen;
        _windowScale = windowScale;
        
        // Set the core's content manager to a reference of the base Game's
        // content manager.
        Content = base.Content;

        // Set the root directory for content.
        Content.RootDirectory = "Content";

        // Mouse is visible by default.
        IsMouseVisible = true;
    }
    
    protected override void Initialize()
    {
        Input = new InputManager(new InputMappingConfig(), PlayerIndex.One);
        Window.Title = _title;
        GraphicsDevice = base.GraphicsDevice;

        WindowManager.Instance.Init(
            GraphicsDevice,
            Graphics,
            Window,
            _width,
            _height,
            _fullscreen,
            _windowScale);
        base.Initialize();
    }
}