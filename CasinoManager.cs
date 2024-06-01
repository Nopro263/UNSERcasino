namespace UNSERcasino
{
    internal class CasinoManager : IPurse
    {
        private readonly string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "UNSERcasino.save");
        public int Balance { get; private set; } // Self-explanatory
        public int TwoBefore { get; private set; } // The last two transactions (for PlayerBalanceView)
        public int Previous { get; private set; } // ^

        public string Name { get; private set; }

        private static CasinoManager? _instance; // Only allow a single instance (a.k.a. Singleton)
        public static CasinoManager Instance { get { if (_instance == null) { _instance = new CasinoManager(); } return _instance; } }

        public void Save()
        {
            FileStream? fs = null;
            StreamWriter? sw = null;
            try
            {
                fs = new FileStream(_path, FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs);

                sw.WriteLine(this.Name);
                sw.WriteLine(this.Balance);

            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }

                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        private void load()
        {
            FileStream? fs = null;
            StreamReader? sr = null;
            try
            {
                fs = new FileStream(_path, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs);

                this.Name = sr.ReadLine();
                this.Balance = Convert.ToInt32(sr.ReadLine());

            } 
            catch (FileNotFoundException) { }
            finally
            {
                if(sr != null)
                {
                    sr.Close();
                }

                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        private CasinoManager()
        {
            Name = Environment.UserName;
            Balance = 100;

            load();

            Save();
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
