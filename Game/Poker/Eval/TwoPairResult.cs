namespace UNSERcasino.Game.Poker.Eval
{
    internal class TwoPairResult : Result
    {
        private PairResult[] _pairs;
        private TwoPairResult(PairResult p1, PairResult p2)
        {
            _pairs = new PairResult[] { p1, p2 };
        }
        public override Card[] GetCards()
        {
            Card[] cards = new Card[4];
            _pairs[0].GetCards().CopyTo(cards, 0);
            _pairs[1].GetCards().CopyTo(cards, 2);
            return cards;
        }

        public static List<TwoPairResult> GetTwoPairs(List<PairResult> pairs)
        {
            List<TwoPairResult> tpr = new List<TwoPairResult>();
            foreach (PairResult pair in pairs)
            {
                foreach (PairResult pair2 in pairs)
                {
                    if (!pair.Equals(pair2))
                    {
                        tpr.Add(new TwoPairResult(pair, pair2));
                    }
                }
            }

            return tpr;
        }
    }
}
