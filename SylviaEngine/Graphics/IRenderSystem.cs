using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SylviaEngine.Components;
using SylviaEngine.Interfaces;

namespace SylviaEngine.Graphics;

public interface IRenderSystem
{
    public SpriteBatch SpriteBatch { get; set; }
    public RenderTarget2D RenderTarget { get; set; }
    public Effect CRTEffect { get; set; }

    public void Register(RenderComponent r);
    public void Unregister(RenderComponent r);
    public void RenderAll();
    public void RenderToWindow(int width, int height, Color color, GameTime gameTime);
}