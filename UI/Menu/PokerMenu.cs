using UNSERcasino.Game.Poker;
using UNSERcasino.Game.Poker.Eval;

namespace UNSERcasino.UI.Menu
{
    internal class PokerMenu : Menu, IUpdateable
    {
        private Poker _poker;
        private Text _potText;
        private Text[][] _opponents;
        private CardView _card1;
        private CardView _card2;

        private Text btFold;
        private Text btCheck;

        private TextInputView tip;
        public PokerMenu() : base()
        {
            _poker = new Poker();

            _potText = new Text("Pot: ");

            _opponents = new Text[][]
            {
                new Text[]
                {
                    new Text("Player"),
                    new Text(""),
                    new Text(""),
                    new Text(""),
                    new Text("")
                },
                new Text[]
                {
                    new Text("Bet"),
                    new Text(""),
                    new Text(""),
                    new Text(""),
                    new Text("")
                }
            };

            _card1 = new CardView(_poker.Me.Hand[0]);
            _card2 = new CardView(_poker.Me.Hand[1]);


            scene.addView(_card1, Flow.CENTER, Flow.END, -15 / 2 - 1, 0);
            scene.addView(_card2, Flow.CENTER, Flow.END, 15 / 2 + 1, 0);

            scene.addView(new CardView(_poker.DealerHand[0]), Flow.CENTER, Flow.START, -32, 0);
            scene.addView(new CardView(_poker.DealerHand[1]), Flow.CENTER, Flow.START, -16, 0);
            scene.addView(new CardView(_poker.DealerHand[2]), Flow.CENTER, Flow.START, 0, 0);
            scene.addView(new CardView(_poker.DealerHand[3]), Flow.CENTER, Flow.START, 16, 0);
            scene.addView(new CardView(_poker.DealerHand[4]), Flow.CENTER, Flow.START, 32, 0);

            scene.addView(new TextView(_potText, false, true), Flow.CENTER, Flow.CENTER);
            scene.addView(new TableView(_opponents), Flow.START, Flow.END);

            btFold = new Text("Fold");
            btCheck = new Text("Check");

            tip = new TextInputView(false, 5, "Raise");

            scene.addView(new ButtonView(btCheck, false), Flow.END, Flow.END, 0, -2);
            scene.addView(tip, Flow.END, Flow.END, 0, -1);
            scene.addView(new ButtonView(btFold, false), Flow.END, Flow.END, 0, 0);

            renderOpponents();
        }

        private void renderOpponents()
        {
            Text[] players = _opponents[0];
            Text[] bets = _opponents[1];

            int i = 1;
            foreach (PokerPlayer player in _poker.Players)
            {
                players[i].setContent(player.Name);
                if (player.Folded)
                {
                    bets[i].setContent("-");
                }
                else
                {
                    bets[i].setContent(player.Bet.ToString());
                }

                if (!_poker.Ended && _poker.Current == player)
                {
                    bets[i].Bg = ConsoleColor.DarkYellow;
                    players[i].Bg = ConsoleColor.DarkYellow;
                }
                else
                {
                    bets[i].Bg = Canvas.BACKGROUND;
                    players[i].Bg = Canvas.BACKGROUND;
                }
                i++;
            }
        }

        public void Update()
        {
            renderOpponents();
            _potText.setContent($"Pot: {_poker.Pot}");

            if (!_poker.isCurrentMe)
            {
                btFold.Fg = ConsoleColor.DarkGray;
                btCheck.Fg = ConsoleColor.DarkGray;
                tip.Text.Fg = ConsoleColor.DarkGray;
            }
            else
            {
                btFold.Fg = Canvas.FOREGROUND;
                btCheck.Fg = Canvas.FOREGROUND;
                tip.Text.Fg = Canvas.FOREGROUND;
            }

            if (_poker.Ended)
            {
                MenuManager.close();
            }

            _card1.Card = _poker.Me.Hand[0];
            _card2.Card = _poker.Me.Hand[1];


        }

        public override void onClick(IClickable button)
        {
            base.onClick(button);

            ButtonView? bv = button as ButtonView;

            if (bv != null)
            {
                switch (bv.Text.getContent())
                {
                    case "Fold":
                        {
                            _poker.Me.Fold();
                            break;
                        }
                    case "Check":
                        {
                            _poker.Me.Check();
                            break;
                        }
                }
            }

            TextInputView? tiv = button as TextInputView;

            if(tiv != null)
            {
                _poker.Me.Raise(int.Parse(tiv.FullContent));
            }
        }
    }
}
