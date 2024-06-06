namespace UNSERcasino.Game.Poker
{
    internal class PokerPlayerWithBalance : PokerPlayer
    {
        private IPurse _purse; // Apple???
        public PokerPlayerWithBalance(Poker poker, string name, Card[] hand, IPurse purse) : base(poker, name, hand)
        {
            _purse = purse;
        }

        public override bool CanCheckAmount(int amount)
        {
            if (!base.CanCheckAmount(amount)) return false;

            return _purse.CanRemove(amount);
        }

        public override bool CanRaiseAmount(int amount)
        {
            if (!base.CanRaiseAmount(amount)) return false;

            return _purse.CanRemove(amount);
        }

        public override void afterCheck(int checkedAmount)
        {
            base.afterCheck(checkedAmount);

            _purse.Remove(checkedAmount);
        }

        public override void afterRaise(int checkedAmount, int raisedAmount)
        {
            base.afterRaise(checkedAmount, raisedAmount);

            _purse.Remove(checkedAmount + raisedAmount);
        }

        public override void Win(int amount)
        {
            base.Win(amount);

            _purse.Add(amount);
        }
    }
}
