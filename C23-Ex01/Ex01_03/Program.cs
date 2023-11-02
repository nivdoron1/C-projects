using System;

namespace Ex01_03
{
    /// <summary>
    /// Main program class to get user input and print sand-clock patterns.
    /// </summary>
    public class Program
    {
        private const string insertUserCommend = "Insert the number of lines for the sand clock and then press enter.";

        /// <summary>
        /// Main entry point of the application.
        /// </summary>
        public static void Main()
        {
            int numOfLines = GetUserInput();
            Ex01_02.Program.PrintSandClock(numOfLines);
            Console.Read();
        }

        /// <summary>
        /// Gets the number of lines for the sand-clock from user input. 
        /// Validates the input to ensure it's a non-negative integer.
        /// </summary>
        /// <returns>Number of lines for the sand-clock.</returns>
        private static int GetUserInput()
        {
            bool receivedValidInput = false;
            int numOfLines = 0;

            Console.WriteLine(insertUserCommend);

            while (!receivedValidInput)
            {
                bool isValidInput = int.TryParse(Console.ReadLine(), out numOfLines);

                if (isValidInput && numOfLines > 0)
                {
                    receivedValidInput = true;

                    if (numOfLines % 2 == 0)
                    {
                        numOfLines++;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input!");
                    Console.WriteLine(insertUserCommend);
                }
            }

            return numOfLines;
        }
    }
}
