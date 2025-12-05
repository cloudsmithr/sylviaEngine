using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SylviaEngine.Input;

public partial class InputManager : IInputManager
{
    private KeyboardState _keyboardState;
    private GamePadState _gamePadState;
    private KeyboardState _prevKeyboardState;
    private GamePadState _prevGamePadState;
    
    private InputMapping _inputMapping;
    private PlayerIndex _playerIndex;

    public bool PreviousInputGamepad { get; private set; }
    
    public InputManager(InputMappingConfig mappingConfig, PlayerIndex playerIndex = PlayerIndex.One)
    {
        _playerIndex = playerIndex;
        _inputMapping = new InputMapping(mappingConfig);
    }

    public void Update()
    {
        _prevKeyboardState = _keyboardState;
        _prevGamePadState = _gamePadState;
        
        _keyboardState = Keyboard.GetState();
        _gamePadState = GamePad.GetState(_playerIndex);
    }
    
    public bool IsGamePadConnected() { return _gamePadState.IsConnected; }
    
    // Is keyboard key currently pressed down
    private bool IsKeyDown(Keys key) { return (_keyboardState.IsKeyDown(key)); }
    
    // Pushed keyboard key this frame
    private bool IsKeyPressed(Keys key) { return _keyboardState.IsKeyDown(key) && !_prevKeyboardState.IsKeyDown(key); }

    // Let go of keyboard key this frame
    private bool IsKeyReleased(Keys key) { return !_keyboardState.IsKeyDown(key) && _prevKeyboardState.IsKeyDown(key); }

    // Is holding this key down
    private bool IsKeyHeld(Keys key) { return _keyboardState.IsKeyDown(key) && _prevKeyboardState.IsKeyDown(key); }

    
    
    // Is gamepad button currently pressed down
    private bool IsPadButtonDown(Buttons button) { return (_gamePadState.IsButtonDown(button)); }

    // Pushed gamepad button this frame
    private bool IsPadButtonPressed(Buttons button) { return _gamePadState.IsButtonDown(button) && !_prevGamePadState.IsButtonDown(button); }
    
    // Let go of gamepad button this frame
    private bool IsPadButtonReleased(Buttons button) { return !_gamePadState.IsButtonDown(button) && _prevGamePadState.IsButtonDown(button); }

    // Is holding gamepad button this frame
    private bool IsPadButtonHeld(Buttons button) { return _gamePadState.IsButtonDown(button) && _prevGamePadState.IsButtonDown(button); }
    
    
    // Is Dpad dir currently pressed down
    private bool IsDPadDown(string dir) { return _inputMapping.MapDPadDown(_gamePadState, dir); }
    
    // Pushed Dpad dir this frame
    private bool IsDPadPressed(string dir) { return _inputMapping.MapDPadDown(_gamePadState, dir) && !_inputMapping.MapDPadDown(_prevGamePadState, dir); }
    
    // Let go of Dpad dir this frame
    private bool IsDPadReleased(string dir) { return !_inputMapping.MapDPadDown(_gamePadState, dir) && _inputMapping.MapDPadDown(_prevGamePadState, dir); }
    
    // Is hold Dpad dir this frame
    private bool IsDPadHeld(string dir) { return _inputMapping.MapDPadDown(_gamePadState, dir) && _inputMapping.MapDPadDown(_prevGamePadState, dir); }

    
    // Is joystick currently pushed in a direction
    private bool IsJoystickDown(string dir) { return _inputMapping.MapJoystickDown(_gamePadState, dir); }
    
    // Pushed Joystick this frame
    private bool IsJoystickPressed(string dir) { return _inputMapping.MapJoystickDown(_gamePadState, dir) && !_inputMapping.MapJoystickDown(_prevGamePadState, dir); }
    
    // Let go of Joystick this frame
    private bool IsJoystickReleased(string dir) { return !_inputMapping.MapJoystickDown(_gamePadState, dir) && _inputMapping.MapJoystickDown(_prevGamePadState, dir); }
    
    // Is hold Joystick this frame
    private bool IsJoystickHeld(string dir) { return _inputMapping.MapJoystickDown(_gamePadState, dir) && _inputMapping.MapJoystickDown(_prevGamePadState, dir); }


}