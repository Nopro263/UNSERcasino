using System.Text;

namespace NoahCardOutput
{
    class PrintableCard
    {
        public PrintableCardType Type { get; private set; }
        public PrintableCardValue Value { get; private set; }
        public bool Hidden { get; private set; }
        /// <summary>
        /// Constructor for Creating Printable Cards
        /// </summary>
        /// <param name="type">color of the card</param>
        /// <param name="value">value of the card (numeric, king, etc)</param>
        /// <param name="hidden">should the card be on the back</param>
        public PrintableCard (PrintableCardType type, PrintableCardValue value, bool hidden)
        {
            Type = type;
            Value = value;
            Hidden = hidden;
        }

        private String setAt(String s, int i, char c)
        {
            StringBuilder sb = new StringBuilder(s);
            sb[i] = c;
            return sb.ToString();
        }


        private String[] addNumber()
        {
            String[] r = new String[13];
            String numberTop;
            String numberBottom;
            if (Value == PrintableCardValue.BUB || Value == PrintableCardValue.DAME || Value == PrintableCardValue.KOENIG || Value == PrintableCardValue.ASS)
            {
                numberTop = Char.ToString((char)Value);
                numberBottom = Char.ToString((char)Value);
            } else
            {
                numberTop = Convert.ToString((int)Value);
                numberBottom = Convert.ToString((int)Value);
            }
            numberTop = numberTop.PadLeft(2, ' ');
            numberBottom = numberBottom.PadRight(2, ' ');

            String color = "?";
            switch(Type)
            {
                case PrintableCardType.Pik: { color = "♠"; break; }
                case PrintableCardType.Kreuz: { color = "♣"; break; }
                case PrintableCardType.Herz: { color = "♥"; break; }
                case PrintableCardType.Karo: { color = "♦"; break; }
            }

            r[0]  = " ------------- ";
            r[1]  = "|" + numberTop + color + "          |";
            r[2]  = "|   -------   |";
            r[3]  = "|  |       |  |";
            r[4]  = "|  |       |  |";
            r[5]  = "|  |       |  |";
            r[6]  = "|  |       |  |";
            r[7]  = "|  |       |  |";
            r[8]  = "|  |       |  |";
            r[9]  = "|  |       |  |";
            r[10] = "|   -------   |";
            r[11] = "|          " + color + numberBottom + "|";
            r[12] = " ------------- ";

            return r;
        }

        private String[] getHidden()
        {
            String[] r = new String[13];

            r[0]  = " ------------- ";
            r[1]  = "| * * * * * * |";
            r[2]  = "|* * * * * * *|";
            r[3]  = "| * * * * * * |";
            r[4]  = "|* * * * * * *|";
            r[5]  = "| * * * * * * |";
            r[6]  = "|* * * * * * *|";
            r[7]  = "| * * * * * * |";
            r[8]  = "|* * * * * * *|";
            r[9]  = "| * * * * * * |";
            r[10] = "|* * * * * * *|";
            r[11] = "| * * * * * * |";
            r[12] = " ------------- ";

            return r;
        }

        private void addDesign(String[] inp)
        {
            char color = (char)Type;
            
            switch(Value)
            {
                case PrintableCardValue.BUB: {
                        inp[3] = setAt(inp[3], 4, color);
                        inp[6] = setAt(inp[6], 7, 'B');
                        inp[9] = setAt(inp[9], 10, color);
                        break;
                    }
                case PrintableCardValue.DAME:
                    {
                        inp[3] = setAt(inp[3], 4, color);
                        inp[6] = setAt(inp[6], 7, 'Q');
                        inp[9] = setAt(inp[9], 10, color);
                        break;
                    }
                case PrintableCardValue.KOENIG:
                    {
                        inp[3] = setAt(inp[3], 4, color);
                        inp[6] = setAt(inp[6], 7, 'K');
                        inp[9] = setAt(inp[9], 10, color);
                        break;
                    }
                case PrintableCardValue.ASS:
                    {
                        inp[6] = setAt(inp[6], 7, color);
                        break;
                    }

                case PrintableCardValue.ZWEI:
                    {
                        inp[4] = setAt(inp[4], 7, color);
                        inp[8] = setAt(inp[8], 7, color);
                        break;
                    }
                case PrintableCardValue.DREI:
                    {
                        inp[4] = setAt(inp[4], 7, color);
                        inp[6] = setAt(inp[6], 7, color);
                        inp[8] = setAt(inp[8], 7, color);
                        break;
                    }
                case PrintableCardValue.VIER:
                    {
                        inp[3] = setAt(inp[3], 4, color);
                        inp[3] = setAt(inp[3], 10, color);
                        inp[9] = setAt(inp[9], 10, color);
                        inp[9] = setAt(inp[9], 4, color);
                        break;
                    }
                case PrintableCardValue.FUENF:
                    {
                        inp[3] = setAt(inp[3], 4, color);
                        inp[3] = setAt(inp[3], 10, color);
                        inp[6] = setAt(inp[6], 7, color);
                        inp[9] = setAt(inp[9], 10, color);
                        inp[9] = setAt(inp[9], 4, color);
                        break;
                    }
                case PrintableCardValue.SECHS:
                    {
                        inp[3] = setAt(inp[3], 4, color);
                        inp[3] = setAt(inp[3], 10, color);

                        inp[6] = setAt(inp[6], 4, color);
                        inp[6] = setAt(inp[6], 10, color);

                        inp[9] = setAt(inp[9], 10, color);
                        inp[9] = setAt(inp[9], 4, color);
                        break;
                    }
                case PrintableCardValue.SIEBEN:
                    {
                        inp[3] = setAt(inp[3], 4, color);
                        inp[3] = setAt(inp[3], 10, color);

                        inp[6] = setAt(inp[6], 4, color);
                        inp[4] = setAt(inp[4], 7, color);
                        inp[6] = setAt(inp[6], 10, color);

                        inp[9] = setAt(inp[9], 10, color);
                        inp[9] = setAt(inp[9], 4, color);
                        break;
                    }
                case PrintableCardValue.ACHT:
                    {
                        inp[3] = setAt(inp[3], 4, color);
                        inp[3] = setAt(inp[3], 10, color);

                        inp[6] = setAt(inp[6], 4, color);
                        inp[4] = setAt(inp[4], 7, color);
                        inp[8] = setAt(inp[8], 7, color);
                        inp[6] = setAt(inp[6], 10, color);

                        inp[9] = setAt(inp[9], 10, color);
                        inp[9] = setAt(inp[9], 4, color);
                        break;
                    }
                case PrintableCardValue.NEUN:
                    {
                        inp[3] = setAt(inp[3], 4, color);
                        inp[3] = setAt(inp[3], 10, color);

                        inp[5] = setAt(inp[5], 4, color);
                        inp[5] = setAt(inp[5], 10, color);

                        inp[7] = setAt(inp[7], 4, color);
                        inp[7] = setAt(inp[7], 10, color);

                        inp[9] = setAt(inp[9], 4, color);
                        inp[9] = setAt(inp[9], 10, color);

                        inp[6] = setAt(inp[6], 7, color);

                        break;
                    }
                case PrintableCardValue.ZEHN:
                    {
                        inp[3] = setAt(inp[3], 4, color);
                        inp[3] = setAt(inp[3], 10, color);

                        inp[5] = setAt(inp[5], 4, color);
                        inp[5] = setAt(inp[5], 10, color);

                        inp[7] = setAt(inp[7], 4, color);
                        inp[7] = setAt(inp[7], 10, color);

                        inp[9] = setAt(inp[9], 4, color);
                        inp[9] = setAt(inp[9], 10, color);

                        inp[4] = setAt(inp[4], 7, color);
                        inp[8] = setAt(inp[8], 7, color);

                        break;
                    }
            }
        }

        public String[] getStrings()
        {
            if (!Hidden)
            {
                String[] r = addNumber();
                addDesign(r);
                return r;
            } else
            {
                return getHidden();
            }
        }

        public static PrintableCard FromKarte(string name)
        {
            string[] s = name.Split(" ");
            PrintableCardType type;
            PrintableCardValue value;
            int zahl;
            switch(s[1])
            {
                case "Pik": { type = PrintableCardType.Pik;break; }
                case "Kreuz": { type = PrintableCardType.Kreuz; break; }
                case "Herz": { type = PrintableCardType.Herz; break; }
                case "Karo": { type = PrintableCardType.Karo; break; }
                default: throw new Exception("Type not found");
            }

            if (int.TryParse(s[0], out zahl))
            {
                value = (PrintableCardValue) zahl;
            } else
            {
                switch(s[0])
                {
                    case "Bube": { value = PrintableCardValue.BUB; break; }
                    case "Dame": { value = PrintableCardValue.DAME; break; }
                    case "König": { value = PrintableCardValue.KOENIG; break; }
                    case "Ass": { value = PrintableCardValue.ASS; break; }
                    default: { Console.WriteLine(s[0]); throw new Exception("Value not found"); }
                }
            }

            return new PrintableCard(type, value, false);
        }
    }

    enum PrintableCardType
    {
        Herz = '♥',
        Pik = '♠',
        Karo = '♦',
        Kreuz = '♣'
    }

    enum PrintableCardValue
    {
        ZWEI = 2,
        DREI = 3,
        VIER = 4,
        FUENF = 5,
        SECHS = 6,
        SIEBEN = 7,
        ACHT = 8,
        NEUN = 9,
        ZEHN = 10,
        BUB = 'B',
        DAME = 'Q',
        KOENIG = 'K',
        ASS = 'A'
    }
}
