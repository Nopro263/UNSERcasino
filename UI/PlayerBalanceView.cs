namespace UNSERcasino.UI
{
    internal class PlayerBalanceView : TableView, IUpdateable
    {
        private Text _balance;
        private Text _2before;
        private Text _previous;
        public PlayerBalanceView() : base(new Text[][] { new Text[] { new Text("123 000"), new Text("+ 12"), new Text("- 52") } })
        {
            // hack, beacuse we can't access non-static vars in the parent-constructor
            _balance = _data[0][0];
            _2before = _data[0][1];
            _previous = _data[0][2];
        }

        public void Update()
        {
            CasinoManager casino = CasinoManager.Instance;

            _balance.setContent(casino.Balance.ToString("F2"));
            _previous.setContent(casino.Previous.ToString("F2"));
            _2before.setContent(casino.TwoBefore.ToString("F2"));
        }
    }
}
