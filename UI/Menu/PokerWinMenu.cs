using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UNSERcasino.Game.Poker;
using UNSERcasino.Game.Poker.Eval;

namespace UNSERcasino.UI.Menu
{
    internal class PokerWinMenu : Menu
    {
        private int _index = 0;
        public PokerWinMenu(Card[] dealer)
        {
            _scene.addView(new CardView(dealer[0]), Flow.END, Flow.START, -32, 0);
            _scene.addView(new CardView(dealer[1]), Flow.END, Flow.START, -16, 0);
            _scene.addView(new CardView(dealer[2]), Flow.END, Flow.START, 0, 14);
            _scene.addView(new CardView(dealer[3]), Flow.END, Flow.START, -16, 14);
            _scene.addView(new CardView(dealer[4]), Flow.END, Flow.START, -32, 14);
        }

        public void addPlayer(PokerPlayer player, List<Result> results)
        {
            _scene.addView(new CardView(player.Hand[0]), Flow.START, Flow.START, _index * 16, 0);
            _scene.addView(new CardView(player.Hand[1]), Flow.START, Flow.START, _index * 16, 14);

            int i = 0;
            foreach(Result result in results)
            {
                _scene.addView(new TextView(new Text(result.ToString()), false, false), Flow.START, Flow.START, _index * 16, 28 + i);
                i++;
            }

            _scene.addView(new TextView(new Text(Evaluator.GetScore(results).ToString(), ConsoleColor.Green, Canvas.BACKGROUND), false, false), Flow.START, Flow.START, _index * 16, 28 + i);

            _index++;
        }
    }
}
