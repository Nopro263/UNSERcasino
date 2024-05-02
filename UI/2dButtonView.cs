
namespace UNSERcasino.UI
{
    internal class _2dButtonView : IView, IKeyListener
    {
        private int _dx;
        private int _dy;
        private char[][] _chars;

        public int X { get; private set; }
        public int Y { get; private set; }
        public _2dButtonView(int dx, int dy, char def) {
            _dx = dx;
            _dy = dy;
            X = 0;
            Y = 0;

            _chars = new char[_dx][];
            for(int i = 0; i<  _dx; i++)
            {
                _chars[i] = new char[_dy];
                for (int j = 0; j < _dy; j++)
                {
                    _chars[i][j] = def;
                }
            }
        }

        public _2dButtonView(int dx, int dy) : this(dx, dy, 'X')
        {}

        public void setChar(int x, int y, char c) 
        {
            _chars[x][y] = c;
        }

        public bool canSelectNext()
        {
            return Y == 0;
        }

        public bool canSelectPrev()
        {
            return Y == _dy - 1;
        }

        public void deselect()
        {
            
        }

        public int getXSize()
        {
            return _dx;
        }

        public int getYSize()
        {
            return _dy;
        }

        public void onClick()
        {
            
        }

        public void onKey(ConsoleKeyInfo key)
        {
            switch(key.Key)
            {
                case ConsoleKey.UpArrow:
                    {
                        if(Y <= 0)
                        {
                            break;
                        }
                        Y--;
                        break;
                    }
                case ConsoleKey.DownArrow:
                    {
                        if (Y + 1 >= _dy)
                        {
                            break;
                        }
                        Y++;
                        break;
                    }
                case ConsoleKey.LeftArrow:
                    {
                        if (X <= 0)
                        {
                            break;
                        }
                        X--;
                        break;
                    }
                case ConsoleKey.RightArrow:
                    {
                        if (X + 1 >= _dx)
                        {
                            break;
                        }
                        X++;
                        break;
                    }
            }
        }

        public void printToCanvas(Canvas canvas, int x, int y)
        {
            canvas.setColor(x+X, y+Y, Canvas.BACKGROUND, Canvas.FOREGROUND);
            for(int i = 0; i < _dx; i++)
            {
                for (int j = 0; j < _dy; j++)
                {
                    canvas.print(x + i, y + j, _chars[i][j]);
                }
            }
        }

        public void select()
        {
            
        }
    }
}
