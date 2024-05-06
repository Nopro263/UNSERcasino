namespace UNSERcasino.Game.Poker
{
    internal class RandomBotPokerPlayer : PokerPlayer
    {
        private Random random;
        public RandomBotPokerPlayer(Poker game, string name, int bet, Card[] hand) : base(game, name, bet, hand)
        {
            random = new Random();
        }

        public override void OnAction()
        {
            base.OnAction();

            int r = random.Next(0, 3);
            switch(r)
            {
                case 0:
                    {
                        Fold();
                        break;
                    }
                case 1:
                    {
                        Raise(2);
                        break;
                    }
                case 2:
                    {
                        Check();
                        break;
                    }
            }
        }
    }
}
