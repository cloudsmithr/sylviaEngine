using Microsoft.Xna.Framework.Input;

namespace SylviaEngine.Input;

public class InputMappingConfig
{
    // Keyboard Input
    public Keys KeyboardUp = Keys.Up;
    public Keys KeyboardDown = Keys.Down;
    public Keys KeyboardLeft = Keys.Left;
    public Keys KeyboardRight = Keys.Right;
    public Keys KeyboardAction = Keys.E;
    public Keys KeyboardAffirm = Keys.Enter;
    public Keys KeyboardCancel = Keys.Escape;
    public Keys KeyboardInventory = Keys.Q;
    public Keys KeyboardMenu = Keys.Tab;
    
    // Gamepad Input
    public Buttons GamepadAction = Buttons.X;
    public Buttons GamepadAffirm = Buttons.A;
    public Buttons GamepadCancel = Buttons.B;
    public Buttons GamepadInventory = Buttons.Y;
    public Buttons GamepadMenu = Buttons.Start;
    
    // Gamepad config
    public float AnalogInnerDeadZone = 0.01f;
    public float AnalogOutDeadZone = 0.9f;
}