namespace UNSERcasino.Game
{
    internal class Dice
    {
        private bool _wonOrNot = false;
        private double _betAmount;
        private int _min;
        private int _max;

        public Dice(double betAmount, int min, int max)
        {
            _betAmount = betAmount;
            _min = min;
            _max = max;
        }

        private double SelectRoll()
        {
            Random random = new Random();
            double roll = random.NextDouble() * _max;
            return roll;
        }

        private bool ValidateRoll(double input, bool _overOrUnder) // Over -> True | Under -> False
        {
            double roll = SelectRoll();


            if (_overOrUnder == true)
            {
                if (roll > input)
                {
                    return true;
                }
                else { return false; }
            }
            else if (_overOrUnder == false)
            {
                if (roll < input)
                {
                    return true;
                }
                else { return false; }
            }
            else { return false; } // If Number matches Roll, will return false
        }

        public double Play(double input, double _betAmount, bool _overOrUnder)
        {
            _wonOrNot = ValidateRoll(input, _overOrUnder);
            if (_wonOrNot == true)
            {
                return _betAmount * 2;
            }
            else { return 0; }
        }

        public double CalcMultiplier(double input, bool _overOrUnder)
        {
            double house_edge = 0.02;
            if (_overOrUnder == true)
            {
                return 1 - ((1 - house_edge) * (_max - input) / _max) + 1;
            }
            else
            {
                return 1 - ((1 - house_edge) * input / _max);
            }
        }
    }
}
