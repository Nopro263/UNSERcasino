namespace UNSERcasino.UI.Menu
{
    internal class MainMenu : Menu
    {
        public MainMenu() : base()
        {
            scene.addView(new ButtonView(new Text("Crash"), false), Console.BufferWidth / 2, Console.BufferHeight / 2);
            scene.addView(new ButtonView(new Text("Exit"), false), Console.BufferWidth / 2, Console.BufferHeight / 2 + 1);
        }

        public override void onClick(ButtonView button)
        {
            if(button.Text.getContent() == "Crash")
            {
                MenuManager.open(new CrashMenu());
            }
            if (button.Text.getContent() == "Exit")
            {
                MenuManager.close();
            }
        }
    }
}
