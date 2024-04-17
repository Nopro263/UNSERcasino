namespace UNSERcasino.Game
{
    internal class Dice
    {
        private bool _wonOrNot = false;
        private double _betAmount;

        public Dice(double betAmount)
        {
            _betAmount = betAmount;
        }

        private double SelectRoll()
        {
            const int maxRoll = 100;

            Random random = new Random();
            double roll = random.NextDouble() * maxRoll;
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
    }
}
