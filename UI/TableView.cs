﻿namespace UNSERcasino.UI
{
    internal class TableView : BaseView
    {
        protected Text[][] _data;
        public TableView(Text[][] data) {
            _data = data;
        }

        private int[] getLongestValue()
        {
            int[] res = new int[_data.Length];
            for(int i = 0; i < _data.Length; i++)
            {
                res[i] = 0;
                for (int j = 0; j < _data[i].Length; j++)
                {
                    if (_data[i][j].getContent().Length > res[i])
                    {
                        res[i] = _data[i][j].getContent().Length;
                    }
                }
            }

            return res;
        }
        public override void printToCanvas(Canvas canvas, int x, int y)
        {
            int[] longestWords = getLongestValue();
            int offset = 0;
            int _x = 0;
            

            for (int j = 0; j < _data.Length; j++)
            {
                Text[] column = _data[j];
                _x = 0;
                for (int i = 0; i < column.Length; i++)
                {
                    if(i > 0)
                    {
                        _x = 1;
                    }
                    for (int k = 0; k < column[i].getContent().Length; k++)
                    {
                        canvas.setColor(x + offset + k, y + i + _x, column[i].Fg, column[i].Bg);
                    }
                    canvas.print(x + offset, y + i + _x, column[i].getContent().PadRight(longestWords[j]));
                }

                offset += longestWords[j] + 2;
            }
        }

        public override int getXSize()
        {
            int[] v = getLongestValue();
            int max = 0;
            for(int i = 0; i < v.Length; i++)
            {
                max += v[i];
            }

            return max;
        }

        public override int getYSize()
        {
            int max = 0;
            for(int i = 0; i < _data.Length; i++)
            {
                if (_data[i].Length > max)
                {
                    max = _data[i].Length;
                }
            }

            return max+1;
        }
    }
}
