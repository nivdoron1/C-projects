using Ex04.Menus.Delagates;
using Ex04_Interfaces.Menus;
using System;

namespace Ex04.Menus.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Menu Using Interfaces");
            IntefaceMenuManager.ShowMainMenu();
            Console.WriteLine("Menu Using Delagates");
            MenuManagerDelagates menuManager = new MenuManagerDelagates();
            menuManager.ShowMainMenu();
        }
    }
}
