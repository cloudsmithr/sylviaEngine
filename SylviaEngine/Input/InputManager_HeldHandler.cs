using Microsoft.Xna.Framework.Input;

namespace SylviaEngine.Input;

/// <summary>
/// Input detection methods for "held" input (continuous across frames).
/// See also: InputManager_DownHandler.cs, InputManager_PressedHandler.cs, InputManager_ReleasedHandler.cs
/// </summary>
public partial class InputManager
{
    // Checks if an input is pressed down this frame and was already pressed the previous frame. 
    // Use this for inputs where you want the user to continuously hold the button, for example charge shots.
    public bool InputHeld(InputAction action)
    {
        return action switch
        {
            InputAction.Up => 
                CheckIsKeyboardHeld(_inputMapping.KeyboardUp) ||
                CheckIsJoystickHeld(InputMapping.Up) ||
                CheckIsDPadHeld(InputMapping.Up),
            InputAction.Down =>
                CheckIsKeyboardHeld(_inputMapping.KeyboardDown) ||
                CheckIsJoystickHeld(InputMapping.Down) ||
                CheckIsDPadHeld(InputMapping.Down),
            InputAction.Left =>
                CheckIsKeyboardHeld(_inputMapping.KeyboardLeft) ||
                CheckIsJoystickHeld(InputMapping.Left) ||
                CheckIsDPadHeld(InputMapping.Left),
            InputAction.Right =>
                CheckIsKeyboardHeld(_inputMapping.KeyboardRight) ||
                CheckIsJoystickHeld(InputMapping.Right) ||
                CheckIsDPadHeld(InputMapping.Right),
            InputAction.Action =>
                CheckIsKeyboardHeld(_inputMapping.KeyboardAction) ||
                CheckIsPadButtonHeld(_inputMapping.GamepadAction),
            InputAction.Affirm =>
                CheckIsKeyboardHeld(_inputMapping.KeyboardAffirm) ||
                CheckIsPadButtonHeld(_inputMapping.GamepadAffirm),
            InputAction.Cancel =>
                CheckIsKeyboardHeld(_inputMapping.KeyboardCancel) ||
                CheckIsPadButtonHeld(_inputMapping.GamepadCancel),
            InputAction.Inventory =>
                CheckIsKeyboardHeld(_inputMapping.KeyboardInventory) ||
                CheckIsPadButtonHeld(_inputMapping.GamepadInventory),
            InputAction.Menu =>
                CheckIsKeyboardHeld(_inputMapping.KeyboardMenu) ||
                CheckIsPadButtonHeld(_inputMapping.GamepadMenu),
            _ => false
        };        
    }
    
    private bool CheckIsKeyboardHeld(Keys key)
    {
        return IsKeyHeld(key);
    }

    private bool CheckIsPadButtonHeld(Buttons button)
    {
        return IsGamePadConnected() && IsPadButtonHeld(button);
    }

    private bool CheckIsDPadHeld(string dir)
    {
        return IsGamePadConnected() && IsDPadHeld(dir);
    }

    private bool CheckIsJoystickHeld(string dir)
    {
        return IsGamePadConnected() && IsJoystickHeld(dir);
    }
}