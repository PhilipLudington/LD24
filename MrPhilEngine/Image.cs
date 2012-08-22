using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;

namespace MrPhilEngine
{
    public class Image : Entity
    {
        private ResourceManager resourceManager;

        public Image(ResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
        }

        public Image(ResourceManager resourceManager, string textureName)
            : this(resourceManager)
        {
            Texture texture = resourceManager.GetTexture(textureName);
            Sprite = new Sprite(texture);
        }

        public Sprite Sprite
        {
            get;
            set;
        }

        public override void Draw(RenderWindow window)
        {
            window.Draw(Sprite);
        }
    }
}
