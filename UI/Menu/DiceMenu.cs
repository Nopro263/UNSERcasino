using UNSERcasino.Game;

namespace UNSERcasino.UI.Menu
{
    internal class DiceMenu : Menu, IUpdateable
    {
        private DiceView[] _dices;
        private TextView _diceSum;
        private ButtonView _over;
        private ButtonView _under;
        private SliderView _betSlider;
        private TextInputView _textInput;

        private DateTime? _rollStopTime;
        private Random _random;

        private Dice _dice;

        public DiceMenu() : base()
        {
            const int col = 4;
            const int row = 3;
            int x = (Console.BufferWidth / 2) - (col * 10 / 2);
            int y = (Console.BufferHeight / 2) - (row * 6 / 2);

            _dice = new Dice();

            _random = new Random();
            _dices = new DiceView[row * col];
            _diceSum = new TextView(new Text("??"), false, false);
            _betSlider = new SliderView(14, 70, (col * 10) - 3);
            _textInput = new TextInputViewRegex(false, 5, "");

            _over = new ButtonView(new Text("Over"), false);
            _under = new ButtonView(new Text("Under"), false);

            _scene.addView(_diceSum, Flow.CENTER, Flow.START, 0, y - 1);
            _scene.addView(_over, 0, 0);
            _scene.addView(_under, 0, 1);
            _scene.addView(_textInput, 0, 2);

            for (int i = 0; i < _dices.Length; i++)
            {
                _dices[i] = new DiceView(1);
                _scene.addView(_dices[i], x, y);
                x += 10;
                if ((i + 1) % 4 == 0)
                {
                    x = (Console.BufferWidth / 2) - (col * 10 / 2);
                    y += 6;
                }
            }


            _scene.addView(_betSlider, x, y);

            //roll();
        }

        public void roll()
        {
            _rollStopTime = DateTime.Now.AddSeconds(1);

            CasinoManager.Instance.Remove(_dice.Bet);
        }

        public void Update()
        {
            TimeUtil.OnlyEvery(0.05, this);
            if (_rollStopTime != null)
            {
                if (_rollStopTime < DateTime.Now)
                {
                    _rollStopTime = null;
                    int v = 0;
                    for (int i = 0; i < _dices.Length; i++)
                    {
                        v += _dices[i].Value;
                    }

                    CasinoManager.Instance.Add(_dice.Play(v));
                    _diceSum.Text.setContent(v.ToString());
                }
                else
                {
                    _diceSum.Text.setContent("??");
                    for (int i = 0; i < _dices.Length; i++)
                    {
                        _dices[i].Value = _random.Next(1, 7); // Upper is exclusive
                    }
                }
            }


        }


        public override void onClick(IClickable i)
        {
            int x = 0;
            if (_rollStopTime == null)
            {
                if (i == _over)
                {
                    if (int.TryParse(_textInput.FullContent, out x) && x >= 1)
                    {
                        _dice.Bet = x;
                        _dice.Over = true;
                        _dice.Value = _betSlider.Value;
                        _textInput.FullContent = "";
                        roll();
                    }
                }

                if (i == _under)
                {
                    if (int.TryParse(_textInput.FullContent, out x) && x > 1)
                    {
                        _dice.Bet = x;
                        _dice.Over = false;
                        _dice.Value = _betSlider.Value;
                        _textInput.FullContent = "";
                        roll();
                    }
                }
            }
        }
    }
}
