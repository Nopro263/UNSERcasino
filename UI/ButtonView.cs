namespace UNSERcasino.UI
{
    internal class ButtonView : TextView, IClickable
    {
        private bool _disabled;
        public ButtonView(Text text, bool vertical) : base(text, vertical, false)
        {

        }

        public void deselect()
        {
            Selected = false;
        }

        public void disable()
        {
            Text.Bg = ConsoleColor.DarkGray;
            Selected = false;
            _disabled = true;
        }

        public void enable()
        {
            Text.Bg = Canvas.BACKGROUND;
            _disabled = false;
        }

        public void onClick()
        {

        }

        public void select()
        {
            if(!_disabled)
            {
                Selected = true;
            }
        }
    }
}
