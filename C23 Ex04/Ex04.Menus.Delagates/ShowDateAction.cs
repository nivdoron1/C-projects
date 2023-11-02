using System;

namespace Ex04.Menus.Delagates
{
    public class ShowDateAction
    {
        public Action ShowDate = delegate ()
        {
            DateTime currentDate = DateTime.Now;
            string dateString = currentDate.ToShortDateString();
            Console.WriteLine($"Today's Date: {dateString}");
        };
    }
}
