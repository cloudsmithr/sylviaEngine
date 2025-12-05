using Microsoft.Xna.Framework.Input;

namespace SylviaEngine.Input;

/// <summary>
/// Input detection methods for checking state of a button press regardless of timing.
/// See also: InputManager_HeldHandler.cs, InputManager_PressedHandler.cs, InputManager_ReleasedHandler.cs
/// </summary>
public partial class InputManager
{
    // Checks if an input is in the pressed down state this frame. 
    // Use this for inputs where you just want to check the status of an input and don't care if it's continuous or 
    // happened for the first time this frame.
    // Use this for things like run buttons where state but not timing matters.
    public bool InputDown(InputAction action)
    {
        return action switch
        {
            InputAction.Up => 
                CheckIsKeyboardDown(_inputMapping.KeyboardUp) ||
                CheckIsJoystickDown(InputMapping.Up) ||
                CheckIsDPadDown(InputMapping.Up),
            InputAction.Down =>
                CheckIsKeyboardDown(_inputMapping.KeyboardDown) ||
                CheckIsJoystickDown(InputMapping.Down) ||
                CheckIsDPadDown(InputMapping.Down),
            InputAction.Left =>
                CheckIsKeyboardDown(_inputMapping.KeyboardLeft) ||
                CheckIsJoystickDown(InputMapping.Left) ||
                CheckIsDPadDown(InputMapping.Left),
            InputAction.Right =>
                CheckIsKeyboardDown(_inputMapping.KeyboardRight) ||
                CheckIsJoystickDown(InputMapping.Right) ||
                CheckIsDPadDown(InputMapping.Right),
            InputAction.Action =>
                CheckIsKeyboardDown(_inputMapping.KeyboardAction) ||
                CheckIsPadButtonDown(_inputMapping.GamepadAction),
            InputAction.Affirm =>
                CheckIsKeyboardDown(_inputMapping.KeyboardAffirm) ||
                CheckIsPadButtonDown(_inputMapping.GamepadAffirm),
            InputAction.Cancel =>
                CheckIsKeyboardDown(_inputMapping.KeyboardCancel) ||
                CheckIsPadButtonDown(_inputMapping.GamepadCancel),
            InputAction.Inventory =>
                CheckIsKeyboardDown(_inputMapping.KeyboardInventory) ||
                CheckIsPadButtonDown(_inputMapping.GamepadInventory),
            InputAction.Menu =>
                CheckIsKeyboardDown(_inputMapping.KeyboardMenu) ||
                CheckIsPadButtonDown(_inputMapping.GamepadMenu),
            _ => false
        };        
    }
    
    private bool CheckIsKeyboardDown(Keys key)
    {
        if (IsKeyPressed(key)) PreviousInputGamepad = false;
        return IsKeyDown(key);
    }

    private bool CheckIsPadButtonDown(Buttons button)
    {
        if (!IsGamePadConnected()) return false;
        if (IsPadButtonPressed(button)) PreviousInputGamepad = true;
        return IsPadButtonDown(button);
    }

    private bool CheckIsDPadDown(string dir)
    {
        if (!IsGamePadConnected()) return false;
        if (IsDPadPressed(dir)) PreviousInputGamepad = true;
        return IsDPadDown(dir);
    }

    private bool CheckIsJoystickDown(string dir)
    {
        if (!IsGamePadConnected()) return false;
        if (IsJoystickPressed(dir)) PreviousInputGamepad = true;
        return IsJoystickDown(dir);
    }
}