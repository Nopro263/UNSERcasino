using UNSERcasino.UI;

namespace UNSERcasino.Game.Poker.Eval
{
    internal class Evaluator
    {
        public static void Eval(Card[] dealer, Card[] hand)
        {
            foreach(Result res in InternalEval(dealer, hand))
            {
                foreach(Card card in res.GetCards()) 
                {
                    //Console.Write(card.CardValue);
                }

                //Console.WriteLine();
            }
        }

        private static List<Result> InternalEval(Card[] dealer, Card[] hand)
        {
            Card[] cards = new Card[dealer.Length + hand.Length];
            dealer.CopyTo(cards, 0);
            hand.CopyTo(cards, dealer.Length);


            List<Result> result = new List<Result>();

            List<PairResult> pairs = PairResult.GetPairs(cards);

            result.AddRange(pairs);
            result.AddRange(TripletResult.GetTriplets(cards));
            result.AddRange(TwoPairResult.GetTwoPairs(pairs));
            result.AddRange(QuadrupletResult.GetQuadruplets(cards));
            result.AddRange(StraightResult.GetStraights(cards));
            result.AddRange(FlushResult.GetFlushes(cards)); 

            return result;
        }

        public static bool containsType(Card[] cards, CardType type)
        {
            foreach(Card card in cards)
            {
                if(card.CardType == type) return true;
            }

            return false;
        }

        public static Card[] allOfType(Card[] cards, CardType type)
        {

            List<Card> cards1 = new List<Card>();
            foreach(Card card in cards)
            {
                if(card.CardType == type)
                {
                    cards1.Add(card);
                }
            }

            return cards1.ToArray();
        }

        public static bool containsValue(Card[] cards, CardValue value)
        {
            foreach (Card card in cards)
            {
                if (card.CardValue == value) return true;
            }

            return false;
        }

        public static bool containsCard(Card[] cards, Card c)
        {
            foreach (Card card in cards)
            {
                if (card.CardType == c.CardType && card.CardValue == c.CardValue) return true;
            }

            return false;
        }
    }
}
