using NoahCardOutput;

namespace UNSERcasino
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CardPrinter.print(PrintableCard.FromKarte("König Herz"));
            Console.ReadLine();
        }
    }
}
