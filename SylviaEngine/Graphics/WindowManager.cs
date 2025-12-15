using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SylviaEngine.Graphics;

public class WindowManager : IWindowManager
{
    public int RenderWidth { get; private set; }
    public int RenderHeight { get; private set; }
    public int Scale { get; private set; }

    public bool Fullscreen { get; private set; }
    public GameWindow Window { get; private set; }
    
    public static IWindowManager Instance { get; set; } = new WindowManager(); // replaceable
    
    private GraphicsDevice _graphicsDevice;
    private GraphicsDeviceManager _graphicsDeviceManager;
    private bool _initialized = false;
    
    public void Init(GraphicsDevice graphicsDevice,
        GraphicsDeviceManager graphicsDeviceManager,
        GameWindow window,
        int renderWidth = 640,
        int renderHeight = 480,
        bool fullscreen = false,
        int scale = 2)
    {
        RenderWidth = renderWidth;
        RenderHeight = renderHeight;
        Scale = scale;
        Fullscreen = fullscreen;
        Window = window;
        _graphicsDevice = graphicsDevice;
        _graphicsDeviceManager = graphicsDeviceManager;

        ToggleFullscreen(fullscreen);
        
        RenderSystem.Instance.SpriteBatch = new SpriteBatch(_graphicsDevice);
        RenderSystem.Instance.RenderTarget = new RenderTarget2D(_graphicsDevice, RenderWidth, RenderHeight);

        _initialized = true;
    }

    public void DrawWindow()
    {
        if (!_initialized) throw new InvalidOperationException("WindowManager.Init(...) must be called before DrawWindow.");
        if (_graphicsDevice is null) throw new InvalidOperationException("WindowManager not initialized properly.");
        
        // Set up the RenderTarget so that we can upscale the image
        _graphicsDevice.SetRenderTarget(RenderSystem.Instance.RenderTarget);
        _graphicsDevice.Clear(Color.Black);
        RenderSystem.Instance.RenderAll();
        
        // Render to the
        _graphicsDevice.SetRenderTarget(null);
        _graphicsDevice.Clear(Color.Black);

        if (Fullscreen)
            RenderSystem.Instance.RenderToWindow(
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, 
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height,
                Color.White);
        else
            RenderSystem.Instance.RenderToWindow(RenderWidth * Scale, RenderHeight * Scale, Color.White);
    }

    public void SetFullscreen(bool fullscreen)
    {
        Fullscreen = fullscreen;
    }

    public void ToggleFullscreen(bool fullscreen)
    {
        if (!fullscreen)
        {
            _graphicsDeviceManager.PreferredBackBufferWidth = RenderWidth * Scale;
            _graphicsDeviceManager.PreferredBackBufferHeight = RenderHeight * Scale;
            Window.IsBorderless = false;
        }
        else
        {
            // When we go fullscreen we have to set the PreferredBAckBufferWidth and Height to the actual monitor resolution.
            // If we don't do that Monogame will try to switch the monitor mode, which can cause issues if the resolution is very small.
            _graphicsDeviceManager.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphicsDeviceManager.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        }
        Fullscreen = fullscreen;
        _graphicsDeviceManager.IsFullScreen = fullscreen;
        _graphicsDeviceManager.ApplyChanges();
    }
}