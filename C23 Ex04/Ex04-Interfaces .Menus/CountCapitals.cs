using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex04_Interfaces.Menus
{

    public class CountCapitals : IMenuItem
    {
        public void Execute()
        {
            Console.WriteLine("Enter a sentence:");
            string input = Console.ReadLine();
            int count = input.Count(char.IsUpper);
            Console.WriteLine($"Number of uppercase letters: {count}");
        }
    }

}
