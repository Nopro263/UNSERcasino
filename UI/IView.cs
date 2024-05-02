namespace UNSERcasino.UI
{
    internal interface IView // An UI Element
    {
        public void printToCanvas(Canvas canvas, int x, int y);
        public int getXSize();
        public int getYSize();

        public bool canSelectNext()
        {
            return true;
        }

        public bool canSelectPrev()
        {
            return true;
        }
    }
}
