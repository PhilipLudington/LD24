using System;
using SFML.Graphics;

namespace MrPhilEngine
{
    public abstract class Entity
    {
        public abstract void Draw(RenderWindow window);

        // Mouse Messages
        public abstract void ClickMessage(int x, int y);
        public abstract void MessageMouseMove(int x, int y);
    }
}
