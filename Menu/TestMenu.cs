using UNSERcasino.game;
using UNSERcasino.Game;

namespace UNSERcasino.Menu
{
    internal class TestMenu : BaseMenu
    {
        private int _index = 0;
        int counter = 0;

        Crash crash = new Crash();
        Dice dice = new Dice(100, 1, 100);
        int abc = 1;

        public override string[] update(TimeSpan s, string? i)
        {
            string[] d = new string[2];
            d[0] = "d";
            d[1] = Convert.ToString(s.Ticks);

            double result = dice.CalcMultiplier(abc, true); /*crash.Play();*/
            Console.WriteLine(abc);
            Console.Write(result);
            abc++;
            Thread.Sleep(500);

            //while ((1 + counter * 0.01) <= result) //Noah wenn du des siegst, des is nur so a playtest.
            //{
            //    Console.CursorTop = 0;
            //    Console.CursorLeft = 0;
            //    Console.CursorVisible = false;
            //    counter++;
            //    Console.WriteLine(String.Format("{0:0.00}", 1 + counter * 0.01) + "x");
            //    Console.WriteLine("Press any key to cashout.");
            //    if (Console.KeyAvailable) // When a key is pressed this method will enter the if()
            //    {
            //        Console.WriteLine("Player earned " + 100 * counter / 100 + "$ in profits.");
            //        Thread.Sleep(10000);
            //    }
            //    Thread.Sleep(50);
            //}
            //counter = 0;

            _index++;
            return d;
        }
    }
}
