using Microsoft.Xna.Framework;

namespace SylviaEngine.Components;

public class Transform
{
    public Vector2 Position { get; set; }
    public float Rotation { get; set; } // In radians
    public Vector2 Scale { get; set; }
    
    public Transform(Vector2 position)
    {
        Position = position;
        Rotation = 0f;
        Scale = Vector2.One;
    }
    
    public Transform(Vector2 position, float rotation, Vector2 scale)
    {
        Position = position;
        Rotation = rotation;
        Scale = scale;
    }
    
    // Helper for moving
    public void Translate(Vector2 delta)
    {
        Position += delta;
    }
    
    // Helper for rotating
    public void Rotate(float radians)
    {
        Rotation += radians;
    }
}