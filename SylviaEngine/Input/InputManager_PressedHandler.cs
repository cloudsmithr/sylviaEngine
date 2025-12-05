using Microsoft.Xna.Framework.Input;

namespace SylviaEngine.Input;

/// <summary>
/// Input detection methods for "just pressed" input (new this frame).
/// See also: InputManager_HeldHandler.cs, InputManager_DownHandler.cs, InputManager_ReleasedHandler.cs
/// </summary>
public partial class InputManager
{
    // Checks if an input is newly pressed this frame. 
    // Use this for inputs where you want the user to tap the button, for example menu navigation.
    public bool InputPressed(InputAction action)
    {
        return action switch
        {
            InputAction.Up => 
                CheckIsKeyboardPressed(_inputMapping.KeyboardUp) ||
                CheckIsJoystickPressed(InputMapping.Up) ||
                CheckIsDPadPressed(InputMapping.Up),
            InputAction.Down =>
                CheckIsKeyboardPressed(_inputMapping.KeyboardDown) ||
                CheckIsJoystickPressed(InputMapping.Down) ||
                CheckIsDPadPressed(InputMapping.Down),
            InputAction.Left =>
                CheckIsKeyboardPressed(_inputMapping.KeyboardLeft) ||
                CheckIsJoystickPressed(InputMapping.Left) ||
                CheckIsDPadPressed(InputMapping.Left),
            InputAction.Right =>
                CheckIsKeyboardPressed(_inputMapping.KeyboardRight) ||
                CheckIsJoystickPressed(InputMapping.Right) ||
                CheckIsDPadPressed(InputMapping.Right),
            InputAction.Action =>
                CheckIsKeyboardPressed(_inputMapping.KeyboardAction) ||
                CheckIsPadButtonPressed(_inputMapping.GamepadAction),
            InputAction.Affirm =>
                CheckIsKeyboardPressed(_inputMapping.KeyboardAffirm) ||
                CheckIsPadButtonPressed(_inputMapping.GamepadAffirm),
            InputAction.Cancel =>
                CheckIsKeyboardPressed(_inputMapping.KeyboardCancel) ||
                CheckIsPadButtonPressed(_inputMapping.GamepadCancel),
            InputAction.Inventory =>
                CheckIsKeyboardPressed(_inputMapping.KeyboardInventory) ||
                CheckIsPadButtonPressed(_inputMapping.GamepadInventory),
            InputAction.Menu =>
                CheckIsKeyboardPressed(_inputMapping.KeyboardMenu) ||
                CheckIsPadButtonPressed(_inputMapping.GamepadMenu),
            _ => false
        };        
    }
    
    private bool CheckIsKeyboardPressed(Keys key)
    {
        if (!IsKeyPressed(key)) return false;

        PreviousInputGamepad = false;
        return true;
    }

    private bool CheckIsPadButtonPressed(Buttons button)
    {
        if (!IsGamePadConnected() || !IsPadButtonPressed(button)) return false;

        PreviousInputGamepad = true;
        return true;
    }

    private bool CheckIsDPadPressed(string dir)
    {
        if (!IsGamePadConnected() || !IsDPadPressed(dir)) return false;

        PreviousInputGamepad = true;
        return true;
    }

    private bool CheckIsJoystickPressed(string dir)
    {
        if (!IsGamePadConnected() || !IsJoystickPressed(dir)) return false;

        PreviousInputGamepad = true;
        return true;
    }
}