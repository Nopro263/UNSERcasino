using System.Security.Cryptography;

namespace UNSERcasino.Game.Poker
{
    internal class Poker
    {
        public Card[] Hand { get; private set; }
        public Card[] DealerHand { get; private set; }

        private Stack<Card> _cards;

        public Poker()
        {
            Card[] c = Card.GetCards();
            shuffle(c);

            _cards = new Stack<Card>(c);

            Hand = new Card[] {
                _cards.Pop(),
                _cards.Pop()
            };

            DealerHand = new Card[] {
                _cards.Pop(),
                _cards.Pop(),
                _cards.Pop(),
                _cards.Pop(),
                _cards.Pop()
            };
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
