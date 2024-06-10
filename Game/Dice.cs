namespace UNSERcasino.Game
{
    internal class Dice
    {
        private const double HouseAdvantage = 0.02; // 2%
        private const double InputMin = 12;
        private const double InputMax = 72;
        private const double OutputMin = 2;
        private const double OutputMax = 98;

        public int Bet { get; set; }
        public int Value { get; set; }
        public bool Over { get; set; }

        public static double ScaleInput(double input) // Scale the input to a value between 12 and 72
        {
            return ((input - InputMin) / (InputMax - InputMin)) * (OutputMax - OutputMin) + OutputMin;
        }

        private double CalculateMultiplier(int x) // Calculate the multiplier for the bet
        {
            double xnew = ScaleInput(x);
            double winchance;

            if (Over)
            {
                winchance = 100 - xnew;
            }
            else
            {
                winchance = xnew;
            }

            double multiplier = (100 / winchance) * (1 - HouseAdvantage);
            return multiplier;
        }

        public int Play(int randomValue) // Game logic
        {
            int result = 0;

            if (Over)
            {
                if (randomValue > Value) // Win
                {
                    result =(int)(Bet * CalculateMultiplier(Value));
                }
            }
            else
            {
                if (randomValue < Value) // Win
                {
                    result = (int)(Bet * CalculateMultiplier(Value));
                }
            }

            return result;
        }
    }
}
