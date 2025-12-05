using Microsoft.Xna.Framework;

namespace SylviaEngine.Interfaces;

public interface IUpdateable
{
    public void Update(GameTime gameTime);
    public bool Enabled { get; set; }
}