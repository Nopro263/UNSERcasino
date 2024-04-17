using NoahCardOutput;

namespace UNSERcasino.Game
{
    internal class PokerPlayer
    {
        public PrintableCard[] Hand { get; private set; }
        public int Tokens { get; private set; }
        public int Bet { get; private set; }
        public string Name { get; private set; }

        public PokerPlayer(string name, PrintableCard[] hand, int tokens)
        {
            Name = name;
            Hand = hand;
            Tokens = tokens;
            Bet = 0;
        }
    }
}
