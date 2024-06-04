using System.Text.RegularExpressions;

namespace UNSERcasino.UI
{
    internal class TextInputViewRegex : TextInputView
    {
        Regex rx = new Regex(@"^[0-9]+$");
        public TextInputViewRegex(bool vertical, int maxLen, string placeholder) : base(vertical, maxLen, placeholder)
        {
        }

        public override void onKey(ConsoleKeyInfo key)
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
                if (rx.IsMatch(key.KeyChar.ToString())) // check if the key is a number
                {
                    _fullContent += key.KeyChar.ToString(); // add it
                }
            }
        }
    }
}
