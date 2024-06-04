
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

        protected string _fullContent;
        private int _maxLen; // the max len in X direction
        protected string _placeholder;
        protected bool _atStart;
        private bool _disabled;
        public TextInputView(bool vertical, int maxLen, string placeholder) : base(new Text("###"), vertical, false)
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

        public virtual void onKey(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.Backspace)
            {
                if (_fullContent.Length > 0 && !_atStart)
                {
                    _fullContent = _fullContent.Substring(0, _fullContent.Length - 1); // remove the last Element
                }

                if (_fullContent.Length == 0)
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
            if (!_disabled)
            {
                Selected = true;
            }
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

        public void disable()
        {
            Text.Bg = ConsoleColor.DarkGray; // Fix input when disabled
            _disabled = true;
        }

        public void enable()
        {
            Text.Bg = Canvas.BACKGROUND;
            _disabled = false;
        }
    }
}
