﻿namespace UNSERcasino.UI.Menu
{
    internal class MenuManager
    {
        private static Stack<Menu> stack = new Stack<Menu>();

        public static Menu getTopMenu()
        {
            return stack.Peek();
        }

        public static void open(Menu menu)
        {
            stack.Push(menu);
            Console.Clear();
        }

        public static void close() {
            if(stack.Count == 1)
            {
                Environment.Exit(0);
            } else
            {
                stack.Pop();
                Console.Clear();

                getTopMenu().onResume();
            }
        }
    }
}
