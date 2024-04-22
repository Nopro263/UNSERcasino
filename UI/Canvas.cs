namespace UNSERcasino.UI
{
    internal class Canvas
    {
        private char?[][] _data1;
        private ConsoleColor?[][] _fg1;
        private ConsoleColor?[][] _bg1;

        private char?[][] _data2;
        private ConsoleColor?[][] _fg2;
        private ConsoleColor?[][] _bg2;

        private bool _first;

        public Canvas(int width, int height)
        {
            _first = true;

            _data1 = new char?[width][];

            for(int i = 0; i < width; i++)
            {
                _data1[i] = new char?[height];
                for (int j = 0; j < height; j++)
                {
                    _data1[i][j] = null;
                }
            }
            _data2 = new char?[width][];

            for (int i = 0; i < width; i++)
            {
                _data2[i] = new char?[height];
                for (int j = 0; j < height; j++)
                {
                    _data2[i][j] = null;
                }
            }

            _fg1 = new ConsoleColor?[width][];
            for (int i = 0; i < width; i++)
            {
                _fg1[i] = new ConsoleColor?[height];
                for (int j = 0; j < height; j++)
                {
                    _fg1[i][j] = null;
                }
            }
            _fg2 = new ConsoleColor?[width][];
            for (int i = 0; i < width; i++)
            {
                _fg2[i] = new ConsoleColor?[height];
                for (int j = 0; j < height; j++)
                {
                    _fg2[i][j] = null;
                }
            }

            _bg1 = new ConsoleColor?[width][];
            for (int i = 0; i < width; i++)
            {
                _bg1[i] = new ConsoleColor?[height];
                for (int j = 0; j < height; j++)
                {
                    _bg1[i][j] = null;
                }
            }
            _bg2 = new ConsoleColor?[width][];
            for (int i = 0; i < width; i++)
            {
                _bg2[i] = new ConsoleColor?[height];
                for (int j = 0; j < height; j++)
                {
                    _bg2[i][j] = null;
                }
            }
        }

        public void print(int x, int y, char c)
        {
            if (_first)
            {
                _data1[x][y] = c;
            } else
            {
                _data2[x][y] = c;
            }
        }

        public void setColor(int x, int y, ConsoleColor? fg = null, ConsoleColor? bg = null)
        {
            if (_first)
            {
                if (fg != null) { _fg1[x][y] = fg; }
                if (bg != null) { _bg1[x][y] = bg; }
            } else
            {
                if (fg != null) { _fg2[x][y] = fg; }
                if (bg != null) { _bg2[x][y] = bg; }
            }
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
            int imax = _data1.Length;
            int jmax = _data1[0].Length;
            for (int i = 0; i < imax; i++)
            {
                for (int j = 0; j < jmax; j++)
                {
                    char? d;
                    ConsoleColor? fg;
                    ConsoleColor? bg;

                    if(_first)
                    {
                        d = _data1[i][j];
                        fg = _fg1[i][j];
                        bg = _bg1[i][j];
                    } else
                    {
                        d = _data2[i][j];
                        fg = _fg2[i][j];
                        bg = _bg2[i][j];
                    }

                    if (d != null && (_data1[i][j] != _data2[i][j] || _fg1[i][j] != _fg2[i][j] || _bg1[i][j] != _bg2[i][j]))
                    {
                        Console.CursorLeft = i;
                        Console.CursorTop = j;

                        if (bg != null)
                        {
                            Console.BackgroundColor = (ConsoleColor)bg;
                        } else
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                        if (fg != null)
                        {
                            Console.ForegroundColor = (ConsoleColor)fg;
                        } else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        Console.Write(d);
                    }
                }
            }

            _first = !_first;
        }
    }
}
