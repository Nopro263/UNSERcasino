﻿namespace UNSERcasino.UI
{
    internal interface IView
    {
        public void printToCanvas(Canvas canvas, int x, int y);
        public int getXSize();
        public int getYSize();
    }
}
