using UNSERcasino.Game;

namespace UNSERcasino.UI.Menu
{
    internal class MineMenu : Menu
    {
        private Mines _mines;
        private _2dButtonView _buttonview;
        private TextView _textview;

        public MineMenu() : base()
        {
            const int col = 5;
            const int row = 5;
            int x = (Console.BufferWidth / 2) - (col * 10 / 2);
            int y = (Console.BufferHeight / 2) - (row * 6 / 2);




            _mines = new Mines();
            _buttonview = new _2dButtonView(5, 5, '?');

            scene.addView(_buttonview, Flow.CENTER, Flow.CENTER);

            _textview = new TextView(new Text("Total Amount: "), false, false);
            scene.addView(_textview, Flow.CENTER, Flow.CENTER, 0, 8);
            scene.addView(new ButtonView(new Text("Cashout"), false), Flow.CENTER, Flow.CENTER, 0, 10);
            _mines.StartGame(1);


        }

        public override void onClick(IClickable button)
        {


            _2dButtonView? view = _buttonview as _2dButtonView;
            if (view != null)
            {
                if (_mines.Play(view.X, view.Y) != 0)
                {
                    view.setChar(view.X, view.Y, 'W');
                    _textview.Text.setContent("Multiplier: " + (_mines.Play(view.X, view.Y) )); //add Bets
                }
                else if (_mines.Play(view.X, view.Y) == 0)
                {
                    view.setChar(view.X, view.Y, 'L');
                    _textview.Text.setContent("Multiplier: " + _mines.Play(view.X, view.Y));

                }
            }
        }
    }
}
