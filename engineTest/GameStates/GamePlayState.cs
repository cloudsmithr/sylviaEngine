using engineTest.Levels;
using Microsoft.Xna.Framework;
using SylviaEngine.Components;
using SylviaEngine.Engine;
using SylviaEngine.Enums;
using SylviaEngine.UI.Text;
using SylviaEngine.UI.Text.Effects;

namespace engineTest.GameStates;

public class GamePlayState : GameState
{
    public override Scene Scene { get; set; }
    
    public override void Enter()
    {
        Scene = new Scene();
        Scene.LoadContent(LevelPaths.TestLevel);
        
        GameObject test6 = Scene.AddGameObject(new GameObject(new Vector2(15,35)));
        FormattedTextRenderer animTest6 = test6.AddComponent(
            new FormattedTextRenderer(
                RenderLayer.UI,
                Core.Fonts[FontStyle.Body],
                "In the first age, the sun fell from the sky. \nThe Lord of Demons arose, casting the world into darkness. \n Humankind was before the brink of destruction. \nUntil... \n The Hero of Light appeared, to slay the Lord of Demons, and free the land from his reign of darkness.",
                Color.Red,
                width: 200,
                maxLines:4)
        );
        //animTest6.Effects.Add(new WaveTextEffect());
        //animTest6.Effects.Add(new RainbowTextEffect());
        animTest6.Effects.Add(new TypeWriterTextEffect(0.1f));
    }

    public override void Update(GameTime gameTime)
    {
       // if (Core.Input.InputReleased(InputAction.Menu))
       //     Core.States.Push(new PauseState());
       
       Scene.Update(gameTime);
    }
}