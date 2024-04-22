
namespace UNSERcasino.UI
{
    internal class SliderView : TextView, IKeyListener
    {
        private static string _g(int min, int max, int val, int width)
        {
            int p = (int)((val-min) / (float)(max-min) * width);

            string e = "";
            string d = new string('.', width-p); ;

            if(p > 0)
            {
                e = new string('#', p);
            }

            string r = "[" + e + d + "] " + val.ToString().PadLeft(2);
            return r;
        }

        public int Value { get; set; }
        private int _min;
        private int _max;
        private int _width;

        public SliderView(int min, int max, int width) : base(new Text(_g(min, max, min, width)), false, false)
        {
            _min = min;
            _max = max;
            Value = min;
            _width = width;
        }

        public void deselect()
        {
            Selected = false;
        }

        public void onClick()
        {
            
        }

        public void onKey(ConsoleKey key)
        {
            if(key == ConsoleKey.LeftArrow) {
                Value--;
                if(Value < _min)
                {
                    Value = _min;
                }
            }
            if (key == ConsoleKey.RightArrow)
            {
                Value++;
                if (Value > _max)
                {
                    Value = _max;
                }
            }

            Text.setContent(_g(_min, _max, Value, _width));
        }

        public void select()
        {
            Selected = true;
        }
    }
}
