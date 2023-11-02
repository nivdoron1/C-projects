using System;

namespace Ex04.Menus.Delagates
{
    public class ShowTimeAction
    {
        public Action ShowTime = delegate ()
        {
            DateTime currentTime = DateTime.Now;
            string timeString = currentTime.ToLongTimeString();
            Console.WriteLine($"Current Time: {timeString}");
        };
    }
}
