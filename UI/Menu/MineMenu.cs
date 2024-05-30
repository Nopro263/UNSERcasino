using UNSERcasino.Game;

namespace UNSERcasino.UI.Menu
{
    internal class MineMenu : Menu
    {
        private Mines _mines;
        private _2dButtonView _buttonview;
        private TextView _textview;
        private TextInputView _bet;
        public MineMenu() : base()
        {
            const int col = 5;
            const int row = 5;
            int x = (Console.BufferWidth / 2) - (col * 10 / 2);
            int y = (Console.BufferHeight / 2) - (row * 6 / 2);

            _bet = new TextInputView(false, 5, "Bet");



            _mines = new Mines();
            _buttonview = new _2dButtonView(5, 5, '?');

            scene.addView(_buttonview, Flow.CENTER, Flow.CENTER);

            _textview = new TextView(new Text("Total Amount: "), false, false);
            scene.addView(_bet, Flow.CENTER, Flow.CENTER, 0, 6);
            scene.addView(_textview, Flow.CENTER, Flow.CENTER, 0, 8);
            scene.addView(new ButtonView(new Text("Cashout"), false), Flow.CENTER, Flow.CENTER, 0, 10);

            _mines.StartGame(1);


        }

        public override void onClick(IClickable button)
        {

            ButtonView cashout = button as ButtonView;
            _2dButtonView? view = _buttonview as _2dButtonView;
            if (view != null)
            {
                if (_mines.Play(view.X, view.Y) != 0 && _mines.Play(view.X, view.Y) != 3)
                {
                    view.setChar(view.X, view.Y, '\u25C7');
                    _textview.Text.setContent("Multiplier: " + (_mines.Play(view.X, view.Y)));
                }
                else if (_mines.Play(view.X, view.Y) == 0)
                {
                    view.setChar(view.X, view.Y, '\u25CB');
                    _textview.Text.setContent("Multiplier: " + _mines.Play(view.X, view.Y));
                    _mines.MineRevealed = true;
                }
                else if (_mines.Play(view.X, view.Y) == 3)
                {
                    _textview.Text.setContent("Game Over");
                }
            }
            if (cashout != null)
            {

                if (int.TryParse(_bet.FullContent) != null)
                {
                    if (!_mines.MineRevealed)
                    {

                        _textview.Text.setContent("You won: " + _mines.CalcMultiplier() * int.Parse(_bet.FullContent));
                        CasinoManager.Instance.add(_mines.CalcMultiplier() * int.Parse(_bet.FullContent));
                    }
                    else
                    {
                        _textview.Text.setContent("You lost: " + (-int.Parse(_bet.FullContent)));
                        CasinoManager.Instance.add(-int.Parse(_bet.FullContent));
                    }
                }
                else { _textview.Text.setContent("Please enter a valid bet"); }
            }
        }
    }
}
