namespace UNSERcasino.Game
{
    internal class Dice
    {
        private double SelectRoll()
        {
            const int maxRoll = 100;

            Random random = new Random();
            double roll = random.NextDouble() * maxRoll;
            return roll;
        }

        public bool ValidateRoll(double input, bool OverOrUnder) // Over -> True | Under -> False
        {
            double roll = SelectRoll();

            if (OverOrUnder == true)
            {
                return roll > input; // If correct, will return true
            }
            else if (OverOrUnder == false)
            {
                return roll < input; // If correct, will return true
            }
            else { return false; } // If Number matches Roll, will return false
        }
    }
}
