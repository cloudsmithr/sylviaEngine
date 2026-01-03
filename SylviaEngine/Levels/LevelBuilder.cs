using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using SylviaEngine.Engine;
using SylviaEngine.Tilesets;
using SylviaEngine.Tilesets.Importers.Tiled;

namespace SylviaEngine.Levels;

public class LevelBuilder
{
    private readonly ContentManager _content;
    private readonly Scene _scene;
    
    public LevelBuilder(Scene scene, ContentManager content)
    {
        _scene = scene;
        _content = content;
    }
    
    public void BuildLevel(Level level)
    {
        // Create tilemap
        CreateTileMap(level.TileMapPath);
        
        // Create player
        //CreatePlayer(level.PlayerSpawnPoint);
        
        // Create enemies
        //foreach (var enemySpawn in level.EnemySpawns)
        //{
         //   CreateEnemy(enemySpawn);
        //}
        
        // Load background music
        //LoadMusic(level.BackgroundMusic);
        
        // Setup transition effects
        //SetupTransitionEffect(level.TransitionEffect);
        
        // etc...
    }
    
    private void CreateTileMap(string tileMapPath)
    {
        var importer = new TiledMapImporter();
        importer.LoadMap(tileMapPath, _content);
        
        var mapObject = new GameObject();
        mapObject.AddComponent(new TileMapRenderer(importer.ImportedMap.ToArray(), 0));
        _scene.AddGameObject(mapObject);
    }
    
    private void CreatePlayer(Vector2 spawnPoint)
    {
        var player = new GameObject();
        // ... setup player components
        _scene.AddGameObject(player);
    }
    
    /*private void CreateEnemy(EnemySpawnData spawnData)
    {
        var enemy = new GameObject();
        // ... setup enemy components based on spawn data
        _scene.AddGameObject(enemy);
    }*/
}