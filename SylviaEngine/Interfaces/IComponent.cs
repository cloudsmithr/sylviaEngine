using SylviaEngine.Engine;

namespace SylviaEngine.Interfaces;

public interface IComponent
{
    public GameObject? Owner { get; set; }
    public bool Enabled { get; set; }

    void Enable();
    void Disable();
    
    void Destroy();
}