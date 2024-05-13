namespace UNSERcasino.Game.Poker
{
    internal class PokerPlayer
    {
        public string Name { get; private set; }
        public int Bet { get; set; }
        public bool Folded { get; private set; }
        public Card[] Hand { get; private set; }

        protected Poker _game;

        public PokerPlayer(Poker game, string name, int bet, Card[] hand)
        {
            Name = name;
            Bet = bet;
            Hand = hand;
            _game = game;
        }

        public void Fold() 
        {
            if(Folded) { return; }
            try
            {
                _game.fold(this);
                _fold();
                Folded = true;
            } catch (NotYouException){}
        }

        public void Raise(int addedAmount)
        {
            if (Folded) { return; }
            try
            {
                addedAmount = _game.raise(this, addedAmount);
                _raise(addedAmount);
            }
            catch (NotYouException) { }
        }

        public void Check()
        {
            if (Folded) { return; }
            try
            {
                _game.check(this);
                _check();
            }
            catch (NotYouException) { }
        }

        protected virtual void _fold() { }
        protected virtual void _raise(int amount) { }
        protected virtual void _check() { }

        public virtual void OnAction()
        {

        }

        public virtual void OnWin(int win)
        {

        }

    }
}
