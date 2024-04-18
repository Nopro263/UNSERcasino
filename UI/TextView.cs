namespace UNSERcasino.UI
{
    internal class TextView : BaseView
    {
        public Text Text { get; private set; }
        public bool Vertical { get; private set; }
        public bool Selected { get; private set; }
        public TextView(Text text, bool vertical, bool selected)
        {
            Text = text;
            Vertical = vertical;
            Selected = selected;
        }

        public override void printToCanvas(Canvas canvas, int x, int y)
        {
            if(Vertical)
            {
                for(int i = 0; i < Text.getContent().Length; i++)
                {
                    canvas.setColor(x, y + i, Text.Fg, Text.Bg);
                }
                canvas.printVertical(x, y, Text.getContent());
            } else
            {
                for (int i = 0; i < Text.getContent().Length; i++)
                {
                    canvas.setColor(x + i, y, Text.Fg, Text.Bg);
                }
                canvas.print(x, y, Text.getContent());
            }
        }
    }
}
