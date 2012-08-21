using System;
using SFML.Graphics;

namespace MrPhilEngine
{
    public class Button : Entity
    {
        private ResourceManager resourceManager;
        private Sprite spriteButton;
        private Sprite spriteMouseOverButton;
        private bool mouseHover = false;

        public delegate void ClickEventHandler(object sender, EventArgs e);
        public event ClickEventHandler Click;

        public Button(ResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
        }

        public void SetTextureNameButton(string textureName)
        {
            Texture texture = resourceManager.GetTexture(textureName);
            spriteButton = new Sprite(texture);
        }

        public void SetTextureNameButtonMouseOver(string textureName)
        {
            Texture texture = resourceManager.GetTexture(textureName);
            spriteMouseOverButton = new Sprite(texture);
        }

        protected virtual void OnClick(EventArgs e)
        {
            if (Click != null)
                Click(this, e);
        }

        override public void ClickMessage(int x, int y)
        {
        }

        override public void Draw(RenderWindow window)
        {
            if (mouseHover)
            {
                window.Draw(spriteButton);
            }
            else
            {
                window.Draw(spriteMouseOverButton);
            }
        }

        public override void MessageMouseMove(int x, int y)
        {
            IntRect intRect = new IntRect(x, y, 1, 1);

            if (spriteButton.TextureRect.Intersects(intRect))
            {
                mouseHover = true;
            }
            else
            {
                mouseHover = false;
            }
        }
    }
}
