using Microsoft.Xna.Framework;
using SylviaEngine.Components;
using SylviaEngine.Engine;
using SylviaEngine.Enums;
using SylviaEngine.Input;
using SylviaEngine.UI.Text;
using SylviaEngine.UI.Text.Effects;

namespace engineTest.GameStates;

public class MainMenuState : GameState
{
    public override Scene Scene { get; set; }

    public override void Enter()
    {
        Scene = new Scene();
        
        GameObject test6 = Scene.AddGameObject(new GameObject(new Vector2(15,35)));
        FormattedTextRenderer animTest6 = test6.AddComponent(
            new FormattedTextRenderer(
                RenderLayer.UI,
                Core.Fonts[FontStyle.Body],
                "WELCOME ASSHOLES",
                Color.Red,
                width: 200,
                maxLines:4)
        );
        animTest6.Effects.Add(new WaveTextEffect());
        animTest6.Effects.Add(new RainbowTextEffect());
    }

    public override void Update(GameTime gameTime)
    {
        if (Core.Input.InputReleased(InputAction.Menu))
             Core.States.Switch(new GamePlayState());
       
        Scene.Update(gameTime);
    }
}