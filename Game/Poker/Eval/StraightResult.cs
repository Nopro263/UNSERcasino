using UNSERcasino.UI;

namespace UNSERcasino.Game.Poker.Eval
{
    internal class StraightResult : Result
    {
        private Card[] _cards;
        private StraightResult(Card[] c)
        {
            _cards = c;
        }
        public override Card[] GetCards()
        {
            return _cards;
        }

        private static Card[] purgeDoubleCards(Card[] _cards)
        {
            Card[] cards = new Card[_cards.Length];
            int i = 0;
            foreach (Card card in _cards)
            {
                bool seen = false;
                foreach(Card c in cards)
                {
                    if(c != null && c.CardValue == card.CardValue)
                    {
                        seen = true;
                    }
                }
            
                if(!seen)
                {
                    cards[i] = card;
                    i++;
                }
                
            }

            Array.Resize<Card>(ref cards, i);

            return cards;
        }

        public static List<StraightResult> GetStraights(Card[] _cards)
        {
            List<StraightResult> pairs = new List<StraightResult>();

            Card[] cards = purgeDoubleCards(_cards);
            Card[] values;

            Array.Sort(cards);

            if(Evaluator.containsValue(cards, UI.CardValue.ASS))
            {
                values = new Card[cards.Length + 1];
                values[0] = new Card(CardValue._SINGLE_ASS_DO_NOT_USE_OUTSIDE_OF_EVALUATION, cards.Last().CardType, false);
                for(int i = 1; i < values.Length; i++)
                {
                    values[i] = cards[i-1];
                }
            } else
            {
                values = new Card[cards.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = cards[i];
                }
            }

            for(int i = 0; i <= values.Length - 5; i++)
            {
                int lastRating = 0;
                int k = 1;
                for(int j = i; j < i+5; j++)
                {
                    if(lastRating != 0 && lastRating+1 != values[j].CardValue.Rating)
                    {
                        break;
                    }
                    lastRating = values[j].CardValue.Rating;
                    k++;
                }

                if(k > 5)
                {
                    pairs.Add(new StraightResult(new List<Card>(values).GetRange(i, 5).ToArray()));
                }
            }

            return pairs;
        }

        protected override int GetRanking()
        {
            return 6;
        }
    }
}
