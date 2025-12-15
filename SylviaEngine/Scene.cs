using System.Text.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using SylviaEngine.Levels;
using SylviaEngine.Tilesets;
using SylviaEngine.Tilesets.Importers.Tiled;

namespace SylviaEngine;

public class Scene : IDisposable
{
    protected ContentManager Content { get; }
    public bool IsDisposed { get; private set; }
    public Level? Level { get; private set; }
    
    private List<GameObject> _gameObjects = new();
    
    public Scene()
    {
        Content = new ContentManager(Core.Content.ServiceProvider);
        Content.RootDirectory = Core.Content.RootDirectory;
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

    public virtual void LoadContent(string levelPath)
    {
        // Clear previous level
        UnloadContent();
        
        // Load level config
        Level = LoadLevel(levelPath);
        
        // Create tilemap GameObject
        var mapObject = CreateTileMapObject(Level.TileMapPath);
        AddGameObject(mapObject); // ✅ Don't forget this!
    }
    
    private Level LoadLevel(string levelPath)
    {
        string fullPath = Path.Combine(Content.RootDirectory, levelPath);
        string json = File.ReadAllText(fullPath);
        return JsonSerializer.Deserialize<Level>(json) 
            ?? throw new FileLoadException($"Could not load level: {levelPath}");
    }
    
    private GameObject CreateTileMapObject(string tileMapPath)
    {
        var importer = new TiledMapImporter();
        importer.LoadMap(tileMapPath, Content);
        
        var mapObject = new GameObject();
        mapObject.AddComponent(new TileMapRenderer(importer.ImportedMap.ToArray(), 0));
        return mapObject;
    }

    public virtual void UnloadContent()
    {
        // Dispose in reverse order for possible dependency chains
        for (int i = _gameObjects.Count - 1; i >= 0; i--)
        {
            _gameObjects[i].Dispose();
        }
        
        _gameObjects.Clear();
        Content.Unload();
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed) return;

        if (disposing)
        {
            UnloadContent();
            Content.Dispose();
        }
        
        IsDisposed = true;
    }
    
    ~Scene() => Dispose(false);
}