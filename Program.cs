using System.Text;
using UNSERcasino.UI;

namespace UNSERcasino
{
    internal class Program
    {
        static void WriteCentered(string text)
        {
            string[] splitText = text.Split('\n');

            foreach (string line in splitText)
            {
                WriteCenteredLine(line);
            }
        }

        static void WriteCenteredLine(string line)
        {
            int screenWidth = Console.WindowWidth;
            int stringWidth = line.Length;
            int spaces = (screenWidth - stringWidth) / 2;

            for (int i = 0; i < spaces; i++)
            {
                Console.Write(" ");
            }

            foreach (char c in line)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(100);
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            string intro = "EA Sports\nIt's in the game";
            WriteCentered(intro);
            System.Threading.Thread.Sleep(1000);

            UI.Menu.MenuManager.open(new UI.Menu.MainMenu());

            do
            {
                Scene scene1 = UI.Menu.MenuManager.getTopMenu().GetScene();
                if (UI.Menu.MenuManager.getTopMenu() is IUpdateable)
                {
                    try
                    {
                        ((IUpdateable)UI.Menu.MenuManager.getTopMenu()).Update();
                    }
                    catch (SkipThisUpdateException) { }
                }
                scene1.print();

                if (Console.KeyAvailable)
                {
                    scene1.onKey(Console.ReadKey(true));
                }
            } while (true);
        }
    }
}
