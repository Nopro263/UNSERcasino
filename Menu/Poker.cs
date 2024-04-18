
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
            Console.Clear();
            if(i != null)
            {
                _game.FirstThree();
            }
            string[] result = new string[0];
            string[] community_cards = printCards(_game.getCommunityCards());

            string[] my_cards = printCards(_game.getPlayer().Hand);

            string[] buffer = new string[] {"", "", "", "", ""};

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
