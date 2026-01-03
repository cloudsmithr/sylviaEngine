using Microsoft.Xna.Framework;

namespace SylviaEngine.Interfaces;

public interface IGameState : IDisposable
{
    // Called when the state becomes active (pushed or revealed)
    void OnEnter();
    // Called when the state is covered (another state pushed on top) or removed
    void OnExit();

    void Update(GameTime gameTime);
    void Draw(GameTime gameTime);

    // Controls stack behavior
    bool BlocksUpdateBelow { get; }
    bool BlocksDrawBelow   { get; }
    bool BlocksInputBelow  { get; }
}