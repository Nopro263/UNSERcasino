namespace UNSERcasino.UI
{
    internal class Text
    {
        private string _content;
        public ConsoleColor Bg;
        public ConsoleColor Fg;
        public Text(string content, ConsoleColor fg = ConsoleColor.White, ConsoleColor bg = ConsoleColor.Black)
        {
            _content = content;
            Fg = fg;
            Bg = bg;
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
