using System;

namespace MrPhilEngine
{
    public class Button
    {
        public delegate void ClickEventHandler(object sender, EventArgs e);
        public event ClickEventHandler Click;

        protected virtual void OnChanged(EventArgs e)
        {
            if (Click != null)
                Click(this, e);
        }
    }
}
