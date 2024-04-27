namespace UNSERcasino.UI
{
    internal class CardValue
    {
        private int? _value;
        private char? _char;
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
    }
}
