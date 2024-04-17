
using NoahCardOutput;
using UNSERcasino.Game;

namespace UNSERcasino.Menu
{
    internal class Poker : BaseMenu
    {
        private PokerGame _game;

        public override void enter(bool isReenter)
        {
            base.enter(isReenter);

            if(!isReenter)
            {
                _game = new PokerGame();
                _game.addPlayer("Noah", 1000);
                _game.addPlayer("Jonas", 10);
            }
        }
        public override string[] update(TimeSpan s, string? i)
        {
            if(i != null)
            {
                _game.FirstThree();
            }
            string[] result = new string[0];
            string[] community_cards = printCards(_game.getCommunityCards());

            string[] my_cards = printCards(_game.getPlayer().Hand);

            try
            {
                my_cards[0] = unCenter(my_cards[0], " Players | Bet     ", "");
                my_cards[1] = unCenter(my_cards[1], _game.getOpponents()[0].Name.PadRight(7) + " | " + _game.getOpponents()[0].Bet.ToString().PadRight(4) + "    ", "");
                my_cards[2] = unCenter(my_cards[2], _game.getOpponents()[1].Name.PadRight(7) + " | " + _game.getOpponents()[1].Bet.ToString().PadRight(4) + "    ", "");
                my_cards[3] = unCenter(my_cards[3], _game.getOpponents()[2].Name.PadRight(7) + " | " + _game.getOpponents()[2].Bet.ToString().PadRight(4) + "    ", "");
                my_cards[4] = unCenter(my_cards[4], _game.getOpponents()[3].Name.PadRight(7) + " | " + _game.getOpponents()[3].Bet.ToString().PadRight(4) + "    ", "");
                my_cards[5] = unCenter(my_cards[5], _game.getOpponents()[4].Name.PadRight(7) + " | " + _game.getOpponents()[4].Bet.ToString().PadRight(4) + "    ", "");
            } catch (Exception e) {}
            my_cards[6] = unCenter(my_cards[6], "", _game.getPlayer().Tokens.ToString() + " Tokens");

            string[] buffer = new string[] {"", "", _game.getPot().ToString(), "", ""};

            result = result.Concat(community_cards).Concat(buffer).Concat(my_cards).ToArray();

            MenuManager.Instance.requestInput();
            return result;
        }




        private string[] printCards(PrintableCard[] cards)
        {
            string[] res = new string[13];

            for(int i = 0; i < 13; i++)
            {
                res[i] = "";
                for (int j = 0; j < cards.Length; j++)
                {
                    res[i] += cards[j].getStrings()[i];
                    res[i] += " ";
                }
                res[i] = res[i];
            }

            return res;
        }

        private string unCenter(string orig, string left, string right)
        {
            int len = right.Length + 1 - left.Length;
            int lenr = Math.Max(0, -len);
            int lenl = Math.Max(0, len);
            return left + new string(' ', lenl) + " " + orig + " " + right + new string(' ', lenr);
        }
    }
}
