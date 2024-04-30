using System.Security.Cryptography;

namespace UNSERcasino.Game.Poker
{
    internal class Poker
    {
        public Card[] DealerHand { get; private set; }

        public int Pot { get; private set; }

        private Stack<Card> _cards;
        public List<PokerPlayer> Players { get; private set; }

        public PokerPlayer Me { get; private set; }

        public Poker()
        {
            Players = new List<PokerPlayer>();

            Card[] c = Card.GetCards();
            shuffle(c);

            _cards = new Stack<Card>(c);

            Me = createPlayer("Noah");
            createPlayer("Emilio");

            DealerHand = new Card[] {
                _cards.Pop(),
                _cards.Pop(),
                _cards.Pop(),
                _cards.Pop(),
                _cards.Pop()
            };

            Pot = 100;
        }

        public PokerPlayer createPlayer(string name)
        {
            Card[] hand = new Card[] {
                _cards.Pop(),
                _cards.Pop()
            };

            PokerPlayer player = new PokerPlayer(name, name.Length, hand);
            Players.Add(player);
            return player;
        }

        private static void shuffle(Card[] cards)
        {
            for(int _ = 0; _ < 10000; _++)
            {
                int i = RandomNumberGenerator.GetInt32(0, cards.Length);
                int j = RandomNumberGenerator.GetInt32(0, cards.Length);

                Card temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }
        }
    }
}
