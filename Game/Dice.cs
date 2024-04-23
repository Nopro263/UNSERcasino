namespace UNSERcasino.Game
{
    internal class Dice
    {
        public int Bet { get; set; }

        private int calculateMultiplier()
        {
            int x = Bet;
            return (int)(0.01 * Math.Pow(x - ((72 - 12) / 2 + 12), 2) + 1);
        }
    }
}
