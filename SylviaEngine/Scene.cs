using Microsoft.Xna.Framework;

namespace SylviaEngine;

public class Scene
{
    private List<GameObject> _gameObjects { get; set; } = new();

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
}