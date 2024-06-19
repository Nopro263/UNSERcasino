using UNSERcasino.Game.Poker.NN;

namespace UNSERcasino.UI.Menu
{
    internal class MenuManager
    {
        private static Stack<Menu> stack = new Stack<Menu>();

        /// <summary>
        /// retrieves the current open menu
        /// </summary>
        /// <returns>the menu</returns>
        public static Menu getTopMenu()
        {
            return stack.Peek();
        }

        /// <summary>
        /// opens a new menu and sets it as the new current
        /// </summary>
        /// <param name="menu">the menu to open</param>
        public static void open(Menu menu)
        {
            stack.Push(menu);
            Console.Clear();
        }

        /// <summary>
        /// closes the current menu and exits the program if there are no more
        /// </summary>
        public static void close()
        {
            if (stack.Count == 1) // last menu open?
            {
                getTopMenu().onHide();
                NetworkTrainer.saveAll();
                Environment.Exit(0);
            }
            else
            {
                getTopMenu().onHide();

                stack.Pop();
                Console.Clear();

                getTopMenu().onResume();
            }
        }
    }
}
