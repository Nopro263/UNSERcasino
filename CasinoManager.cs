namespace UNSERcasino
{
    internal class CasinoManager
    {
        public double PlayerBalance { get; private set; } // Self-explanatory
        public double TwoBefore { get; private set; } // The last two transactions (for PlayerBalanceView)
        public double Previous { get; private set; } // ^

        private static CasinoManager? _instance; // Only allow a single instance (a.k.a. Singleton)
        public static CasinoManager Instance { get { if (_instance == null) { _instance = new CasinoManager(); } return _instance; } }

        private CasinoManager()
        {
            PlayerBalance = 0;
        }

        /// <summary>
        /// Called when a player bets some coins.
        /// </summary>
        /// <param name="coins">the coins to bet</param>
        public void bet(double coins)
        {
            PlayerBalance -= coins;
        }

        /// <summary>
        /// Called when the player wins or looses some coins.
        /// </summary>
        /// <param name="coins">the coins to change</param>
        public void add(double coins)
        {
            change(coins);
        }

        private void change(double coins)
        {
            TwoBefore = Previous; // Update
            Previous = coins;

            PlayerBalance += coins;
        }
    }
}
