using NoahCardOutput;
using System.Text;

namespace UNSERcasino
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            MenuManager.Instance.open(new Menu.MainMenu());

            while(true)
            {
                MenuManager.Instance.update();
            }
        }
    }
}
