using UNSERcasino.UI.Menu;

namespace UNSERcasino.UI
{
    internal class Scene
    {       
        private List<ViewData> _views = new List<ViewData>();
        private List<int> _currentButtons = new List<int>(); // List of indexes into _views, that are IClickable
        private int _currentButtonsIndex = -1;

        private Canvas _canvas;

        private int _fps = 0;
        private Menu.Menu _menu;

        public Scene(Menu.Menu menu)
        {
            _canvas = new Canvas(); // Create Canvas
            _menu = menu;
        }

        /// <summary>
        /// adds a view with static coordinates
        /// </summary>
        /// <param name="view">the view</param>
        /// <param name="x">X-Pos</param>
        /// <param name="y">Y-Pos</param>
        public void addView(IView view, int x, int y)
        {
            addView(view, Flow.START, Flow.START, x, y);
        }

        /// <summary>
        /// adds a view with only a Flow
        /// </summary>
        /// <param name="view">the view</param>
        /// <param name="x">Horizontal Flow</param>
        /// <param name="y">Vertical Flow</param>
        public void addView(IView view, Flow x, Flow y)
        {
            addView(view, x, y, 0, 0);
        }

        /// <summary>
        /// adds a view with flow and an offset from these
        /// </summary>
        /// <param name="view">the view</param>
        /// <param name="xo">Horizontal Flow</param>
        /// <param name="yo">Vertical Flow</param>
        /// <param name="x">X-Offset</param>
        /// <param name="y">Y-Offset</param>
        public void addView(IView view, Flow xo, Flow yo, int x, int y)
        {
            if (view is IClickable)
            {
                _currentButtonsIndex = _currentButtons.Count;
                _currentButtons.Add(_views.Count);
            }

            _views.Add(new ViewData(view, xo, yo, x, y));
        }

        /// <summary>
        /// reset all
        /// </summary>
        public void reset()
        {
            _canvas.resetAll();
        }

        /// <summary>
        /// called when a key is pressed
        /// </summary>
        /// <param name="key">the key pressed</param>
        public void onKey(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.Escape:
                    {
                        MenuManager.close();
                        return;
                    }
                case ConsoleKey.UpArrow:
                    {
                        if (_currentButtonsIndex - 1 < 0) { break; }

                        IView view = _views[_currentButtons[_currentButtonsIndex]].BaseView;
                        IClickable? clickable = view as IClickable;
                        IClickable? next = _views[_currentButtons[_currentButtonsIndex-1]].BaseView as IClickable;
                        if (clickable != null && next != null&& view.canSelectNext())
                        {
                            clickable.deselect(); // Deselect the curret
                            _currentButtonsIndex--;
                            next.select(); // Select the previous
                            return;
                        }
                        break;
                    }
                case ConsoleKey.DownArrow:
                    {
                        if (_currentButtonsIndex + 1 >= _currentButtons.Count) { break; }
                        if (_currentButtonsIndex >= 0)
                        {
                            IView view = _views[_currentButtons[_currentButtonsIndex]].BaseView;
                            IClickable? clickable = view as IClickable;
                            IClickable? prev = _views[_currentButtons[_currentButtonsIndex + 1]].BaseView as IClickable;
                            if (clickable != null && prev != null && view.canSelectPrev())
                            {
                                clickable.deselect(); // Deselect the curret
                                _currentButtonsIndex++;
                                prev.select(); // Select the previous
                                return;
                            }
                        }
                        break;
                    }
                case ConsoleKey.Enter:
                    {
                        if (_currentButtonsIndex >= 0)
                        {
                            IView view = _views[_currentButtons[_currentButtonsIndex]].BaseView;
                            IClickable? clickable = view as IClickable;
                            if (clickable != null)
                            {
                                clickable.onClick();
                                MenuManager.getTopMenu().onClick(clickable); // Let the top menu handle this
                            } 
                            return;
                        }
                        break;
                    }
            }

            if (_currentButtonsIndex >= 0)
            {
                if (_views[_currentButtons[_currentButtonsIndex]].BaseView is IKeyListener keyListener)
                {
                    keyListener.onKey(key);
                }
            }
        }

        /// <summary>
        /// prints the scene to the scene
        /// </summary>
        /// <param name="fps">the current fps</param>
        /// <param name="showFps">should show the fps</param>
        public void print(int? fps, bool showFps)
        {
            Console.CursorVisible = false;

            foreach(ViewData vd in _views)
            {
                if (vd.BaseView is IUpdateable) // Update the updateable
                {
                    IUpdateable updateable = (IUpdateable)vd.BaseView;
                    try
                    {
                        updateable.Update();
                    }
                    catch (SkipThisUpdateException) { }
                }
            }

            // has the update changed the top menu
            if(MenuManager.getTopMenu() != _menu)
            {
                return;
            }

            // has the aspectratio changed
            if(_canvas.check())
            {
                return;
            }

            foreach (ViewData vd in _views)
            {

                if (_currentButtonsIndex >= 0)
                {
                    ((IClickable)_views[_currentButtons[_currentButtonsIndex]].BaseView).select(); // Re-Select the current button to highlight the selected button before any keypresses.
                }

                vd.BaseView.printToCanvas(_canvas, vd.getX(_canvas.getWidth()), vd.getY(_canvas.getHeight()));
            }

            if(fps != null)
            {
                _fps = (int)fps;
            }

            if (showFps)
            {
                // put a new TextView in the upper left corner
                _canvas.print(0, 0, new TextView(new Text(_fps.ToString()), false, true));
            }

            _canvas.show();
        }
    }

    /// <summary>
    /// an IView with position-information
    /// </summary>
    internal class ViewData
    {

        public IView BaseView { get; private set; }
        public int getX(int maxX)
        {
            if (_fx == Flow.START) { return _x; }
            if (_fx == Flow.CENTER) { return (maxX - BaseView.getXSize()) / 2 + _x; }
            if (_fx == Flow.END) { return maxX - BaseView.getXSize() + _x; }

            throw new NotImplementedException(); // never the case
        }

        public int getY(int maxY)
        {
            if (_fy == Flow.START) { return _y; }
            if (_fy == Flow.CENTER) { return (maxY - BaseView.getYSize()) / 2 + _y; }
            if (_fy == Flow.END) { return maxY - BaseView.getYSize() + _y; }

            throw new NotImplementedException();
        }

        private int _x;
        private int _y;

        private Flow _fx;
        private Flow _fy;
        public ViewData(IView view, int x, int y)
        {
            BaseView = view;
            _x = x;
            _y = y;
            _fx = Flow.START;
            _fy = Flow.START;
        }
        public ViewData(IView view, Flow xo, Flow yo, int x, int y)
        {
            BaseView = view;
            _fx = xo;
            _fy = yo;
            _x = x;
            _y = y;
        }
    }

    internal enum Flow
    {
        START,
        CENTER,
        END
    }
}
