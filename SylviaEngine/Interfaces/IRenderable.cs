using SylviaEngine.Enums;

namespace SylviaEngine.Interfaces;

public interface IRenderable
{
    public RenderLayer Layer { get; set; }
    public float ZIndex { get; set; }
}