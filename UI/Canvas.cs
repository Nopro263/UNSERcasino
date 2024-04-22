namespace UNSERcasino.UI
{
    internal class Canvas
    {
        private char?[][] _data;
        private ConsoleColor?[][] _fg;
        private ConsoleColor?[][] _bg;

        public Canvas(int width, int height)
        {
            _data = new char?[width][];

            for(int i = 0; i < width; i++)
            {
                _data[i] = new char?[height];
                for (int j = 0; j < height; j++)
                {
                    _data[i][j] = null;
                }
            }

            _fg = new ConsoleColor?[width][];
            for (int i = 0; i < width; i++)
            {
                _fg[i] = new ConsoleColor?[height];
                for (int j = 0; j < height; j++)
                {
                    _fg[i][j] = null;
                }
            }

            _bg = new ConsoleColor?[width][];
            for (int i = 0; i < width; i++)
            {
                _bg[i] = new ConsoleColor?[height];
                for (int j = 0; j < height; j++)
                {
                    _bg[i][j] = null;
                }
            }
        }

        public void print(int x, int y, char c)
        {
            _data[x][y] = c;
        }

        public void setColor(int x, int y, ConsoleColor? fg = null, ConsoleColor? bg = null)
        {
            if (fg != null) { _fg[x][y] = fg; }
            if (bg != null) { _bg[x][y] = bg; }
        }
        public void print(int x, int y, BaseView view)
        {
            view.printToCanvas(this, x, y);
        }



        public void print(int x, int y, string s)
        {
            for(int i = 0; i < s.Length; i++)
            {
                print(x+i, y, s[i]);
            }
        }

        public void printVertical(int x, int y, string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                print(x, y + i, s[i]);
            }
        }

        public void show()
        {
            int imax = _data.Length;
            int jmax = _data[0].Length;
            for (int i = 0; i < imax; i++)
            {
                for (int j = 0; j < jmax; j++)
                {
                    char? d = _data[i][j];
                    ConsoleColor? fg = _fg[i][j];
                    ConsoleColor? bg = _bg[i][j];

                    if (d != null)
                    {
                        Console.CursorLeft = i;
                        Console.CursorTop = j;

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;

                        if (bg != null)
                        {
                            Console.BackgroundColor = (ConsoleColor)bg;
                        }
                        if (fg != null)
                        {
                            Console.ForegroundColor = (ConsoleColor)fg;
                        }

                        Console.Write(d);
                    }
                }
            }
        }
    }
}
