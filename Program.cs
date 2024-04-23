﻿using NoahCardOutput;
using System.Text;
using UNSERcasino.UI;

namespace UNSERcasino
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            UI.Menu.MenuManager.open(new UI.Menu.MainMenu());

            do
            {
                Scene scene1 = UI.Menu.MenuManager.getTopMenu().GetScene();
                if(UI.Menu.MenuManager.getTopMenu() is IUpdateable)
                {
                    try
                    {
                        ((IUpdateable)UI.Menu.MenuManager.getTopMenu()).Update();
                    } catch (SkipThisUpdateException) {}
                }
                scene1.print();

                if(Console.KeyAvailable)
                {
                    scene1.onKey(Console.ReadKey(true));
                }
            } while (true);
        }
    }
}
