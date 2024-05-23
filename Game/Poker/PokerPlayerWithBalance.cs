namespace UNSERcasino.Game.Poker
{
    internal class PokerPlayerWithBalance : PokerPlayer
    {
        private IPurse _purse;
        public PokerPlayerWithBalance(Poker poker, string name, Card[] hand, IPurse purse) : base(poker, name, hand)
        {
            _purse = purse;
        }

        public override bool CanCheckAmount(int amount)
        {
            if(!base.CanCheckAmount(amount)) return false;

            return _purse.CanRemove(amount);
        }

        public override bool CanRaiseAmount(int amount)
        {
            if (!base.CanRaiseAmount(amount)) return false;

            return _purse.CanRemove(amount);
        }
    }
}
