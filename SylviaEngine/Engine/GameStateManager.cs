using Microsoft.Xna.Framework;

namespace SylviaEngine.Engine;

public class GameStateManager
{

    private readonly List<GameState> _states = new();
    
    public void Push(GameState state)
    {
        if (_states.Count > 0)
            _states[^1].Pause();
        
        _states.Add(state);
        state.Enter();
    }
    
    public void Pop()
    {
        if (_states.Count == 0) return;
        
        GameState state = _states[^1];
        _states.RemoveAt(_states.Count - 1);
        state.Exit();
        
        if (_states.Count > 0)
            _states[^1].Resume();
        //else
        //    Core.Instance.Exit();
    }
    
    public void Switch(GameState state)
    {
        while (_states.Count > 0)
            Pop();
        
        Push(state);
        state.Enter();
    }
    
    public void Update(GameTime gameTime)
    {
        if (_states.Count == 0) return;
        
        foreach (GameState state in _states) // top-down iteration
        {
            state.Update(gameTime);
            if (!state.UpdateBelow)
                break;
        }
    }
}