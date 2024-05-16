namespace UNSERcasino.Game.Poker.Eval
{
    internal abstract class Result : IEquatable<Result>, IComparable<Result>
    {
        public int CompareTo(Result? other)
        {
            int r1 = this.GetRanking();
            int r2;
            if(other == null)
            {
                r2 = 0;
            }
            else
            {
                 r2 = other.GetRanking();
            }

            return r1 - r2;
        }

        public bool Equals(Result? other)
        {
            if(other == null) return false;

            Card[] cards = other.GetCards();

            foreach(Card c in GetCards())
            {
                if(!cards.Contains(c)) return false;
            }

            return true;
        }

        public abstract Card[] GetCards();
        protected abstract int GetRanking();


    }
}
