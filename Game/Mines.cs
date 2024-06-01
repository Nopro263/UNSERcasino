using System.Numerics;

namespace UNSERcasino.Game
{
    internal class Mines
    {
        private int[][] SafeOrNot = new int[5][]; // True -> No Mine | False -> Mine ----------> NOW: 0 -> No Mine | 1 -> Revealed | 2 -> Mine
        private int _revealedDiamonds;
        private int _mineAmount;
        public bool MineRevealed { get; private set; } = false;
        public bool HasCashedout { get; set; } = false;
        public bool firstBet { get; set; } = true;

        public Mines()
        {
            for (int i = 0; i < 5; i++) { SafeOrNot[i] = new int[5]; }
        }

        public void StartGame(int mineAmount)
        {
            _mineAmount = mineAmount;
            PlantMines();
        }

        public double Play(int x, int y)
        {
            if (CheckTile(x, y))
            {
                return CalcMultiplier();
            }
            else
            {
                return 0;
            }
        }

        private void PlantMines()
        {
            Random random = new Random();
            int minecount = 0;
            while (minecount != _mineAmount)
            {
                int x = random.Next(0, 5);
                int y = random.Next(0, 5);
                if (SafeOrNot[x][y] == 0)
                {
                    SafeOrNot[x][y] = 2;
                    minecount++;
                }
            }
        }

        private bool CheckTile(int x, int y)
        {
            if (SafeOrNot[x][y] == 0)
            {
                _revealedDiamonds++;
                SafeOrNot[x][y] = 1;
                return true;
            }
            else if (SafeOrNot[x][y] == 2)
            {
                _revealedDiamonds = 0;
                MineRevealed = true;
                return false;
            }
            else if (SafeOrNot[x][y] == 1)
            {
                return true;
            }
            return false;
        }

        public double CalcMultiplier()
        {
            double house_edge = 0.02;
            return (1 - house_edge) * (double)NCr(25, _revealedDiamonds) / (double)NCr(25 - _mineAmount, _revealedDiamonds);
        }

        private BigInteger NCr(int n, int r)
        {
            return Factorial(n) / (Factorial(r) * Factorial(n - r));
        }

        static BigInteger Factorial(int number)
        {
            BigInteger result = 1;

            for (int i = 1; i <= number; i++)
            {
                result *= i;
            }

            return result;
        }

    }
}
