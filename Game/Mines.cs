namespace UNSERcasino.Game
{
    internal class Mines
    {
        private bool[][] SafeOrNot = new bool[5][]; // True -> No Mine | False -> Mine
        private int _revealedMines;

        public Mines()
        {
            for (int i = 0; i < 5; i++) { SafeOrNot[i] = new bool[5]; }
        }

        private void PlantMines(int wantedMines)
        {
            Random random = new Random();
            int minecount = wantedMines;
            while (minecount != wantedMines)
            {
                int x = random.Next(1, 5);
                int y = random.Next(1, 5);
                if (SafeOrNot[x][y] == true)
                {
                    SafeOrNot[x][y] = false;
                    minecount++;
                }
            }
        }

        //private double CalcMultiplier()
        //{
        //    double house_edge = 0.02;
        //    return (1 - house_edge) * nCr(25, diamonds) / nCr(25 - mines, diamonds);

        //}

        //private double nCr(n, r)
        //{
        //    f = System.Math.F
        //    return f(n) / f(r) / f(n - r)
        //}
    }
}
