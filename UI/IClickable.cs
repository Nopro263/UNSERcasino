namespace UNSERcasino.UI
{
    internal interface IClickable // Something that can be selected and clicked.
    {
        public void select();
        public void deselect();
        public void onClick();
    }
}
