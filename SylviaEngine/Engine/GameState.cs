using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SylviaEngine.Engine;

public abstract class GameState
{
    public abstract Scene Scene { get; set; }
    public virtual bool UpdateBelow => false;  // should states below this one update?
    public virtual bool RenderBelow => false;  // should states below this one render?
    public virtual string Name => string.Empty;
    public virtual void Enter() { }

    public virtual void Exit()
    {
        if (Scene == null)
            return;
        
        Scene.UnloadContent();
        Scene.Dispose();
    }
    public virtual void Pause() { }   // called when another state is pushed on top
    public virtual void Resume() { }  // called when state above is popped
    
    public abstract void Update(GameTime gameTime);
    //public abstract void Render(SpriteBatch spriteBatch);
}