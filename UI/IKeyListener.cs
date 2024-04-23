namespace UNSERcasino.UI
{
    internal interface IKeyListener : IClickable
    {
        public void onKey(ConsoleKeyInfo key);
    }
}
