namespace UNSERcasino.UI.Menu
{
    internal class MainMenu : Menu, IUpdateable
    {
        public MainMenu() : base()
        {
            _scene.addView(new TextView(new Text(@"   _____               _____   _____   _   _    ____   "), false, false), Flow.CENTER, Flow.CENTER, 0, -13); // Top Row of Characters of Casino Text
            _scene.addView(new TextView(new Text(@"  / ____|     /\      / ____| |_   _| | \ | |  / __ \  "), false, false), Flow.CENTER, Flow.CENTER, 0, -12);
            _scene.addView(new TextView(new Text(@" | |         /  \    | (___     | |   |  \| | | |  | | "), false, false), Flow.CENTER, Flow.CENTER, 0, -11);
            _scene.addView(new TextView(new Text(@" | |        / /\ \    \___ \    | |   | . ` | | |  | | "), false, false), Flow.CENTER, Flow.CENTER, 0, -10);
            _scene.addView(new TextView(new Text(@" | |____   / ____ \   ____) |  _| |_  | |\  | | |__| | "), false, false), Flow.CENTER, Flow.CENTER, 0, -9);
            _scene.addView(new TextView(new Text(@"  \_____| /_/    \_\ |_____/  |_____| |_| \_|  \____/  "), false, false), Flow.CENTER, Flow.CENTER, 0, -8); // El Carino

            _scene.addView(new TextView(new Text("┏─────────────────┓"), false, false), Flow.CENTER, Flow.CENTER, 0, -2);
            _scene.addView(new TextView(new Text("┃                 ┃"), false, false), Flow.CENTER, Flow.CENTER, 0, -1);
            _scene.addView(new TextView(new Text("┃                 ┃"), false, false), Flow.CENTER, Flow.CENTER, 0, 0);
            _scene.addView(new TextView(new Text("┃                 ┃"), false, false), Flow.CENTER, Flow.CENTER, 0, 1);
            _scene.addView(new TextView(new Text("┃                 ┃"), false, false), Flow.CENTER, Flow.CENTER, 0, 2);
            _scene.addView(new TextView(new Text("┃                 ┃"), false, false), Flow.CENTER, Flow.CENTER, 0, 3);
            _scene.addView(new TextView(new Text("┃                 ┃"), false, false), Flow.CENTER, Flow.CENTER, 0, 4);
            _scene.addView(new TextView(new Text("┃                 ┃"), false, false), Flow.CENTER, Flow.CENTER, 0, 5);
            _scene.addView(new TextView(new Text("┗─────────────────┛"), false, false), Flow.CENTER, Flow.CENTER, 0, 6);

            _scene.addView(new ButtonView(new Text("Test"), false), Flow.CENTER, Flow.CENTER);
            _scene.addView(new ButtonView(new Text("Dice"), false), Flow.CENTER, Flow.CENTER, 0, 1);
            _scene.addView(new ButtonView(new Text("Poker"), false), Flow.CENTER, Flow.CENTER, 0, 2);
            _scene.addView(new ButtonView(new Text("Mines"), false), Flow.CENTER, Flow.CENTER, 0, 3);
            _scene.addView(new ButtonView(new Text("Exit"), false), Flow.CENTER, Flow.CENTER, 0, 4);
        }

        public override void onClick(IClickable i)
        {
            ButtonView? button = i as ButtonView;
            if (button == null)
            {
                return;
            }
            if (button.Text.getContent() == "Test")
            {
                MenuManager.open(new CrashMenu());
            }
            if (button.Text.getContent() == "Dice")
            {
                MenuManager.open(new DiceMenu());
            }
            if (button.Text.getContent() == "Poker")
            {
                MenuManager.open(new PokerMenu());
            }
            if (button.Text.getContent() == "Mines")
            {
                MenuManager.open(new MineMenu());
            }
            if (button.Text.getContent() == "Exit")
            {
                MenuManager.close();
            }
        }

        public void Update()
        {

        }
    }
}
