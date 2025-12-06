using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SylviaEngine;
using SylviaEngine.Components;
using SylviaEngine.Enums;
using SylviaEngine.Graphics;
using SylviaEngine.Input;
using SylviaEngine.UI.Text.Effects;

namespace dungeonTtest;

public class Game1 : Game
{
    private const int INTERNAL_WIDTH = 240; //480;
    private const int INTERNAL_HEIGHT = 135; //270;
    private int WINDOW_SCALE = 8;
    private GraphicsDeviceManager _graphics;
    private IInputManager _inputManager;
    private SpriteFont font;
    private Scene _scene;
    
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _inputManager = new InputManager(new InputMappingConfig());
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        WindowManager.Instance.Init(
            GraphicsDevice,
            _graphics,
            INTERNAL_WIDTH,
            INTERNAL_HEIGHT,
            WINDOW_SCALE);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        font = Content.Load<SpriteFont>("Fonts/main");
        _scene = new Scene();
        GameObject test = _scene.AddGameObject(new GameObject(new Vector2(15,5)));
        test.AddComponent(new TextRenderer(RenderLayer.UI, font));
        test.GetComponent<TextRenderer>().Text = "Hello!";
        
        test2 = _scene.AddGameObject(new GameObject(new Vector2(15,25)));
        AnimatedTextRenderer animTest2 = test2.AddComponent(
            new AnimatedTextRenderer(
                RenderLayer.UI,
                font,
            "WaveTextEffect",
                color: Color.Cyan)
        );
        animTest2.Effect = new WaveTextEffect();
        GameObject test3 = _scene.AddGameObject(new GameObject(new Vector2(15,45)));
        AnimatedTextRenderer animTest3 = test3.AddComponent(
            new AnimatedTextRenderer(
                RenderLayer.UI,
                font,
                "ShakeTextEffect",
                color: Color.Yellow)
        );
        animTest3.Effect = new ShakeTextEffect();
        
        GameObject test4 = _scene.AddGameObject(new GameObject(new Vector2(15,65)));
        AnimatedTextRenderer animTest4 = test4.AddComponent(
            new AnimatedTextRenderer(
                RenderLayer.UI,
                font,
                "JitterTextEffect",
                color: Color.Green)
        );
        animTest4.Effect = new JitterTextEffect();
            
    }

    private GameObject test2;
    
    protected override void Update(GameTime gameTime)
    {
        _inputManager.Update();

        if (_inputManager.InputReleased(InputAction.Menu))
            Exit();

        _scene.Update(gameTime);
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        WindowManager.Instance.DrawWindow();
        _scene.Update(gameTime);
        base.Draw(gameTime);
    }
}