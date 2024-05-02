namespace UNSERcasino.Game
{
    internal class Peg
    {
        public Peg(int posX, int posY, int angle)
        {
            PosX = posX;
            PosY = posY;
            Angle = angle;
        }

        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public int Angle { get; private set; }
    }
}
