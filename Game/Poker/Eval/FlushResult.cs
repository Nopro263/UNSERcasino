using UNSERcasino.UI;

namespace UNSERcasino.Game.Poker.Eval
{
    internal class FlushResult : Result
    {
        private Card[] _cards;
        private FlushResult(Card[] c)
        {
            _cards = c;
        }
        public override Card[] GetCards()
        {
            return _cards;
        }

        private static List<FlushResult> InternalGetFlushes(Card[] _cards, CardType cardType)
        {
            List<FlushResult> pairs = new List<FlushResult>();

            Card[] cards = Evaluator.allOfType(_cards, cardType);

            for(int i = 0; i < cards.Length - 5; i++)
            {
                pairs.Add(new FlushResult(new List<Card>(cards).GetRange(i, 5).ToArray()));
            }

            return pairs;
        }

        public static List<FlushResult> GetFlushes(Card[] _cards)
        {
            List<FlushResult> pairs = new List<FlushResult>();

            foreach(CardType t in Enum.GetValues<CardType>())
            {
                pairs.AddRange(InternalGetFlushes(_cards, t));
            }

            return pairs;
        }
    }
}
