using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SylviaEngine;
using SylviaEngine.Components;
using SylviaEngine.Enums;
using SylviaEngine.Graphics;
using SylviaEngine.Input;
using SylviaEngine.UI.Text.Effects;

namespace engineTest;

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
        
        GameObject test6 = _scene.AddGameObject(new GameObject(new Vector2(15,5)));
        FormattedTextRenderer animTest6 = test6.AddComponent(
            new FormattedTextRenderer(
                RenderLayer.UI,
                font,
                "In the first age, the sun fell from the sky. \nIt was devoured by the Bergdrache, the unholy creation of the mountain ranges of the North.",
                Color.White,
                width: 200,
                maxLines:2)
        );
        animTest6.InputManager = _inputManager;
        animTest6.Effects.Add(new RainbowTextEffect(2f, 0.5f, 0.5f));
        animTest6.Effects.Add(new ShakeTextEffect(0.55f));
        animTest6.Effects.Add(new TypeWriterTextEffect(0.5f));
    }
    
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
        base.Draw(gameTime);
    }
}