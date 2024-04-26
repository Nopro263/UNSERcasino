namespace UNSERcasino
{
    internal class CasinoManager
    {
        public double PlayerBalance { get; private set; }

        private static CasinoManager? _instance;
        public static CasinoManager Instance { get { if (_instance == null) { _instance = new CasinoManager(); } return _instance; } }

        private CasinoManager() {
            PlayerBalance = 0;
        }

        public void remove(double coins)
        {
            PlayerBalance -= coins;
        }

        public void add(double coins) { 
            PlayerBalance += coins;
        }
    }
}
