namespace UNSERcasino.Game
{
    internal class Dice
    {
        public int Bet { get; set; }
        public int Value { get; set; }
        public bool Over {  get; set; }

        private double calculateMultiplier(int x)
        {
            return 0.01 * Math.Pow(x - ((72 - 12) / 2 + 12), 2) + 1;
        }

        public double Play(int randomValue)
        {
            return calculateMultiplier(randomValue) * Bet;
        }
    }
}
