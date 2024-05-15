﻿namespace UNSERcasino.Game.Poker
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

        protected override void _check(int addedAmount)
        {
            CasinoManager.Instance.bet(addedAmount);
        }

        public override void OnWin(int win)
        {
            base.OnWin(win);

            CasinoManager.Instance.add(win);
        }
    }
}