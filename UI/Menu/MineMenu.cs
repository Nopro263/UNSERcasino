using UNSERcasino.Game;

namespace UNSERcasino.UI.Menu
{
    internal class MineMenu : Menu
    {
        private Mines _mines;
        private ButtonView _buttonview;

        public MineMenu() : base()
        {
            const int col = 5;
            const int row = 5;
            int x = (Console.BufferWidth / 2) - (col * 10 / 2);
            int y = (Console.BufferHeight / 2) - (row * 6 / 2);

            _mines = new Mines();
            _buttonview = new ButtonView(new Text("Reveal"), false);


        }


    }
}
