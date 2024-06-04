namespace UNSERcasino.Game.Poker.Eval
{
    internal abstract class Result : IEquatable<Result>, IComparable<Result>
    {
        public int CompareTo(Result? other)
        {
            int r1 = this.GetFinalRanking();
            int r2;
            if(other == null)
            {
                r2 = 0;
            }
            else
            {
                 r2 = other.GetFinalRanking();
            }

            return r2 - r1;
        }

        public bool Equals(Result? other)
        {
            if(other == null || this.GetType() != other.GetType()) return false;

            Card[] cards = other.GetCards();

            foreach(Card c in GetCards())
            {
                if(!cards.Contains(c)) return false;
            }

            return true;
        }

        public bool EqualsIgnoreType(Result? other)
        {
            if (other == null) return false;

            Card[] cards = other.GetCards();

            foreach (Card c in GetCards())
            {
                if (!cards.Contains(c)) return false;
            }

            return true;
        }

        public abstract Card[] GetCards();
        protected abstract int GetRanking();

        public int GetFinalRanking()
        {
            int rank = (10-GetRanking())*100;
            foreach(Card c in GetCards())
            {
                rank += c.CardValue.Rating;
            }

            return rank;
        }
    }
}
