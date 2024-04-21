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
            Menu.MenuManager.getTopMenu().onClick(this);
        }

        public void select()
        {
            Selected = true;
        }
    }
}
