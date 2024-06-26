﻿namespace UNSERcasino.UI
{
    internal class Canvas
    {
        private char?[][] _data1; // The data we are building
        private ConsoleColor?[][] _fg1;
        private ConsoleColor?[][] _bg1;

        private char?[][] _currentView; // The currently shown data
        private ConsoleColor?[][] _currentFg;
        private ConsoleColor?[][] _currentBg;

        private int _width;
        private int _height;

        public static readonly ConsoleColor FOREGROUND = ConsoleColor.White;
        public static readonly ConsoleColor BACKGROUND = ConsoleColor.Black;

        public int getWidth()
        {
            return _width;
        }

        public int getHeight()
        {
            return _height;
        }

        public Canvas()
        {
            resetAll();
        }


        /// <summary>
        /// print [c] at [x],[y]
        /// </summary>
        /// <param name="x">X-pos</param>
        /// <param name="y">Y-pos</param>
        /// <param name="c">char to print</param>
        public void print(int x, int y, char c)
        {
            _data1[x][y] = c;
        }

        public void setColor(int x, int y, ConsoleColor? fg = null, ConsoleColor? bg = null)
        {
            if (fg != null && fg != FOREGROUND) { _fg1[x][y] = fg; }
            if (bg != null && bg != BACKGROUND) { _bg1[x][y] = bg; }
        }
        /// <summary>
        /// print a view to this canvas. (calls printToCanvas)
        /// </summary>
        /// <param name="x">X-pos</param>
        /// <param name="y">Y-pos</param>
        /// <param name="view">view to print</param>
        public void print(int x, int y, IView view)
        {
            view.printToCanvas(this, x, y);
        }


        /// <summary>
        /// print a string horizontal
        /// </summary>
        /// <param name="x">X-pos</param>
        /// <param name="y">Y-pos</param>
        /// <param name="s">string to print</param>
        public void print(int x, int y, string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                print(x + i, y, s[i]);
            }
        }

        /// <summary>
        /// print a string vertical
        /// </summary>
        /// <param name="x">X-pos</param>
        /// <param name="y">Y-pos</param>
        /// <param name="s">string to print</param>
        public void printVertical(int x, int y, string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                print(x, y + i, s[i]);
            }
        }

        /// <summary>
        /// reset the buffer for our current output
        /// </summary>
        public void resetWorking()
        {
            for (int i = 0; i < _width; i++)
            {
                _data1[i] = new char?[_height];
                for (int j = 0; j < _height; j++)
                {
                    _data1[i][j] = null;
                }
            }

            for (int i = 0; i < _width; i++)
            {
                _fg1[i] = new ConsoleColor?[_height];
                for (int j = 0; j < _height; j++)
                {
                    _fg1[i][j] = null;
                }
            }

            for (int i = 0; i < _width; i++)
            {
                _bg1[i] = new ConsoleColor?[_height];
                for (int j = 0; j < _height; j++)
                {
                    _bg1[i][j] = null;
                }
            }
        }

        /// <summary>
        /// reset the buffer of the previous outputed values
        /// </summary>
        public void resetCurrent()
        {
            for (int i = 0; i < _width; i++)
            {
                _currentView[i] = new char?[_height];
                for (int j = 0; j < _height; j++)
                {
                    _currentView[i][j] = null;
                }
            }

            for (int i = 0; i < _width; i++)
            {
                _currentFg[i] = new ConsoleColor?[_height];
                for (int j = 0; j < _height; j++)
                {
                    _currentFg[i][j] = null;
                }
            }

            for (int i = 0; i < _width; i++)
            {
                _currentBg[i] = new ConsoleColor?[_height];
                for (int j = 0; j < _height; j++)
                {
                    _currentBg[i][j] = null;
                }
            }
        }

        public bool check()
        {
            if(Console.BufferHeight != _height || Console.BufferWidth != _width)
            {
                resetAll();
                return true;
            }
            return false;
        }

        public void resetAll()
        {
            _width = Console.BufferWidth;
            _height = Console.BufferHeight;

            _data1 = new char?[_width][];
            _currentView = new char?[_width][];
            _fg1 = new ConsoleColor?[_width][];
            _currentFg = new ConsoleColor?[_width][];
            _bg1 = new ConsoleColor?[_width][];
            _currentBg = new ConsoleColor?[_width][];

            resetWorking();
            resetCurrent();

            Console.BackgroundColor = BACKGROUND;
            Console.ForegroundColor = FOREGROUND;

            Console.Clear();
        }

        /// <summary>
        /// print the canvas to screen
        /// </summary>
        public void show()
        {
            try
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

                        d = _data1[i][j];
                        fg = _fg1[i][j];
                        bg = _bg1[i][j];

                        if (_data1[i][j] != _currentView[i][j] ||
                            _fg1[i][j] != _currentFg[i][j] ||
                            _bg1[i][j] != _currentBg[i][j]) // sth. has changed
                        {
                            Console.CursorLeft = i;
                            Console.CursorTop = j;

                            if (bg != null)
                            {
                                Console.BackgroundColor = (ConsoleColor)bg;
                                _currentBg[i][j] = bg;
                            }
                            else
                            {
                                Console.BackgroundColor = BACKGROUND;
                                _currentBg[i][j] = null;
                            }
                            if (fg != null)
                            {
                                Console.ForegroundColor = (ConsoleColor)fg;
                                _currentFg[i][j] = fg;
                            }
                            else
                            {
                                Console.ForegroundColor = FOREGROUND;
                                _currentFg[i][j] = null;
                            }

                            if (d != null)
                            {
                                Console.Write(d);
                                _currentView[i][j] = d;
                            }
                            else
                            {
                                Console.Write(' ');
                                _currentView[i][j] = null;
                            }
                        }
                    }
                }
                resetWorking();
            } catch (Exception e)
            {
                resetAll();
            }
        }

    }
}
