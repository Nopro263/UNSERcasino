
namespace UNSERcasino.UI
{
    internal class TextInputView : TextView, IKeyListener
    {
        public string FullContent { get {
                return _fullContent;
            } set {
                _fullContent = value;
            } }

        private string _fullContent;
        private int _maxLen;
        public TextInputView(bool vertical, int maxLen) : base(new Text("###", ConsoleColor.White, ConsoleColor.DarkGray), vertical, false)
        {
            _fullContent = "";
            _maxLen = maxLen;

            Text.setContent(new string(' ', maxLen));
        }

        public void deselect()
        {
            Selected = false;
        }

        public void onClick()
        {
            
        }

        public void onKey(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.Backspace)
            {
                if (_fullContent.Length > 0)
                {
                    _fullContent = _fullContent.Substring(0, _fullContent.Length - 1);
                } else
                {
                    _fullContent = "";
                }
            }
            else
            {
                _fullContent += key.KeyChar.ToString();
            }
        }

        public void select()
        {
            Selected = true;
        }

        public override void printToCanvas(Canvas canvas, int x, int y)
        {
            string content;
            if(_fullContent.Length > _maxLen)
            {
                content = _fullContent.Substring(_fullContent.Length - _maxLen, _maxLen);
            } else if(_fullContent.Length < _maxLen)
            {
                content = _fullContent + new string(' ', _maxLen - _fullContent.Length);
            } else
            {
                content = _fullContent;
            }

            Text.setContent(content);

            base.printToCanvas(canvas, x, y);
        }
    }
}
