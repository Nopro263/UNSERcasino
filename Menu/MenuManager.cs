namespace UNSERcasino
{
    internal class MenuManager
    {
        private Stack<BaseMenu> menus = new Stack<BaseMenu>();
        private TimeSpan last_update = TimeSpan.Zero;
        public MenuManager()
        {

        }

        public void open(BaseMenu menu)
        {
            if (menus.Count > 0)
            {
                menus.Peek().exit(true);
            }
            menu.enter(false);
            menus.Push(menu);
        }

        public void close()
        {
            if (menus.Count > 0)
            {
                BaseMenu menu = menus.Pop();
                menu.exit(false);
                menu.destroy();
            }

            if (menus.Count > 0)
            {
                BaseMenu menu = menus.Peek();
                menu.enter(true);
            }
        }

        public void update()
        {
            if (menus.Count > 0)
            {
                Console.CursorTop = 0;
                Console.CursorLeft = 0;
                Console.CursorVisible = false;
                string[] content = menus.Peek().update();
                foreach (string item in content)
                {
                    WriteCenteredLine(item);
                }
            }
        }


        private static void WriteCenteredLine(string line)
        {
            int screenWidth = Console.WindowWidth;
            int stringWidth = line.Length;
            int spaces = (screenWidth - stringWidth) / 2;

            for (int i = 0; i < spaces; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine(line);
        }
    }
}
