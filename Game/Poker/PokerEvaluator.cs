using UNSERcasino.UI;

namespace UNSERcasino.Game.Poker
{
    internal class PokerEvaluator
    {
        private static Card? containsValue(IEnumerable<Card> cards, CardValue value)
        {
            foreach (Card card in cards)
            {
                if (card.CardValue.Equals(value)) return card;
            }
            return null;
        }

        private static List<Card>? isFlush(Card[] cards) {
            Dictionary<CardType, List<Card>> counters = new Dictionary<CardType, List<Card>>
            {
                { CardType.Pik, new List<Card>() },
                { CardType.Kreuz, new List<Card>() },
                { CardType.Karo, new List<Card>() },
                { CardType.Herz, new List<Card>() }
            };

            foreach (Card card in cards)
            {
                counters[card.CardType].Add(card);
            }

            foreach (List<Card> c in counters.Values)
            {
                if(c.Count >= 5)
                {
                    return c;
                }
            }

            return null;
        }

        private static bool isStraight(List<Card>? flush)
        {
            if(flush == null || flush.Count < 5)
            {
                return false;
            }

            for(int i = 0; i <= flush.Count - 5; i++)
            {
                if(flush[0+i].CardValue.Rating + 1 == flush[1 + i].CardValue.Rating &&
                flush[1 + i].CardValue.Rating + 1 == flush[2 + i].CardValue.Rating &&
                flush[2 + i].CardValue.Rating + 1 == flush[3 + i].CardValue.Rating &&
                flush[3 + i].CardValue.Rating + 1 == flush[4 + i].CardValue.Rating)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool isStraight(Card[]? flush)
        {
            if (flush == null || flush.Length < 5)
            {
                return false;
            }

            for (int i = 0; i <= flush.Length - 5; i++)
            {
                if (flush[0 + i].CardValue.Rating + 1 == flush[1 + i].CardValue.Rating &&
                flush[1 + i].CardValue.Rating + 1 == flush[2 + i].CardValue.Rating &&
                flush[2 + i].CardValue.Rating + 1 == flush[3 + i].CardValue.Rating &&
                flush[3 + i].CardValue.Rating + 1 == flush[4 + i].CardValue.Rating)
                {
                    return true;
                }
            }
            return false;
        }

        public static PokerEvaluationResult? evaluateHand(Card[] dealer, Card[] player)
        {
            Card[] temp = new Card[dealer.Length + player.Length];

            dealer.CopyTo(temp, 0);
            player.CopyTo(temp, dealer.Length);

            Array.Sort(temp);

            // Royal Flush
            List<Card>? flush = isFlush(temp);

            if (flush != null &&
                containsValue(flush, CardValue.ASS) != null &&
                containsValue(flush, CardValue.KOENIG) != null &&
                containsValue(flush, CardValue.DAME) != null &&
                containsValue(flush, CardValue.BUB) != null &&
                containsValue(flush, CardValue.ZEHN) != null)
            {
                return PokerEvaluationResult.ROYAL_FLUSH;
            }

            // Straight Flush
            if (flush != null && isStraight(flush))
            {
                return PokerEvaluationResult.STRAIGHT_FLUSH;
            }

            // Flush
            if (flush != null && flush.Count >= 5)
            {
                foreach(Card card in flush)
                {
                    Console.WriteLine(card.CardValue);
                }
                return PokerEvaluationResult.FLUSH;
            }

            // Straight
            if(isStraight(temp))
            {
                return PokerEvaluationResult.STRAIGHT;
            }

            return null;
        }
    }
}
