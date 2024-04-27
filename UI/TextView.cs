namespace UNSERcasino.UI
{
    internal class TextView : IView
    {
        public Text Text { get; private set; } // Text to show
        public bool Vertical { get; private set; } // Should the text be printed vertical?
        public bool Selected { get; set; } // Invert Fg and Bg
        public TextView(Text text, bool vertical, bool selected)
        {
            Text = text;
            Vertical = vertical;
            Selected = selected;
        }

        public virtual void printToCanvas(Canvas canvas, int x, int y)
        {
            ConsoleColor fg = Text.Fg;
            ConsoleColor bg = Text.Bg;

            if (Selected) // Invert Fg and Bg
            {
                ConsoleColor temp = fg;
                fg = bg;
                bg = temp;
            }

            if (Vertical)
            {
                for (int i = 0; i < Text.getContent().Length; i++)
                {
                    canvas.setColor(x, y + i, fg, bg); // Set the correct color
                }
                canvas.printVertical(x, y, Text.getContent()); // print the content
            }
            else
            {
                for (int i = 0; i < Text.getContent().Length; i++)
                {
                    canvas.setColor(x + i, y, fg, bg);
                }
                canvas.print(x, y, Text.getContent());
            }
        }

        // get an estimated size in the X direction
        public int getXSize()
        {
            if (Vertical)
            {
                return 1;
            }
            return Text.getContent().Length;
        }

        // get an estimated size in the Y direction
        public int getYSize()
        {
            if (Vertical)
            {
                return Text.getContent().Length;
            }
            return 1;
        }
    }
}
