namespace UNSERcasino
{
    internal class MenuManager
    {
        private Stack<BaseMenu> menus = new Stack<BaseMenu>();
        public MenuManager()
        {

        }

        public void open(BaseMenu menu)
        {
            menus.Push(menu);
        }
    }
}
