namespace UNSERcasino.UI
{
    internal class TextView : IView
    {
        public Text Text { get; private set; }
        public bool Vertical { get; private set; }
        public bool Selected { get; set; }
        public TextView(Text text, bool vertical, bool selected)
        {
            Text = text;
            Vertical = vertical;
            Selected = selected;
        }

        public override void printToCanvas(Canvas canvas, int x, int y)
        {
            ConsoleColor fg = Text.Fg;
            ConsoleColor bg = Text.Bg;

            if(Selected)
            {
                ConsoleColor temp = fg;
                fg = bg;
                bg = temp;
            }

            if (Vertical)
            {
                for(int i = 0; i < Text.getContent().Length; i++)
                {
                    canvas.setColor(x, y + i, fg, bg);
                }
                canvas.printVertical(x, y, Text.getContent());
            } else
            {
                for (int i = 0; i < Text.getContent().Length; i++)
                {
                    canvas.setColor(x + i, y, fg, bg);
                }
                canvas.print(x, y, Text.getContent());
            }
        }
    }
}
