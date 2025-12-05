using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SylviaEngine.Enums;

namespace SylviaEngine.Components;

public class SpriteRenderer : RenderComponent
{
    public Texture2D? Texture { get; set; }
    public Rectangle? SourceRectangle { get; set; }

    public SpriteRenderer(RenderLayer layer, Texture2D? texture = null, float zIndex = 0f) 
        : base(layer, zIndex)
    {
        Texture = texture;
    }
    
    public override void Render(SpriteBatch spriteBatch)
    {
        if (Texture == null || Owner == null) return;

        spriteBatch.Draw(
            Texture,
            Owner.Transform.Position,
            SourceRectangle,
            Color,
            Owner.Transform.Rotation,
            Pivot,
            Owner.Transform.Scale,
            Effects,
            0f
        );
    }

}