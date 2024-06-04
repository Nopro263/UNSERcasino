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

            _bet = new TextInputViewRegex(false, 5, "Bet");



            _mines = new Mines();
            _buttonview = new _2dButtonView(5, 5, '?');

            _scene.addView(_buttonview, Flow.CENTER, Flow.CENTER);

            _textview = new TextView(new Text("Total Amount: "), false, false);
            _scene.addView(_bet, Flow.CENTER, Flow.CENTER, 0, 6);
            _scene.addView(_textview, Flow.CENTER, Flow.CENTER, 0, 8);
            _scene.addView(new ButtonView(new Text("Cashout"), false), Flow.CENTER, Flow.CENTER, 0, 10);

            _mines.StartGame(1);


        }

        public override void onClick(IClickable button)
        {
            if (_mines.firstBet)
            {
                if (int.TryParse(_bet.FullContent, out int bet) && bet != 0)
                {
                    CasinoManager.Instance.Remove(bet);
                    _mines.firstBet = false;
                }
                else { _textview.Text.setContent("Please enter a valid bet"); }
            }

            ButtonView cashout = button as ButtonView;
            _2dButtonView? view = _buttonview as _2dButtonView;
            if (view != null)
            {
                if (!_mines.HasCashedout)
                {
                    if (int.TryParse(_bet.FullContent, out int bet) && bet != 0)
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
                        }
                        else if (_mines.Play(view.X, view.Y) == 3)
                        {
                            _textview.Text.setContent("Game Over");
                            _mines.HasCashedout = true;
                        }
                    }
                }
                else { _textview.Text.setContent("You have already cashed out."); }
            }
            if (cashout != null)
            {
                if (!_mines.HasCashedout)
                {
                    if (int.TryParse(_bet.FullContent, out int bet) && bet != 0)
                    {
                        if (!_mines.MineRevealed)
                        {

                            _textview.Text.setContent("You won: " + _mines.CalcMultiplier() * int.Parse(_bet.FullContent));
                            CasinoManager.Instance.Add((int)_mines.CalcMultiplier() * int.Parse(_bet.FullContent));
                            _mines.HasCashedout = true;
                        }
                        else
                        {
                            _textview.Text.setContent("You lost: " + (-int.Parse(_bet.FullContent)));
                            CasinoManager.Instance.Remove(int.Parse(_bet.FullContent));
                            _mines.HasCashedout = true;
                        }
                    }
                    else { _textview.Text.setContent("Please enter a valid bet"); }
                }
            }
        }
    }
}
