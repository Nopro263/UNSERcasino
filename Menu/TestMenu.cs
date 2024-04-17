namespace UNSERcasino.Menu
{
    internal class TestMenu : BaseMenu
    {
        private int _index = 0;
        public override string[] update(TimeSpan s, string? i)
        {
            string[] d = new string[2];
            d[0] = "d";
            d[1] = Convert.ToString(s.Ticks);
            _index++;
            return d;
        }
    }
}
