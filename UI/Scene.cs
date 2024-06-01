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

        public void addView(IView view, int x, int y)
        {
            addView(view, Flow.START, Flow.START, x, y);
        }
        public void addView(IView view, Flow x, Flow y)
        {
            addView(view, x, y, 0, 0);
        }

        public void addView(IView view, Flow xo, Flow yo, int x, int y)
        {
            if (view is IClickable)
            {
                _currentButtonsIndex = _currentButtons.Count;
                _currentButtons.Add(_views.Count);
            }

            _views.Add(new ViewData(view, xo, yo, x, y));
        }

        public void reset()
        {
            _canvas.resetAll();
        }

        public void onKey(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.Escape:
                    {
                        UI.Menu.MenuManager.close();
                        return;
                    }
                case ConsoleKey.UpArrow:
                    {
                        if (_currentButtonsIndex - 1 < 0) { break; }
                        if (_views[_currentButtons[_currentButtonsIndex]].BaseView.canSelectNext())
                        {
                            ((IClickable)_views[_currentButtons[_currentButtonsIndex]].BaseView).deselect(); // Deselect the curret
                            _currentButtonsIndex--;
                            ((IClickable)_views[_currentButtons[_currentButtonsIndex]].BaseView).select(); // Select the previous
                            return;
                        }
                        break;
                    }
                case ConsoleKey.DownArrow:
                    {
                        if (_currentButtonsIndex + 1 >= _currentButtons.Count) { break; }
                        if (_currentButtonsIndex >= 0)
                        {
                            if (_views[_currentButtons[_currentButtonsIndex]].BaseView.canSelectPrev())
                            {
                                ((IClickable)_views[_currentButtons[_currentButtonsIndex]].BaseView).deselect(); // Deselect the curret
                                _currentButtonsIndex++;
                                ((IClickable)_views[_currentButtons[_currentButtonsIndex]].BaseView).select(); // Select the next
                                return;
                            }
                        }
                        break;
                    }
                case ConsoleKey.Enter:
                    {
                        if (_currentButtonsIndex >= 0)
                        {
                            ((IClickable)_views[_currentButtons[_currentButtonsIndex]].BaseView).onClick();
                            UI.Menu.MenuManager.getTopMenu().onClick((IClickable)_views[_currentButtons[_currentButtonsIndex]].BaseView); // Let the top menu handle this
                            return;
                        }
                        break;
                    }
            }

            if (_currentButtonsIndex >= 0)
            {
                if (_views[_currentButtons[_currentButtonsIndex]].BaseView is IKeyListener)
                {
                    IKeyListener keyListener = (IKeyListener)_views[_currentButtons[_currentButtonsIndex]].BaseView; // else call onKey
                    keyListener.onKey(key);
                }
            }
        }

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

            if(MenuManager.getTopMenu() != _menu)
            {
                return;
            }

            if(_canvas.check())
            {
                return;
            }

            foreach (ViewData vd in _views)
            {

                if (_currentButtonsIndex >= 0)
                {
                    ((IClickable)_views[_currentButtons[_currentButtonsIndex]].BaseView).select(); // Re-Selecr the current button to highlight the selected button before any keypresses.
                }

                vd.BaseView.printToCanvas(_canvas, vd.getX(_canvas.getWidth()), vd.getY(_canvas.getHeight()));
            }

            if(fps != null)
            {
                _fps = (int)fps;
            }

            if (showFps)
            {
                _canvas.print(0, 0, new TextView(new Text(_fps.ToString()), false, true));
            }

            _canvas.show();
        }
    }

    internal class ViewData
    {

        public IView BaseView { get; private set; }
        public int getX(int maxX)
        {
            if (_fx == Flow.START) { return _x; }
            if (_fx == Flow.CENTER) { return (maxX - BaseView.getXSize()) / 2 + _x; }
            if (_fx == Flow.END) { return maxX - BaseView.getXSize() + _x; }

            throw new NotImplementedException();
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
