namespace UNSERcasino.UI
{
    internal interface IKeyListener : IClickable // Something that can respond to keypresses. must be selectable -> IClickable
    {
        public void onKey(ConsoleKeyInfo key);
    }
}
