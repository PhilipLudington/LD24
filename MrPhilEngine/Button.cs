using System;
using SFML.Graphics;
using SFML.Audio;
using SFML.Window;

namespace MrPhilEngine
{
    public class Button : Entity
    {
        private ResourceManager resourceManager;
        private Sprite spriteButton;
        private Sprite spriteMouseOverButton;
        private bool mouseHover = false;
        private Sound soundClick;
        private Keyboard.Key keyShortcut;

        // Events
        public delegate void ClickEventHandler(object sender, EventArgs e);
        public event ClickEventHandler Click;

        protected virtual void OnClick(EventArgs e)
        {
            if (soundClick != null)
            {
                soundClick.Play();
            }

            if (Click != null)
                Click(this, e);
        }

        public delegate void ShortcutEventHandler(object sender, EventArgs e);
        public event ShortcutEventHandler Shortcut;
        protected virtual void OnShortcut(EventArgs e)
        {
            if (Shortcut != null)
                Shortcut(this, e);
        }

        public Button(ResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
        }

        public void SetTextureNameButton(string textureName)
        {
            Texture texture = resourceManager.GetTexture(textureName);
            spriteButton = new Sprite(texture);
            SetButtonPosition(x, y);
        }

        public void SetTextureNameButtonMouseOver(string textureName)
        {
            Texture texture = resourceManager.GetTexture(textureName);
            spriteMouseOverButton = new Sprite(texture);
            SetButtonPosition(x, y);
        }

        override public void MessageClick(int x, int y)
        {
            IntRect intRect = new IntRect(x, y, 1, 1);

            if (spriteButton.TextureRect.Intersects(intRect))
            {
                EventArgs e = new EventArgs();
                OnClick(e);
            }
        }

        override public void MessageKeyPressed(Keyboard.Key key)
        {
            if (key == keyShortcut)
            {
                EventArgs e = new EventArgs();
                OnShortcut(e);
            }
        }

        override public void Draw(RenderWindow window)
        {
            if (mouseHover
                && spriteMouseOverButton != null)
            {
                window.Draw(spriteMouseOverButton);
            }
            else
            {
                window.Draw(spriteButton);
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

        public void SetSoundClick(string soundName)
        {
            soundClick = new Sound(resourceManager.GetSound(soundName));
        }

        public void SetKeyShortcut(Keyboard.Key key)
        {
            keyShortcut = key;
        }

        public void SetButtonPosition(int x, int y)
        {
            this.x = x;
            this.y = y;

            if (spriteButton != null)
            {
                spriteButton.Position = new Vector2f(x, y);
            }

            if (spriteMouseOverButton != null)
            {
                spriteMouseOverButton.Position = new Vector2f(x, y);
            }
        }

        public int X
        {
            get
            {
                return x;
            }
            set
            {
                SetButtonPosition(value, Y);
            }
        }
        private int x;

        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                SetButtonPosition(X, value);
            }
        }
        private int y;
    }
}
