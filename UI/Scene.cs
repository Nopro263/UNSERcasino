namespace UNSERcasino.UI
{
    internal class Scene
    {
        private List<ViewData> _views = new List<ViewData>();
        private List<int> _currentButtons = new List<int>(); // List of indexes into _views, that are IClickable
        private int _currentButtonsIndex = -1;

        private Canvas _canvas;

        public Scene()
        {
            _canvas = new Canvas(Console.BufferWidth, Console.BufferHeight); // Create Canvas
        }

        public void addView(IView view, int x, int y)
        {
            if (view is IClickable)
            {
                _currentButtonsIndex = _currentButtons.Count;
                _currentButtons.Add(_views.Count);
            }

            _views.Add(new ViewData(view, x, y)); // ViewData combined View and position
        }

        public void addView(IView view, Flow x, Flow y)
        {
            if (view is IClickable)
            {
                _currentButtonsIndex = _currentButtons.Count;
                _currentButtons.Add(_views.Count);
            }

            _views.Add(new ViewData(view, x, y));
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
                        ((IClickable)_views[_currentButtons[_currentButtonsIndex]].BaseView).deselect(); // Deselect the curret
                        _currentButtonsIndex--;
                        ((IClickable)_views[_currentButtons[_currentButtonsIndex]].BaseView).select(); // Select the previous
                        break;
                    }
                case ConsoleKey.DownArrow:
                    {
                        if (_currentButtonsIndex + 1 >= _currentButtons.Count) { break; }
                        if (_currentButtonsIndex >= 0)
                        {
                            ((IClickable)_views[_currentButtons[_currentButtonsIndex]].BaseView).deselect(); // Deselect the curret
                            _currentButtonsIndex++;
                            ((IClickable)_views[_currentButtons[_currentButtonsIndex]].BaseView).select(); // Select the next
                        }
                        break;
                    }
                case ConsoleKey.Enter:
                    {
                        if (_currentButtonsIndex >= 0)
                        {
                            ((IClickable)_views[_currentButtons[_currentButtonsIndex]].BaseView).onClick();
                            UI.Menu.MenuManager.getTopMenu().onClick((IClickable)_views[_currentButtons[_currentButtonsIndex]].BaseView); // Let the top menu handle this
                        }
                        break;
                    }

                default:
                    {
                        if (_views[_currentButtons[_currentButtonsIndex]].BaseView is IKeyListener)
                        {
                            IKeyListener keyListener = (IKeyListener)_views[_currentButtons[_currentButtonsIndex]].BaseView; // else call onKey
                            keyListener.onKey(key);
                        }
                        break;
                    }
            }
        }

        public void print()
        {
            Console.CursorVisible = false;

            foreach (ViewData vd in _views)
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

                if (_currentButtonsIndex >= 0)
                {
                    ((IClickable)_views[_currentButtons[_currentButtonsIndex]].BaseView).select(); // Re-Selecr the current button to highlight the selected button before any keypresses.
                }

                vd.BaseView.printToCanvas(_canvas, vd.getX(_canvas.getWidth()), vd.getY(_canvas.getHeight()));
            }

            _canvas.show();
        }
    }

    internal class ViewData
    {

        public IView BaseView { get; private set; }
        public int getX(int maxX)
        {
            if (_x != null)
            {
                return (int)_x;
            }
            else // calculate position with getXSize
            {
                if (_fx == Flow.START) { return 0; }
                if (_fx == Flow.CENTER) { return (maxX - BaseView.getXSize()) / 2; }
                if (_fx == Flow.END) { return maxX - BaseView.getXSize(); }

                throw new NotImplementedException();
            }
        }

        public int getY(int maxY)
        {
            if (_y != null)
            {
                return (int)_y;
            }
            else // calculate position with getYSize
            {
                if (_fy == Flow.START) { return 0; }
                if (_fy == Flow.CENTER) { return (maxY - BaseView.getYSize()) / 2; }
                if (_fy == Flow.END) { return maxY - BaseView.getYSize(); }

                throw new NotImplementedException();
            }
        }

        private int? _x;
        private int? _y;

        private Flow? _fx;
        private Flow? _fy;
        public ViewData(IView view, int x, int y)
        {
            BaseView = view;
            _x = x;
            _y = y;
        }
        public ViewData(IView view, Flow x, Flow y)
        {
            BaseView = view;
            _fx = x;
            _fy = y;
        }
    }

    internal enum Flow
    {
        START,
        CENTER,
        END
    }
}
