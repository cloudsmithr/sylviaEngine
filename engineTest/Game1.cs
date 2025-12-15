using engineTest.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SylviaEngine;
using SylviaEngine.Components;
using SylviaEngine.Enums;
using SylviaEngine.Graphics;
using SylviaEngine.Input;
using SylviaEngine.UI.Text.Effects;

namespace engineTest;

public class Game1 : Core
{
    private SpriteFont font;
    private Scene _scene;
    
    public Game1(): base("TestDungeon", 240, 135, false, 8)
    {
    }

    protected override void LoadContent()
    {
        font = Content.Load<SpriteFont>("Fonts/main");
        _scene = new Scene();
        
        GameObject test6 = _scene.AddGameObject(new GameObject(new Vector2(15,35)));
        FormattedTextRenderer animTest6 = test6.AddComponent(
            new FormattedTextRenderer(
                RenderLayer.UI,
                font,
                "In the first age, the sun fell from the sky. \nThe Lord of Demons devoured it, casting the world into darkness. \n Humankind was before the brink of destruction. \nUntil... \n The Hero of Light appeared, to slay the Lord of Demons, and free the land from his reign of darkness.",
                Color.Red,
                width: 200,
                maxLines:2)
        );
        animTest6.Effects.Add(new JitterTextEffect(0.5f));
        animTest6.Effects.Add(new WaveTextEffect(1f, 1f, 1f));
        animTest6.Effects.Add(new TypeWriterTextEffect(0.1f));
        
        _scene.LoadContent(LevelPaths.TestLevel);
    }
    
    protected override void Update(GameTime gameTime)
    {
        Input.Update();

        if (Input.InputReleased(InputAction.Menu))
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