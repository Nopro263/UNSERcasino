namespace UNSERcasino.Game.Poker
{
    internal class PokerPlayer
    {
        public bool HasFolded { get; private set; }
        public int Bet { get; private set; }
        public string Name { get; private set; }

        public Card[] Hand { get; private set; }

        private Poker _poker;

        public PokerPlayer(Poker poker, string name, Card[] hand)
        {
            _poker = poker;
            Name = name;
            Hand = hand;
        }
        public virtual bool CanRaise()
        {
            int difference = _poker.CurrentBet - Bet;
            return !HasFolded && CanRaiseAmount(difference);
        }
        public virtual bool CanRaiseAmount(int amount)
        {
            return true;
        }

        public virtual bool CanFold()
        {
            return !HasFolded;
        }

        public virtual bool CanCheck()
        {
            int difference = _poker.CurrentBet - Bet;
            return !HasFolded && CanCheckAmount(difference);
        }

        public virtual bool CanCheckAmount(int amount)
        {
            return true;
        }




        public virtual void Fold()
        {
            if(CanFold())
            {
                HasFolded = true;
                _poker.Fold(this);
            }
        }

        public virtual void Raise(int amount)
        {
            if(CanRaiseAmount(amount))
            {
                int difference = _poker.CurrentBet - Bet;
                Bet += difference + amount;

                _poker.Raise(this);
            }
        }

        public virtual void Check()
        {
            int difference = _poker.CurrentBet - Bet;
            if (CanCheckAmount(difference))
            {
                Bet += difference;

                _poker.Check(this);
            }
        }

        public void ResetBet()
        {
            Bet = 0;
        }
    }
}
