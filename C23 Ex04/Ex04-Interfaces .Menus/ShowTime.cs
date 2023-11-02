using System;

namespace Ex04_Interfaces.Menus
{
    public class ShowTime : IMenuItem
    {
        public void Execute()
        {
            Console.WriteLine($"Current Time: {DateTime.Now.ToLongTimeString()}");
        }
    }

}
