namespace UNSERcasino.UI
{
    internal class CardValue
    {
        private int? _value;
        private char? _char;

        public int Rating {  get
            {
                if(_value != null) return _value.Value;
                switch(_char)
                {
                    case 'B': return 11;
                    case 'D': return 12;
                    case 'K': return 13;
                    case 'A': return 14;
                }
                return 0;
            } }
        private CardValue(int v) {
            _value = v;
        }
        private CardValue(char v)
        {
            _char = v;
        }

        public override string ToString()
        {
            if(_value != null)
            {
                return ((int)_value).ToString();
            }
            else
            {
                if (_char != null)
                {
                    return ((char)_char).ToString();
                }
            }

            return string.Empty;
        }

        public static readonly CardValue _SINGLE_ASS_DO_NOT_USE_OUTSIDE_OF_EVALUATION = new CardValue(1);

        public static readonly CardValue ZWEI = new CardValue(2);
        public static readonly CardValue DREI = new CardValue(3);
        public static readonly CardValue VIER = new CardValue(4);
        public static readonly CardValue FUENF = new CardValue(5);
        public static readonly CardValue SECHS = new CardValue(6);
        public static readonly CardValue SIEBEN = new CardValue(7);
        public static readonly CardValue ACHT = new CardValue(8);
        public static readonly CardValue NEUN = new CardValue(9);
        public static readonly CardValue ZEHN = new CardValue(10);

        public static readonly CardValue BUB = new CardValue('B');
        public static readonly CardValue DAME = new CardValue('D');
        public static readonly CardValue KOENIG = new CardValue('K');
        public static readonly CardValue ASS = new CardValue('A');

        public static CardValue[] GetValues()
        {
            return new CardValue[] { ZWEI, DREI, VIER, FUENF, SECHS, SIEBEN, ACHT, NEUN, ZEHN, BUB, DAME, KOENIG, ASS };
        }

        public override bool Equals(object? obj)
        {
            return obj != null &&
                obj.GetType() == this.GetType() &&
                this._value == ((CardValue)obj)._value &&
                this._char == ((CardValue)obj)._char;
        }
    }
}
