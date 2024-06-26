﻿namespace UNSERcasino.UI
{
    internal class DiceView : IView
    {
        private int _value;
        public int Value // The dices value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value < 0 || value > 6)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                _value = value;
            }
        }

        public DiceView(int value)
        {
            Value = value;
        }

        public void printToCanvas(Canvas canvas, int x, int y)
        {
            canvas.print(x+1, y, "-------");
            canvas.print(x, y + 1, '|');
            canvas.print(x, y + 2, '|');
            canvas.print(x, y + 3, '|');
            canvas.print(x+8, y + 1, '|');
            canvas.print(x+8, y + 2, '|');
            canvas.print(x+8, y + 3, '|');
            canvas.print(x+1, y + 4, "-------"); // Base Framework

            switch (Value) // print the eyes
            {
                case 1: { canvas.print(x + 4, y + 2, 'o'); break; }
                case 2: { canvas.print(x + 1, y + 1, 'o'); canvas.print(x + 7, y + 3, 'o'); break; }
                case 3: { canvas.print(x + 1, y + 1, 'o'); canvas.print(x + 4, y + 2, 'o'); canvas.print(x + 7, y + 3, 'o'); break; }
                case 4: { canvas.print(x + 1, y + 1, 'o'); canvas.print(x + 7, y + 3, 'o'); canvas.print(x + 7, y + 1, 'o'); canvas.print(x + 1, y + 3, 'o'); break; }
                case 5: { canvas.print(x + 1, y + 1, 'o'); canvas.print(x + 7, y + 3, 'o'); canvas.print(x + 7, y + 1, 'o'); canvas.print(x + 1, y + 3, 'o'); canvas.print(x + 4, y + 2, 'o'); break; }
                case 6: { canvas.print(x + 1, y + 1, 'o'); canvas.print(x + 7, y + 3, 'o'); canvas.print(x + 7, y + 1, 'o'); canvas.print(x + 1, y + 3, 'o'); canvas.print(x + 1, y + 2, 'o'); canvas.print(x + 7, y + 2, 'o'); break; }
            }
        }

        public int getXSize()
        {
            return 10;
        }

        public int getYSize()
        {
            return 5;
        }
    }
}
