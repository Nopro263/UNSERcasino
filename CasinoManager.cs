namespace UNSERcasino
{
    internal class CasinoManager : IPurse
    {
        public int Balance { get; private set; } // Self-explanatory
        public int TwoBefore { get; private set; } // The last two transactions (for PlayerBalanceView)
        public int Previous { get; private set; } // ^

        public string Name { get; private set; }

        private static CasinoManager? _instance; // Only allow a single instance (a.k.a. Singleton)
        public static CasinoManager Instance { get { if (_instance == null) { _instance = new CasinoManager(); } return _instance; } }

        private CasinoManager()
        {
            Name = "TestUser";
            Balance = 0;
        }

        private void change(int coins)
        {
            TwoBefore = Previous; // Update
            Previous = coins;

            Balance += coins;
        }

        public void Add(int amount)
        {
            change(amount);
        }

        public void Remove(int amount)
        {
            change(-amount);
        }

        public bool CanRemove(int amount)
        {
            return Balance >= amount;
        }

        public bool CanAdd(int amount)
        {
            return true;
        }
    }
}
