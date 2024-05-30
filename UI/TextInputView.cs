
namespace UNSERcasino.UI
{
    internal class TextInputView : TextView, IKeyListener
    {
        public string FullContent // The content the user has inputed
        {
            get
            {
                return _fullContent;
            }
            set
            {
                _fullContent = value;
            }
        }

        private string _fullContent;
        private int _maxLen; // the max len in X direction
        private string _placeholder;
        private bool _atStart;
        public TextInputView(bool vertical, int maxLen, string placeholder) : base(new Text("###", ConsoleColor.White, ConsoleColor.DarkGray), vertical, false)
        {
            _fullContent = placeholder;
            _maxLen = maxLen;
            _placeholder = placeholder;
            _atStart = true;

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
                if (_fullContent.Length > 0 && !_atStart)
                {
                    _fullContent = _fullContent.Substring(0, _fullContent.Length - 1); // remove the last Element
                }
                
                if(_fullContent.Length == 0)
                {
                    _fullContent = _placeholder;
                    _atStart = true;
                }
            }
            else
            {
                if (_atStart)
                {
                    _atStart = false;
                    _fullContent = "";
                }
                _fullContent += key.KeyChar.ToString(); // add it
            }
        }

        public void select()
        {
            Selected = true;
        }

        public override void printToCanvas(Canvas canvas, int x, int y)
        {
            string content;
            // Bring the string to _maxLen.
            if (_fullContent.Length > _maxLen)
            {
                content = _fullContent.Substring(_fullContent.Length - _maxLen, _maxLen);
            }
            else if (_fullContent.Length < _maxLen)
            {
                content = _fullContent + new string(' ', _maxLen - _fullContent.Length);
            }
            else
            {
                content = _fullContent;
            }

            Text.setContent(content);

            base.printToCanvas(canvas, x, y);
        }
    }
}
