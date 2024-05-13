namespace UNSERcasino.Game.Poker.Eval
{
    internal class TripletResult : Result
    {
        private Card[] _cards;
        private TripletResult(Card c1, Card c2, Card c3)
        {
            _cards = new Card[] { c1, c2, c3 };
        }
        public override Card[] GetCards()
        {
            return _cards;
        }

        public static List<TripletResult> GetTriplets(Card[] cards)
        {
            List<TripletResult> pairs = new List<TripletResult>();

            foreach (Card c in cards)
            {
                foreach (Card d in cards)
                {
                    foreach (Card e in cards)
                    {
                        if (c != d && d != e && c != e && c.CardValue == d.CardValue && d.CardValue == e.CardValue)
                        {
                            bool hasSeen = false;
                            foreach (TripletResult tr in pairs)
                            {
                                if (tr.GetCards().Contains(c) && tr.GetCards().Contains(d) && tr.GetCards().Contains(e))
                                {
                                    hasSeen = true;
                                    break;
                                }
                            }


                            if (!hasSeen)
                            {
                                pairs.Add(new TripletResult(c, d, e));
                            }
                        }
                    }
                }
            }

            return pairs;
        }
    }
}
