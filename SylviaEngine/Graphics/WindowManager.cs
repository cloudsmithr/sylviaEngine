using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SylviaEngine.Graphics;

public class WindowManager : IWindowManager
{
    public int RenderWidth { get; private set; }
    public int RenderHeight { get; private set; }
    public int Scale { get; private set; }

    public static IWindowManager Instance { get; set; } = new WindowManager(); // replaceable
    
    private GraphicsDevice _graphicsDevice;
    private GraphicsDeviceManager _graphicsDeviceManager;
    private bool _initialized = false;
    
    public void Init(
        GraphicsDevice graphicsDevice,
        GraphicsDeviceManager graphicsDeviceManager,
        int renderWidth = 640,
        int renderHeight = 480,
        int scale = 2)
    {
        RenderWidth = renderWidth;
        RenderHeight = renderHeight;
        Scale = scale;
        _graphicsDevice = graphicsDevice;
        _graphicsDeviceManager = graphicsDeviceManager;
        
        _graphicsDeviceManager.PreferredBackBufferWidth = RenderWidth * Scale;
        _graphicsDeviceManager.PreferredBackBufferHeight = RenderHeight * Scale;
        
        _graphicsDeviceManager.ApplyChanges();

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
        RenderSystem.Instance.RenderToWindow(RenderWidth * Scale, RenderHeight * Scale, Color.White);
    }
}