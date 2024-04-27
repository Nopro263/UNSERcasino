namespace UNSERcasino.UI.Menu
{
    internal class CrashMenu : Menu, IUpdateable
    {
        private DiceView _view;
        private Card _card;
        public CrashMenu() : base()
        {
            _view = new DiceView(1);
            scene.addView(_view, 0, 0);
            scene.addView(new SliderView(10, 20, 10), 10, 10);
            scene.addView(new ButtonView(new Text("Bet"), false), 10, 11);

            scene.addView(new TextInputView(false, 10), 20, 0);

            _card = new Card(CardValue.ASS, CardType.Herz, false);

            scene.addView(new CardView(_card), Flow.CENTER, Flow.CENTER);
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
    }
}
