using UNSERcasino.Game.Poker;

namespace UNSERcasino.UI.Menu
{
    internal class PokerMenu : Menu, IUpdateable
    {
        private Poker _poker;
        private Text _potText;
        private Text[][] _opponents;
        public PokerMenu() : base() {
            _poker = new Poker();

            _potText = new Text("Pot: ");

            _opponents = new Text[][]
            {
                new Text[]
                {
                    new Text("Player"),
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
                }
            };


            scene.addView(new CardView(_poker.Me.Hand[0]), Flow.CENTER, Flow.END, -15/2-1, 0);
            scene.addView(new CardView(_poker.Me.Hand[1]), Flow.CENTER, Flow.END, 15/2+1, 0);

            scene.addView(new CardView(_poker.DealerHand[0]), Flow.CENTER, Flow.START, -32, 0);
            scene.addView(new CardView(_poker.DealerHand[1]), Flow.CENTER, Flow.START, -16, 0);
            scene.addView(new CardView(_poker.DealerHand[2]), Flow.CENTER, Flow.START, 0, 0);
            scene.addView(new CardView(_poker.DealerHand[3]), Flow.CENTER, Flow.START, 16, 0);
            scene.addView(new CardView(_poker.DealerHand[4]), Flow.CENTER, Flow.START, 32, 0);

            scene.addView(new TextView(_potText, false, true), Flow.CENTER, Flow.CENTER);
            scene.addView(new TableView(_opponents), Flow.START, Flow.END);

            renderOpponents();
        }

        private void renderOpponents()
        {
            Text[] players = _opponents[0];
            Text[] bets = _opponents[1];

            int i = 1;
            foreach(PokerPlayer player in _poker.Players)
            {
                players[i].setContent(player.Name);
                bets[i].setContent(player.Bet.ToString());
                i++;
            }
        }

        public void Update()
        {
            renderOpponents();
            _potText.setContent($"Pot: {_poker.Pot}");
        }
    }
}
