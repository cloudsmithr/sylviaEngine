using Microsoft.Xna.Framework;

namespace SylviaEngine.Input;

public interface IInputManager
{
    public bool PreviousInputGamepad { get; }

    public void Update();
    public bool IsGamePadConnected();
    public Vector2 GetAxis();
    public Vector2 GetAxisPressed();
    public Vector2 GetAxisCardinal();
    public bool InputDown(InputAction action);
    public bool InputHeld(InputAction action);
    public bool InputPressed(InputAction action);
    public bool InputReleased(InputAction action);

}