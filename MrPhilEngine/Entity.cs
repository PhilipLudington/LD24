using System;
using SFML.Graphics;
using SFML.Window;

namespace MrPhilEngine
{
    public abstract class Entity
    {
        public abstract void Draw(RenderWindow window);

        // Mouse Messages
        public virtual void MessageClick(int x, int y) { }
        public virtual void MessageMouseMove(int x, int y) { }

        // Key Messages
        public virtual void MessageKeyPressed(Keyboard.Key key) { }
    }
}
