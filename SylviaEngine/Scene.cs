using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using SylviaEngine.Graphics;

namespace SylviaEngine;

public class Scene : IDisposable
{
    protected ContentManager Content { get; }
    public bool IsDisposed { get; private set; }
    public ITileMapManager TileMap { get; private set; }
    private List<GameObject> _gameObjects { get; set; } = new();

    public Scene()
    {
        Content = new ContentManager(Core.Content.ServiceProvider);
        Content.RootDirectory = Core.Content.RootDirectory;
        TileMap = new TileMapManager(Content);
    }
    
    public GameObject AddGameObject(GameObject gameObject)
    {
        _gameObjects.Add(gameObject);
        return gameObject;
    }

    public void RemoveGameObject(GameObject gameObject)
    {
        _gameObjects.Remove(gameObject);
    }

    public void Update(GameTime gameTime)
    {
        foreach (var gameObject in _gameObjects)
        {
            if (gameObject.Active)
                gameObject.Update(gameTime);
        }
    }
    
    ~Scene() => Dispose(false);
    
    /// <summary>
    /// Override to provide logic to load content for the scene.
    /// </summary>
    public virtual void LoadContent() { }

    /// <summary>
    /// Unloads scene-specific content.
    /// </summary>
    public virtual void UnloadContent()
    {
        Content.Unload();
    }
    
    /// <summary>
    /// Disposes of this scene.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposes of this scene.
    /// </summary>
    /// <param name="disposing">'
    /// Indicates whether managed resources should be disposed.  This value is only true when called from the main
    /// Dispose method.  When called from the finalizer, this will be false.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed)
        {
            return;
        }

        if (disposing)
        {
            UnloadContent();
            Content.Dispose();
        }
        IsDisposed = true; 
    }
    
}