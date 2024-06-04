using System.Text;
using UNSERcasino.UI;
using UNSERcasino.UI.Menu;

namespace UNSERcasino
{
    internal class Program
    {
        /// <summary>
        /// writes a block of text centered
        /// </summary>
        /// <param name="text">the block, seperated by \n</param>
        static void WriteCentered(string text)
        {
            string[] splitText = text.Split('\n');

            foreach (string line in splitText)
            {
                WriteCenteredLine(line);
            }
        }

        /// <summary>
        /// writes a single line centered to the screen
        /// </summary>
        /// <param name="line">the line</param>
        static void WriteCenteredLine(string line)
        {
            int screenWidth = Console.WindowWidth;
            int stringWidth = line.Length;
            int spaces = (screenWidth - stringWidth) / 2;

            for (int i = 0; i < spaces; i++)
            {
                Console.Write(" ");
            }

            foreach (char c in line)
            {
                Console.Write(c);
                Thread.Sleep(100);
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
            Console.OutputEncoding = Encoding.UTF8;

            // it's funny
            string intro = "EA Sports\nIt's in the game";
            WriteCentered(intro);
            Thread.Sleep(1000);


            // open the main menu
            MenuManager.open(new MainMenu());

            int fps = 0;
            int sec = DateTime.Now.Second;
            int updateFps = 0;

            bool showFps = false;
            do
            {
                // fps stuff
                if (sec != DateTime.Now.Second)
                {
                    sec = DateTime.Now.Second;
                    updateFps = fps;
                    fps = 0;
                }



                Scene scene = MenuManager.getTopMenu().GetScene();

                if (MenuManager.getTopMenu() is IUpdateable updateable)
                {
                    try
                    {
                        updateable.Update();
                    }
                    catch (SkipThisUpdateException) { } // the name says it all
                }

                try
                {
                    scene.print(updateFps, showFps);
                } catch(IndexOutOfRangeException ex)
                {
                    Console.Clear();
                    WriteCentered("The window is too small! ....... Exiting!");
                    Environment.Exit(1);
                }
                fps++;

                if (Console.KeyAvailable)
                {
                    // handle all keyboard input and don't show it
                    scene.onKey(Console.ReadKey(true));
                }
            } while (true);
        }

        static void OnProcessExit(object sender, EventArgs e)
        {
            //from: https://stackoverflow.com/questions/2555292/how-to-run-code-before-program-exit
            CasinoManager.Instance.Save();
        }
    }
}
