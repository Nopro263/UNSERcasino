namespace UNSERcasino.Game.Poker.Eval
{
    internal abstract class Result : IEquatable<Result>
    {
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
    }
}
