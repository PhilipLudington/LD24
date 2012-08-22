using System;
using System.IO;
using SFML.Graphics;
using System.Collections.Generic;
using SFML.Audio;

namespace MrPhilEngine
{
    public class Screen
    {
        private List<Entity> entities;
        private ResourceManager resourceManager;

        public Screen()
        {
            entities = new List<Entity>(50);
            resourceManager = new ResourceManager();
        }

        public void Draw(RenderWindow window)
        {
            foreach (Entity iEntity in entities)
            {
                iEntity.Draw(window);
            }
        }

        public Image CreateImage(string fullPathToTexture)
        {
            Image image = new Image(resourceManager, fullPathToTexture);
            entities.Add(image);

            return image;
        }

        public Button CreateButton()
        {
            Button button = new Button(resourceManager);
            entities.Add(button);

            return button;
        }

        public Sound CreateSound(string soundName)
        {
            return new Sound(resourceManager.GetSound(soundName));
        }

        public void MouseButtonPressed(SFML.Window.Mouse.Button button, int x, int y)
        {
            if (button == SFML.Window.Mouse.Button.Left)
            {
                foreach (Entity iEntity in entities)
                {
                    iEntity.ClickMessage(x, y);
                }
            }
        }

        public void MouseMoved(int x, int y)
        {
            foreach (Entity iEntity in entities)
            {
                iEntity.MessageMouseMove(x, y);
            }
        }

    }
}
