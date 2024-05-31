using UNSERcasino.Game.Poker.Eval;

namespace UNSERcasino.UI.Menu
{
    internal class CrashMenu : Menu, IUpdateable
    {
        private DiceView _view;
        private Card _card;
        private Text _text;
        public CrashMenu() : base()
        {
            _view = new DiceView(1);
            /*scene.addView(_view, 0, 0);
            scene.addView(new SliderView(10, 20, 10), 10, 10);
            scene.addView(new ButtonView(new Text("Bet"), false), 10, 11);

            scene.addView(new TextInputView(false, 10), 20, 0);

            */_card = new Card(CardValue.ASS, CardType.Herz, false);/*

            scene.addView(new CardView(_card), Flow.CENTER, Flow.CENTER);*/

            scene.addView(new _2dButtonView(3, 3), Flow.CENTER, Flow.CENTER);

            _text = new Text("abc");

            scene.addView(new TextView(_text, false, false), Flow.START, Flow.START);

            int d = Evaluator.Eval(
                new Card[]
                {
                    new Card(CardValue.ASS, CardType.Kreuz, false),
                    new Card(CardValue.KOENIG, CardType.Kreuz, false),
                    new Card(CardValue.DAME, CardType.Kreuz, false),
                    new Card(CardValue.BUB, CardType.Kreuz, false),
                    new Card(CardValue.ZEHN, CardType.Kreuz, false),
                },
                new Card[]
                {
                    new Card(CardValue.NEUN, CardType.Herz, false),
                    new Card(CardValue.ACHT, CardType.Karo, false),
                }
                );
        }

        public void Update()
        {
            TimeUtil.OnlyEvery(1, this);
            int nval = _view.Value + 1;
            if (nval == 7)
            {
                nval = 1;
            }
            _view.Value = nval;

            _card.Hidden = !_card.Hidden;
        }

        public override void onClick(IClickable button)
        {
            _2dButtonView? _2DButtonView = button as _2dButtonView;

            if( _2DButtonView != null )
            {
                _text.setContent($"{_2DButtonView.X},{_2DButtonView.Y}");

                _2DButtonView.setChar(_2DButtonView.X, _2DButtonView.Y, '.');
            }
        }
    }
}
