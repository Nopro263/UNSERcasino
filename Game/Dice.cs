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

        public static double ScaleInput(double input)
        {
            return ((input - InputMin) / (InputMax - InputMin)) * (OutputMax - OutputMin) + OutputMin;
        }

        private double calculateMultiplier(int x)
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

        public double Play(int randomValue)
        {
            double result = 0;

            if (Over)
            {
                if (randomValue > Value) // Win
                {
                    result = Bet * calculateMultiplier(Value);
                }
            }
            else
            {
                if (randomValue < Value) // Win
                {
                    result = Bet * calculateMultiplier(Value);
                }
            }

            return result;
        }
    }
}
