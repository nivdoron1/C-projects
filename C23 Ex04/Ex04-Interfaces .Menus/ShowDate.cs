using System;

namespace Ex04_Interfaces.Menus
{
    public class ShowDate : IMenuItem
    {
        public void Execute()
        {
            Console.WriteLine($"Current Date: {DateTime.Now.ToShortDateString()}");
        }
    }
}
