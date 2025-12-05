using Microsoft.Xna.Framework;
using SylviaEngine.Components;
using SylviaEngine.Interfaces;
using IUpdateable = SylviaEngine.Interfaces.IUpdateable;

namespace SylviaEngine;

public class GameObject
{
    public Guid Id;
    public Transform Transform { get; private set; }
    public bool Active { get; set; }
    private readonly List<IComponent> _components = new();
    private readonly List<IUpdateable> _updateables = new();
    
    public GameObject(Vector2 position = new Vector2())
    {
        Id = Guid.NewGuid();
        Transform = new Transform(position);
        Active = true;
    }
    
    public T AddComponent<T>(T component) where T : IComponent
    {
        component.Owner = this;
        _components.Add(component);
        
        if (component is RenderComponent renderable)
            renderable.OnAttach();

        if (component is IUpdateable updateable)
            _updateables.Add(updateable);
        
        return component;
    }
    
    public void RemoveComponent(IComponent c) {
        c.Disable();
        _components.Remove(c);
        
        if (c is IUpdateable updateable)
            _updateables.Remove(updateable);
    }
    
    public T? GetComponent<T>() where T : class, IComponent {
        return _components.OfType<T>().FirstOrDefault();
    }
    
    public void Update(GameTime gameTime)
    {
        // Update all components that need updating
        foreach (var component in _updateables)
        {
            if (component.Enabled)
                component.Update(gameTime);
        }
    }
}