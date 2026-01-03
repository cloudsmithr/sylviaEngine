using engineTest.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SylviaEngine.Engine;
using SylviaEngine.Graphics;
using SylviaEngine.UI.Text;

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
        Core.Fonts.AddFont(font, FontStyle.Body);
        
        States.Push(new MainMenuState());
    }
    
    protected override void Update(GameTime gameTime)
    {
        Input.Update();
        States.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        WindowManager.Instance.DrawWindow(gameTime);
        base.Draw(gameTime);
    }
}