using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SylviaEngine.Graphics;

public interface IWindowManager
{
    public int RenderWidth { get; }
    public int RenderHeight { get; }
    public int Scale { get; }
    public void Init(GraphicsDevice graphicsDevice,
        GraphicsDeviceManager graphicsDeviceManager,
        GameWindow window,
        int renderWidth = 640,
        int renderHeight = 480,
        bool fullscreen = false,
        int scale = 2);
    public void DrawWindow();
    public void SetFullscreen(bool fullscreen);
}