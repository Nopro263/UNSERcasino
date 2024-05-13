namespace UNSERcasino.Game.Poker.Eval
{
    internal class PairResult : Result, IEquatable<PairResult>
    {
        private Card[] _cards;
        private PairResult(Card c1, Card c2)
        {
            _cards = new Card[] { c1, c2 };
        }
        public override Card[] GetCards()
        {
            return _cards;
        }

        public static List<PairResult> GetPairs(Card[] cards)
        {
            List<PairResult> pairs = new List<PairResult>();

            foreach(Card c in cards)
            {
                foreach(Card d in cards)
                {
                    if (c != d && c.CardValue == d.CardValue)
                    {
                        bool hasSeen = false;
                        foreach (PairResult pr in pairs)
                        {
                            if (pr.GetCards().Contains(c) && pr.GetCards().Contains(d))
                            {
                                hasSeen = true;
                                break;
                            }
                        }


                        if (!hasSeen)
                        {
                            pairs.Add(new PairResult(c, d));
                        }
                    }
                }
            }

            return pairs;
        }

        public bool Equals(PairResult? other)
        {
            throw new NotImplementedException();
        }
    }
}
