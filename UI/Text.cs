namespace UNSERcasino.UI
{
    internal class Text // Wrapper-class for using colored text.
    {
        private string _content;
        public ConsoleColor Bg;
        public ConsoleColor Fg;
        public Text(string content, ConsoleColor? fg = null, ConsoleColor? bg = null)
        {
            _content = content;
            if (fg != null)
            {
                Fg = (ConsoleColor)fg;
            } else
            {
                Fg = Canvas.FOREGROUND;
            }

            if (bg != null)
            {
                Bg = (ConsoleColor)bg;
            } else
            {
                Bg = Canvas.BACKGROUND;
            }
        }

        public string getContent()
        {
            return _content;
        }

        public void setContent(string content)
        {
            _content = content;
        }
    }
}
