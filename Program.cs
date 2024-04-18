using NoahCardOutput;
using System.Text;
using UNSERcasino.UI;

namespace UNSERcasino
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            
            Canvas cv = new Canvas(Console.BufferWidth, Console.BufferHeight);
            cv.print(2, 2, new TableView(new Text[][] {new Text[] {new Text("Name"), new Text("Noah") , new Text("Emilio") }, new Text[] { new Text("Bet"), new Text("10"), new Text("1000") } }));
            cv.print(0, 0, new TextView(new Text("Hi", ConsoleColor.Blue, ConsoleColor.Black), true, true));

            cv.show();
        }
    }
}
