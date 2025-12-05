using Microsoft.Xna.Framework;

namespace SylviaEngine.Input;

public partial class InputManager
{
    // Use this for 8-directional movement. Think Link to the Past movement.
    public Vector2 GetAxis()
    {
        Vector2 result = new Vector2 { X = 0, Y = 0 };
        
        if (InputDown(InputAction.Up)) result.Y = -1;
        if (InputDown(InputAction.Down)) result.Y = 1;
        if (InputDown(InputAction.Left)) result.X = -1;
        if (InputDown(InputAction.Right)) result.X = 1;
        
        return result;
    }

    // Use this for single inputs in one of the four cardinal directions. Think menu navigation or old school dungeon crawler movement.
    public Vector2 GetAxisPressed()
    {
        Vector2 pressedResult = Vector2.Zero;
        
        if (InputPressed(InputAction.Up)) pressedResult = new Vector2 { X = 0, Y = -1 };
        else if (InputPressed(InputAction.Down)) pressedResult = new Vector2 { X = 0, Y = 1 };
        else if (InputPressed(InputAction.Left)) pressedResult = new Vector2 { X = -1, Y = 0 };
        else if (InputPressed(InputAction.Right)) pressedResult = new Vector2 { X = 1, Y = 0 };
        
        return pressedResult;
    }

    // Use this for movement snapped to four cardinal directions, think NES Zelda or old school Pokemon.
    public Vector2 GetAxisCardinal()
    {
        Vector2 joystickResult = _inputMapping.GetCardinalDirection(_gamePadState);

        if (joystickResult == Vector2.Zero)
        {
            Vector2 pressed = GetAxisPressed();
            if (pressed != Vector2.Zero) return pressed;
    
            Vector2 axis = GetAxis();
    
            // Prioritize vertical over horizontal to prevent diagonals
            if (axis.Y != 0) return new Vector2(0, axis.Y);
            if (axis.X != 0) return new Vector2(axis.X, 0);
    
            return Vector2.Zero;
        }

        return joystickResult;
    }
}