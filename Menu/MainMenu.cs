

namespace UNSERcasino.Menu
{
    internal class MainMenu : BaseMenu
    {
        public override string[] update(TimeSpan s, string? i)
        {
            preventBug();
            string[] result = new string[5];

            if(i != null)
            {
                int x = 0;
                if(int.TryParse(i, out x))
                {
                    switch(x)
                    {
                        case 1: { MenuManager.Instance.open(new TestMenu()); return result; }
                    }
                }
            }

            result[0] = "MAIN MENU";
            result[1] = "";
            result[2] = "1: Dice Game";
            result[3] = "2: Mines";
            result[4] = "3: exit";

            MenuManager.Instance.requestInput();

            return result;
        }
    }
}
