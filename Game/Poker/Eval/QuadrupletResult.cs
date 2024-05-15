namespace UNSERcasino.Game.Poker.Eval
{
    internal class QuadrupletResult : Result
    {
        private Card[] _cards;
        private QuadrupletResult(Card c1, Card c2, Card c3, Card c4)
        {
            _cards = new Card[] { c1, c2, c3, c4 };
        }
        public override Card[] GetCards()
        {
            return _cards;
        }

        public static List<QuadrupletResult> GetQuadruplets(Card[] cards)
        {
            List<QuadrupletResult> pairs = new List<QuadrupletResult>();

            foreach (Card c in cards)
            {
                foreach (Card d in cards)
                {
                    foreach (Card e in cards)
                    {
                        foreach (Card f in cards)
                        {
                            if (c != d && d != e && c != e && c != f && d != f && e != f && c.CardValue == d.CardValue && d.CardValue == e.CardValue && e.CardValue == f.CardValue)
                            {
                                bool hasSeen = false;
                                foreach (QuadrupletResult tr in pairs)
                                {
                                    if (tr.GetCards().Contains(c) && tr.GetCards().Contains(d) && tr.GetCards().Contains(e) && tr.GetCards().Contains(f))
                                    {
                                        hasSeen = true;
                                        break;
                                    }
                                }


                                if (!hasSeen)
                                {
                                    pairs.Add(new QuadrupletResult(c, d, e, f));
                                }
                            }
                        }
                    }
                }
            }

            return pairs;
        }
    }
}
