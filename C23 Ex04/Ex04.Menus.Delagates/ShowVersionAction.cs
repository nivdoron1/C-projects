using System;

namespace Ex04.Menus.Delagates
{
    public class ShowVersionAction
    {
        public Action ShowVersion = delegate ()
        {
            Console.WriteLine("Version: 23.3.4.9835");
        };
    }
}
