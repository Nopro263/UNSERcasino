namespace UNSERcasino.Game.Poker.Eval
{
    internal class FullHouseResult : Result
    {
        private Card[] _cards;
        public FullHouseResult(PairResult p, TripletResult t)
        {
            _cards = new Card[5];
            p.GetCards().CopyTo(_cards, 0);
            t.GetCards().CopyTo(_cards, 2);
        }
        public override Card[] GetCards()
        {
            return _cards;
        }

        protected override int GetRanking()
        {
            return 4;
        }

        public override string ToString()
        {
            return "full house";
        }
    }
}
