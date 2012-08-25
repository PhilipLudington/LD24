using System;
using System.IO;
using SFML.Graphics;
using System.Collections.Generic;
using SFML.Audio;
using SFML.Window;

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

        public Text CreateText(string textString, string fontName)
        {
            Font font = resourceManager.GetFont(fontName);

            Text text = new Text(textString, font);
            entities.Add(text);

            return text;
        }

        public Image CreateImage(string textureName)
        {
            Image image = new Image(resourceManager, textureName);
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
                    iEntity.MessageClick(x, y);
                }
            }
        }

        public void MessageMouseMove(int x, int y)
        {
            foreach (Entity iEntity in entities)
            {
                iEntity.MessageMouseMove(x, y);
            }
        }


        public void MessageKeyPressed(Keyboard.Key key)
        {
            foreach (Entity iEntity in entities)
            {
                iEntity.MessageKeyPressed(key);
            }
        }
    }
}
