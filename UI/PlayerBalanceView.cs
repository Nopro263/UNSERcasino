namespace UNSERcasino.UI
{
    internal class PlayerBalanceView : TableView, IUpdateable
    {
        public PlayerBalanceView() : base(new Text[][] {new Text[] {new Text("123 000"), new Text("+ 12"), new Text("- 52")} })
        {
            
        }

        public void Update()
        {
            
        }
    }
}
