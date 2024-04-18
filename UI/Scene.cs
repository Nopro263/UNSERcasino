namespace UNSERcasino.UI
{
    internal class Scene
    {
        private List<ViewData> _views = new List<ViewData>();
        private List<int> _currentButtons = new List<int>();
        private int _currentButtonsIndex = -1;

        public Scene() { }

        public void addView(BaseView view, int x, int y) {
            if(view is ButtonView)
            {
                _currentButtonsIndex = _currentButtons.Count;
                _currentButtons.Add(_views.Count);
            }

            _views.Add(new ViewData(view, x, y));
        }

        public void onKey(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Escape: { break; }
                case ConsoleKey.UpArrow: {
                        if (_currentButtonsIndex - 1 < 0) { break; }
                        ((ButtonView)_views[_currentButtons[_currentButtonsIndex]].BaseView).Selected = false;
                        _currentButtonsIndex--;
                        ((ButtonView)_views[_currentButtons[_currentButtonsIndex]].BaseView).Selected = true;
                        break;
                }
                case ConsoleKey.DownArrow:
                    {
                        if (_currentButtonsIndex + 1 >= _currentButtons.Count) { break; }
                        if(_currentButtonsIndex >= 0)
                        {
                            ((ButtonView)_views[_currentButtons[_currentButtonsIndex]].BaseView).Selected = false;
                            _currentButtonsIndex++;
                            ((ButtonView)_views[_currentButtons[_currentButtonsIndex]].BaseView).Selected = true;
                        }
                        break;
                    }
                case ConsoleKey.Enter:
                    {
                        if (_currentButtonsIndex >= 0)
                        {
                            ((ButtonView)_views[_currentButtons[_currentButtonsIndex]].BaseView).onClick();
                        }
                        break;
                    }
            }
        }

        public void print()
        {
            Canvas c = new Canvas(Console.BufferWidth, Console.BufferHeight);
            Console.CursorVisible = false;

            if (_currentButtonsIndex >= 0)
            {
                ((ButtonView)_views[_currentButtons[_currentButtonsIndex]].BaseView).Selected = true;
            }
            

            foreach (ViewData vd in _views)
            {
                vd.BaseView.printToCanvas(c, vd.X, vd.Y);
            }

            c.show();
        }
    }

    internal class ViewData
    {
        public BaseView BaseView { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public ViewData(BaseView view, int x, int y) {
            BaseView = view;
            X = x;
            Y = y;
        }
    }
}
