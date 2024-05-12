using UNSERcasino.Game;

namespace UNSERcasino.UI.Menu
{
    internal class MineMenu : Menu
    {
        private Mines _mines;
        private _2dButtonView _buttonview;

        public MineMenu() : base()
        {
            const int col = 5;
            const int row = 5;
            int x = (Console.BufferWidth / 2) - (col * 10 / 2);
            int y = (Console.BufferHeight / 2) - (row * 6 / 2);

            _mines = new Mines();
            _buttonview = new _2dButtonView(5, 5, '?');

            scene.addView(_buttonview, Flow.CENTER, Flow.CENTER);
        }

        public override void onClick(IClickable button)
        {
            _2dButtonView? view = _buttonview as _2dButtonView;
            if (view != null)
            {
                view.setChar(view.X, view.Y, 'X');
            }
        }
    }
}
