namespace UNSERcasino.UI.Menu
{
    internal class DiceMenu : Menu, IUpdateable
    {
        private DiceView[] _dices;
        private TextView _diceSum;
        private SliderView _betSlider;

        private DateTime? _rollStopTime;
        private Random _random;

        public DiceMenu() : base()
        {
            const int col = 4;
            const int row = 3;
            int x = Console.BufferWidth / 2 - (col * 10 / 2);
            int y = Console.BufferHeight / 2 - (row * 6 / 2);

            _random = new Random();
            _dices = new DiceView[row * col];
            _diceSum = new TextView(new Text("??"), false, false);
            _betSlider = new SliderView(12, 72, col * 10 - 3);
            scene.addView(_diceSum, Console.BufferWidth / 2-1, y-1);

            for (int i  = 0; i < _dices.Length; i++)
            {
                _dices[i] = new DiceView(1);
                scene.addView(_dices[i], x, y);
                x += 10;
                if((i+1) % 4 == 0)
                {
                    x = Console.BufferWidth / 2 - (col * 10 / 2);
                    y += 6;
                }
            }
            

            scene.addView(_betSlider, x, y);

            //roll();
        }

        public void roll()
        {
            _rollStopTime = DateTime.Now.AddSeconds(1);
        }

        public void Update()
        {
            TimeUtil.OnlyEvery(0.05, this);
            if(_rollStopTime != null)
            {
                if(_rollStopTime < DateTime.Now)
                {
                    _rollStopTime = null;
                    int v = 0;
                    for(int i = 0; i < _dices.Length; i++)
                    {
                        v += _dices[i].Value;
                    }

                    _diceSum.Text.setContent(v.ToString());
                } else
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

            if(i == _betSlider && _rollStopTime == null)
            {
                roll();
            }
        }
    }
}
