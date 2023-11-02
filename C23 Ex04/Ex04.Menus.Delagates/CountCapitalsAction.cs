using System;
using System.Linq;

namespace Ex04.Menus.Delagates
{
    public class CountCapitalsAction
    {
        public Action CountCapitals = delegate ()
        {
            Console.WriteLine("Enter a sentence:");
            string input = Console.ReadLine();
            int count = input.Count(char.IsUpper);
            Console.WriteLine($"Number of uppercase letters: {count}");
        };
    }
}
