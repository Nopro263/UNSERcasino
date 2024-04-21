namespace UNSERcasino.UI.Menu
{
    internal class CrashMenu : Menu
    {
        public CrashMenu() : base()
        {
            scene.addView(new TextView(new Text("Test"), true, false), 0, 0);
        }
    }
}
