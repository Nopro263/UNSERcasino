namespace UNSERcasino.Game
{
    internal class Dice
    {
        public int Bet { get; set; }
        public int Value { get; set; }
        public bool Over { get; set; }

        public static double ScaleInput(double input)
        {
            double inputMin = 12;
            double inputMax = 72;

            double outputMin = 1;
            double outputMax = 100;

            double output = ((input - inputMin) / (inputMax - inputMin)) * (outputMax - outputMin) + outputMin;

            return output;
        }

        private double calculateMultiplier(int x)
        {
            double xnew = ScaleInput(x);
            double houseAdvantage = 0.02; // 2%
            double winchance = 100 - x;
            double multiplier = 0;

            //Multiplier Function = Roll Over Num / 2 + 1%
            if (Over)
            {
                multiplier = (xnew / 2 + (xnew / 100));
            }
            else if (!Over)
            {
                multiplier = ((100 - xnew) / 2 + ((100 - xnew) / 100));
            }
            return multiplier;
        }


        public double Play(int randomValue)
        {
            if (Over)
            {
                if (randomValue > Value) // Win
                {
                    return Bet * calculateMultiplier(Value);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                if (randomValue < Value) // Win
                {
                    return Bet * calculateMultiplier(Value);
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
