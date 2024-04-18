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
            for (int i = 0; i < _data.Length; i++)
            {
                for (int j = 0; j < _data[0].Length; j++)
                {
                    if(_data[i][j] != null)
                    {
                        Console.CursorLeft = i;
                        Console.CursorTop = j;
                        if (_bg[i][j] != null)
                        {
                            Console.BackgroundColor = (ConsoleColor)_bg[i][j];
                        }
                        if (_fg[i][j] != null)
                        {
                            Console.ForegroundColor = (ConsoleColor)_fg[i][j];
                        }
                        Console.Write(_data[i][j]);

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                }
            }
        }
    }
}
