using System;

namespace Ex04_Interfaces.Menus
{
    public class ShowVersion : IMenuItem
    {
        public void Execute()
        {
            Console.WriteLine("Version: 23.3.4.9835");
        }
    }

}
