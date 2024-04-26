using System.Text;

namespace NoahCardOutput
{
    class CardPrinter
    {
        /// <summary>
        /// Prints a single card to the screen.
        /// </summary>
        /// <param name="card">card to print</param>
        public static void print(PrintableCard card)
        {
            Console.OutputEncoding = Encoding.UTF8;
            foreach (string s in card.getStrings())
            {
                Console.WriteLine(s);
            }
        }

        public static void print(PrintableCard[] cards)
        {
            print(new List<PrintableCard>(cards));
        }

        /// <summary>
        /// Prints a list of cards to the screen.
        /// </summary>
        /// <param name="cards">list of cards to print</param>
        public static void print(List<PrintableCard> cards)
        {
            Console.OutputEncoding = Encoding.UTF8;
            List<String[]> strings = new List<String[]>();
            foreach (PrintableCard card in cards)
            {
                strings.Add(card.getStrings());
            }
            if (strings.Count == 0)
            {
                return;
            }

            for (int i = 0; i < strings[0].Length; i++)
            {
                for (int j = 0; j < strings.Count; j++)
                {
                    Console.Write(strings[j][i]);
                    if (j < strings.Count - 1)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
