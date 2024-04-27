namespace UNSERcasino.UI
{
    internal class ButtonView : TextView, IClickable
    {
        public ButtonView(Text text, bool vertical) : base(text, vertical, false)
        {

        }

        public void deselect()
        {
            Selected = false;
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
