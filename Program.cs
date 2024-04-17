using NoahCardOutput;

namespace UNSERcasino
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MenuManager m = new MenuManager();
            m.open(new Menu.TestMenu());

            while(true)
            {
                m.update();
            }
        }
    }
}
