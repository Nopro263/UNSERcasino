using UNSERcasino.UI;

namespace UNSERcasino.Game.Poker.Eval
{
    internal class Evaluator
    {
        public static List<Result> Eval(Card[] dealer, Card[] hand)
        {
            List<Result> results = InternalEval(dealer, hand);

            return results;
        }

        private static List<Result> allWithCard(List<Result> results, Card card)
        {
            return results.FindAll((Result r) => {
                return r.GetCards().Contains(card);
            });
        }

        private static List<Result> removeAllDoubleUses(List<Result> results, Card[] cards)
        {
            results.Sort();

            foreach (Card card in cards) {
                List<Result> res = allWithCard(results, card);
                res.Sort();

                foreach(Result res2 in res)
                {
                    if(res2 == res.First())
                    {
                        continue;
                    }
                    results.Remove(res2);
                }
            }

            return results;
        }

        private static List<Result> InternalEval(Card[] dealer, Card[] hand)
        {
            Card[] cards = new Card[dealer.Length + hand.Length];
            dealer.CopyTo(cards, 0);
            hand.CopyTo(cards, dealer.Length);


            List<Result> result = new List<Result>();

            List<PairResult> pairs = PairResult.GetPairs(cards);

            result.AddRange(pairs);
            result.AddRange(HighCardResult.GetHighCards(hand));
            result.AddRange(TripletResult.GetTriplets(cards));
            result.AddRange(TwoPairResult.GetTwoPairs(pairs));
            result.AddRange(QuadrupletResult.GetQuadruplets(cards));
            result.AddRange(StraightResult.GetStraights(cards));
            result.AddRange(FlushResult.GetFlushes(cards));
            result.AddRange(StraightFlushResult.GetStraightFlush(result));
            result.AddRange(RoyalFlushResult.GetRoyalFlush(result));

            result = removeAllDoubleUses(result, cards);

            TripletResult? hasTriplet = null;
            PairResult? hasPair = null;

            foreach(Result res in result)
            {
                if(res is TripletResult)
                {
                    hasTriplet = (TripletResult?)res;
                }
                if(res is PairResult)
                {
                    hasPair = (PairResult?)res;
                }
            }

            if(hasTriplet != null && hasPair != null)
            {
                result.Add(new FullHouseResult(hasPair, hasTriplet)); //FIXME: if cards build sth. better than a pair and a triplet on their own, it doesnt work
                result.Remove(hasPair);
                result.Remove(hasTriplet);
            }

            result = removeAllDoubleUses(result, cards);

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
