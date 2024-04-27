using UNSERcasino.Game.Poker;

namespace UNSERcasino.UI.Menu
{
    internal class PokerMenu : Menu, IUpdateable
    {
        private Poker _poker;
        public PokerMenu() : base() {
            _poker = new Poker();

            scene.addView(new CardView(_poker.Hand[0]), Flow.CENTER, Flow.END, -15/2-1, 0);
            scene.addView(new CardView(_poker.Hand[1]), Flow.CENTER, Flow.END, 15/2+1, 0);

            scene.addView(new CardView(_poker.DealerHand[0]), Flow.CENTER, Flow.START, -32, 0);
            scene.addView(new CardView(_poker.DealerHand[1]), Flow.CENTER, Flow.START, -16, 0);
            scene.addView(new CardView(_poker.DealerHand[2]), Flow.CENTER, Flow.START, 0, 0);
            scene.addView(new CardView(_poker.DealerHand[3]), Flow.CENTER, Flow.START, 16, 0);
            scene.addView(new CardView(_poker.DealerHand[4]), Flow.CENTER, Flow.START, 32, 0);
        }

        public void Update()
        {
            
        }
    }
}
