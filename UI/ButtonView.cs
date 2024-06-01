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
            Text.Fg = ConsoleColor.DarkGray;
            _disabled = true;
        }

        public void enable()
        {
            Text.Fg = Canvas.FOREGROUND;
            _disabled = false;
        }

        public void onClick()
        {

        }

        public void select()
        {
            Selected = true;
        }
    }
}
