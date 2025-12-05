using Microsoft.Xna.Framework.Input;

namespace SylviaEngine.Input;

/// <summary>
/// Input detection methods for "just released" input (new this frame).
/// See also: InputManager_DownHandler.cs, InputManager_HeldHandler.cs, InputManager_PressedHandler.cs
/// </summary>
public partial class InputManager
{
    // Checks if an input is newly released this frame. 
    // Use this for inputs where you want the check if the user just let go of a button, like firing a charge shot.
    public bool InputReleased(InputAction action)
    {
        return action switch
        {
            InputAction.Up => 
                CheckIsKeyboardReleased(_inputMapping.KeyboardUp) ||
                CheckIsJoystickReleased(InputMapping.Up) ||
                CheckIsDPadReleased(InputMapping.Up),
            InputAction.Down =>
                CheckIsKeyboardReleased(_inputMapping.KeyboardDown) ||
                CheckIsJoystickReleased(InputMapping.Down) ||
                CheckIsDPadReleased(InputMapping.Down),
            InputAction.Left =>
                CheckIsKeyboardReleased(_inputMapping.KeyboardLeft) ||
                CheckIsJoystickReleased(InputMapping.Left) ||
                CheckIsDPadReleased(InputMapping.Left),
            InputAction.Right =>
                CheckIsKeyboardReleased(_inputMapping.KeyboardRight) ||
                CheckIsJoystickReleased(InputMapping.Right) ||
                CheckIsDPadReleased(InputMapping.Right),
            InputAction.Action =>
                CheckIsKeyboardReleased(_inputMapping.KeyboardAction) ||
                CheckIsPadButtonReleased(_inputMapping.GamepadAction),
            InputAction.Affirm =>
                CheckIsKeyboardReleased(_inputMapping.KeyboardAffirm) ||
                CheckIsPadButtonReleased(_inputMapping.GamepadAffirm),
            InputAction.Cancel =>
                CheckIsKeyboardReleased(_inputMapping.KeyboardCancel) ||
                CheckIsPadButtonReleased(_inputMapping.GamepadCancel),
            InputAction.Inventory =>
                CheckIsKeyboardReleased(_inputMapping.KeyboardInventory) ||
                CheckIsPadButtonReleased(_inputMapping.GamepadInventory),
            InputAction.Menu =>
                CheckIsKeyboardReleased(_inputMapping.KeyboardMenu) ||
                CheckIsPadButtonReleased(_inputMapping.GamepadMenu),
            _ => false
        };        
    }
    
    private bool CheckIsKeyboardReleased(Keys key)
    {
        return IsKeyReleased(key);
    }

    private bool CheckIsPadButtonReleased(Buttons button)
    {
        return IsGamePadConnected() && IsPadButtonReleased(button);
    }

    private bool CheckIsDPadReleased(string dir)
    {
        return IsGamePadConnected() && IsDPadReleased(dir);
    }

    private bool CheckIsJoystickReleased(string dir)
    {
        return IsGamePadConnected() && IsJoystickReleased(dir);
    }
}