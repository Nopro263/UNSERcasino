using System.Transactions;

namespace UNSERcasino
{
    internal class MenuManager
    {
        private Stack<BaseMenu> _menus = new Stack<BaseMenu>();
        private DateTime? _lastUpdate = null;
        private static MenuManager? _instance = null;
        private bool _input = false;
        private MenuManager() {}
        public static MenuManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MenuManager();
                }
                return _instance;
            }
        }

        public void open(BaseMenu menu)
        {
            Console.Clear();
            if (_menus.Count > 0)
            {
                _menus.Peek().exit(true);
            }
            menu.enter(false);
            _menus.Push(menu);
        }

        public void close()
        {
            _input = false;
            if (_menus.Count > 0)
            {
                BaseMenu menu = _menus.Pop();
                menu.exit(false);
                menu.destroy();
            }

            if (_menus.Count > 0)
            {
                BaseMenu menu = _menus.Peek();
                menu.enter(true);
            } else
            {
                Environment.Exit(0);
            }
            Console.Clear();

            update();
        }

        private string? read()
        {
            string s = "";
            while(true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo k = Console.ReadKey();
                    if (k.Key == ConsoleKey.Escape)
                    {
                        return null;
                    }
                    if(k.Key == ConsoleKey.Enter)
                    {
                        return s;
                    } else
                    {
                        s += k.KeyChar;
                    }
                }
            }
        }

        private bool hasPressedEsc()
        {
            if(Console.KeyAvailable)
            {
                if(Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    return true;
                }
            }
            return false;
        }

        public void update()
        {
            if (_menus.Count > 0)
            {
                BaseMenu oldTop = _menus.Peek();
                string? i = "";

                if (_input)
                {
                    Console.CursorVisible = true;

                    Console.CursorLeft = 0;
                    Console.Write(new string(' ', Console.BufferWidth));
                    Console.CursorLeft = 0;

                    i = read();
                    if(i == null)
                    {
                        close();
                        return;
                    }
                    //Console.Clear();
                } else
                {
                    if(hasPressedEsc())
                    {
                        close();
                        return;
                    }
                }

                TimeSpan span = TimeSpan.Zero;
                if(_lastUpdate != null )
                {
                    span = DateTime.Now - (DateTime)_lastUpdate;
                }

                string[] content;

                if (_input)
                {
                    content = _menus.Peek().update(span, i);
                    _input = false;
                } else {
                    content = _menus.Peek().update(span);
                }

                if(_menus.Count == 0 || oldTop != _menus.Peek()) {
                    return;
                }

                Console.CursorTop = 0;
                Console.CursorLeft = 0;
                Console.CursorVisible = false;
                foreach (string item in content)
                {
                    WriteCenteredLine(item);
                }
                _lastUpdate = DateTime.Now;
            }
        }

        public void requestInput()
        {
            _input = true;
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
            //Console.WriteLine("L");
            Console.Write(line);
            for (int i = 0; i < spaces; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine();
        }
    }
}
