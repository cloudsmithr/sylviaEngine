using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SylviaEngine.Input;

public class InputMapping
{
    // Keyboard Input
    public Keys KeyboardUp;
    public Keys KeyboardDown;
    public Keys KeyboardLeft;
    public Keys KeyboardRight;
    public Keys KeyboardAction;
    public Keys KeyboardAffirm;
    public Keys KeyboardCancel;
    public Keys KeyboardInventory;
    public Keys KeyboardMenu;
    
    // Gamepad Input
    public Buttons GamepadAction;
    public Buttons GamepadAffirm;
    public Buttons GamepadCancel;
    public Buttons GamepadInventory;
    public Buttons GamepadMenu;

    // Gamepad config
    public float AnalogInnerDeadZone;
    public float AnalogOutDeadZone;

    // Direction string constants
    public const string Up = "up";
    public const string Down = "down";
    public const string Left = "left";
    public const string Right = "right";

    public Vector2 Vect2Up = new Vector2(0, -1);
    public Vector2 Vect2Down = new Vector2(0, 1);
    public Vector2 Vect2Left = new Vector2(-1, 0);
    public Vector2 Vect2Right = new Vector2(1, 0);
    
    public InputMapping(InputMappingConfig config)
    {
        KeyboardUp = config.KeyboardUp;
        KeyboardDown = config.KeyboardDown;
        KeyboardLeft = config.KeyboardLeft;
        KeyboardRight = config.KeyboardRight;
        KeyboardAction = config.KeyboardAction;
        KeyboardAffirm = config.KeyboardAffirm;
        KeyboardCancel = config.KeyboardCancel;
        KeyboardInventory = config.KeyboardInventory;
        KeyboardMenu = config.KeyboardMenu;
        GamepadAction = config.GamepadAction;
        GamepadAffirm = config.GamepadAffirm;
        GamepadCancel = config.GamepadCancel;
        GamepadInventory = config.GamepadInventory;
        GamepadMenu = config.GamepadMenu;
        
        AnalogInnerDeadZone = config.AnalogInnerDeadZone;
        AnalogOutDeadZone = config.AnalogOutDeadZone;
    }
    
    public bool MapDPadDown(GamePadState gamePad, string dir)
    {
        switch (dir)
        {
            case Up:
                return gamePad.DPad.Up == ButtonState.Pressed;
            case Down:
                return gamePad.DPad.Down == ButtonState.Pressed;
            case Left:
                return gamePad.DPad.Left == ButtonState.Pressed;
            case Right:
                return gamePad.DPad.Right == ButtonState.Pressed;
            default:
                return false;
        }
    }
    
    public bool MapJoystickDown(GamePadState gamePad, string dir)
    {
        switch (dir)
        {
            case Up:
                return gamePad.ThumbSticks.Left.Y > AnalogInnerDeadZone;
            case Down:
                return gamePad.ThumbSticks.Left.Y < -AnalogInnerDeadZone;
            case Left:
                return gamePad.ThumbSticks.Left.X < -AnalogInnerDeadZone;
            case Right:
                return gamePad.ThumbSticks.Left.X > AnalogInnerDeadZone;
            default:
                return false;
        }
    }

    public Vector2 GetCardinalDirection(GamePadState gamePad, float threshold = 0.5f)
    {
        var stick = gamePad.ThumbSticks.Left;
    
        // Check if stick is pushed far enough
        if (stick.Length() < AnalogInnerDeadZone) return Vector2.Zero;
    
        // Determine which axis is more dominant
        if (Math.Abs(stick.Y) > Math.Abs(stick.X))
        {
            // Vertical is stronger
            return stick.Y < threshold ? Vect2Up : (stick.Y < -threshold ? Vect2Down : Vector2.Zero);
        }
        else
        {
            // Horizontal is stronger
            return stick.X > threshold ? Vect2Right : (stick.X < -threshold ? Vect2Left : Vector2.Zero);
        }
    }
}