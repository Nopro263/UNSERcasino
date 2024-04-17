using NoahCardOutput;

namespace UNSERcasino
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MenuManager.Instance.open(new Menu.MainMenu());

            while(true)
            {
                MenuManager.Instance.update();
            }
        }
    }
}
