namespace UNSERcasino.Game.Poker
{
    internal class PokerPlayer
    {
        public string Name { get; private set; }
        public int Bet { get; private set; }
        public Card[] Hand { get; private set; }

        public PokerPlayer(string name, int bet, Card[] hand)
        {
            Name = name;
            Bet = bet;
            Hand = hand;
        }
    }
}
