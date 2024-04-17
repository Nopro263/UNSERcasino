namespace UNSERcasino.Game
{
    internal class Mines
    {
        private bool[][] SafeOrNot = new bool[5][]; // True -> No Mine | False -> Mine
        private int _revealedDiamonds;
        private int _mineAmount;

        public Mines()
        {
            for (int i = 0; i < 5; i++) { SafeOrNot[i] = new bool[5]; }
        }

        private void PlantMines()
        {
            Random random = new Random();
            int minecount = _mineAmount;
            while (minecount != _mineAmount)
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

        private bool CheckTile(int x, int y)
        {
            if (SafeOrNot[x][y] == true)
            {
                _revealedDiamonds++;
                return true;
            }
            else
            {
                _revealedDiamonds = 0;
                return false;
            }
        }

        private double CalcMultiplier()
        {
            double house_edge = 0.02;
            return (1 - house_edge) * NCr(25, _revealedDiamonds) / NCr(25 - _mineAmount, _revealedDiamonds);

        }

        private int NCr(int n, int r)
        {
            return Factorial(n) / Factorial(r) / Factorial(n - r);
        }

        private int Factorial(int n) // Recursive Factorial Function
        {
            if (n == 0) return 1;
            return n * Factorial(n - 1);
        }
    }
}
