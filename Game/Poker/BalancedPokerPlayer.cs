namespace UNSERcasino.Game.Poker
{
    internal class BalancedPokerPlayer : PokerPlayer
    {
        public BalancedPokerPlayer(Poker game, string name, int bet, Card[] hand) : base(game, name, bet, hand)
        {
        }

        protected override void _raise(int addedAmount)
        {
            CasinoManager.Instance.bet(addedAmount);
        }

        protected override void _check()
        {
            CasinoManager.Instance.bet(_game.CurrentBet - Bet);
        }
    }
}
