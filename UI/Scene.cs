namespace UNSERcasino.UI
{
    internal class Scene
    {
        private List<ViewData> _views = new List<ViewData>();
        private List<int> _currentButtons = new List<int>();
        private int _currentButtonsIndex = -1;

        private Canvas _canvas;

        public Scene() {
            _canvas = new Canvas(Console.BufferWidth, Console.BufferHeight);
        }

        public void addView(IView view, int x, int y) {
            if(view is IClickable)
            {
                _currentButtonsIndex = _currentButtons.Count;
                _currentButtons.Add(_views.Count);
            }

            _views.Add(new ViewData(view, x, y));
        }

        public void reset()
        {
            _canvas.reset();
        }

        public void onKey(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.Escape: {
                        UI.Menu.MenuManager.close();
                        return;
                }
                case ConsoleKey.UpArrow: {
                        if (_currentButtonsIndex - 1 < 0) { break; }
                        ((IClickable)_views[_currentButtons[_currentButtonsIndex]].BaseView).deselect();
                        _currentButtonsIndex--;
                        ((IClickable)_views[_currentButtons[_currentButtonsIndex]].BaseView).select();
                        break;
                }
                case ConsoleKey.DownArrow:
                    {
                        if (_currentButtonsIndex + 1 >= _currentButtons.Count) { break; }
                        if(_currentButtonsIndex >= 0)
                        {
                            ((IClickable)_views[_currentButtons[_currentButtonsIndex]].BaseView).deselect();
                            _currentButtonsIndex++;
                            ((IClickable)_views[_currentButtons[_currentButtonsIndex]].BaseView).select();
                        }
                        break;
                    }
                case ConsoleKey.Enter:
                    {
                        if (_currentButtonsIndex >= 0)
                        {
                            ((IClickable)_views[_currentButtons[_currentButtonsIndex]].BaseView).onClick();
                            UI.Menu.MenuManager.getTopMenu().onClick(((IClickable)_views[_currentButtons[_currentButtonsIndex]].BaseView));
                        }
                        break;
                    }

                default:
                    {
                        if(_views[_currentButtons[_currentButtonsIndex]].BaseView is IKeyListener)
                        {
                            IKeyListener keyListener = (IKeyListener)_views[_currentButtons[_currentButtonsIndex]].BaseView;
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
                if(vd.BaseView is IUpdateable)
                {
                    IUpdateable updateable = (IUpdateable)vd.BaseView;
                    try
                    {
                        updateable.Update();
                    } catch (SkipThisUpdateException) {}
                }

                if (_currentButtonsIndex >= 0)
                {
                    ((IClickable)_views[_currentButtons[_currentButtonsIndex]].BaseView).select();
                }

                vd.BaseView.printToCanvas(_canvas, vd.X, vd.Y);
            }

            _canvas.show();
        }
    }

    internal class ViewData
    {
        public IView BaseView { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public ViewData(IView view, int x, int y) {
            BaseView = view;
            X = x;
            Y = y;
        }
    }
}
