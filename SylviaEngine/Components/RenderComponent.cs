using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SylviaEngine.Engine;
using SylviaEngine.Enums;
using SylviaEngine.Graphics;
using SylviaEngine.Interfaces;

namespace SylviaEngine.Components;

public abstract class RenderComponent : IComponent, IRenderable
{
    public GameObject? Owner { get; set; }
    public RenderLayer Layer { get; set; }
    public bool Enabled { get; set; }
    public float ZIndex { get; set; }
    public Color Color { get; set; } = Color.White;
    public Vector2 Pivot { get; set; }
    public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;

    
    protected RenderComponent(RenderLayer layer, float zIndex = 0f, bool enabled = true)
    {
        Layer = layer;
        ZIndex = zIndex;
        Enabled = enabled;
    }
    
    public abstract void Render(SpriteBatch spriteBatch);

    public void Enable()
    {
        Enabled = true;
    }

    public void Disable()
    {
        Enabled = false;
    }

    // Called when component is added to GameObject
    public virtual void OnAttach()
    {
        RenderSystem.Instance.Register(this);
    }

    // Called when component is removed or destroyed
    public virtual void OnDetach()
    {
        RenderSystem.Instance.Unregister(this);
    }
    
    public void Destroy()
    {
        OnDetach();
        Owner?.RemoveComponent(this);
        Enabled = false;
        Owner = null;
    }
}