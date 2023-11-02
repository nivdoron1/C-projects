using System;
using System.Text;

namespace Ex01_02
{
    /// <summary>
    /// Main program class to print sand-clock patterns.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entry point of the application.
        /// </summary>
        public static void Main()
        {
            PrintSandClock(5);
            Console.Read();
        }

        /// <summary>
        /// Initialize the sand-clock printing process.
        /// </summary>
        /// <param name="numOfLines">Number of lines for the sand-clock.</param>
        public static void PrintSandClock(int numOfLines)
        {
            const bool isSandClockDescending = true;

            RecursiveSandClockPrinter(isSandClockDescending, numOfLines, 0, numOfLines);
        }

        /// <summary>
        /// Recursively prints the sand-clock pattern.
        /// </summary>
        /// <param name="isSandClockDescending">Indicates whether the sand-clock is currently in descending or ascending mode.</param>
        /// <param name="numOfAsterisk">Number of asterisks in the current line.</param>
        /// <param name="numOfLine">Current line number.</param>
        /// <param name="targetNumOfLines">Total number of lines for the sand-clock.</param>
        private static void RecursiveSandClockPrinter(bool isSandClockDescending, int numOfAsterisk, int numOfLine, int targetNumOfLines)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (numOfAsterisk == 1)
            {
                isSandClockDescending = false;
            }

            if (numOfLine < 0)
            {
                return;
            }

            stringBuilder.Append(' ', numOfLine).Append('*', numOfAsterisk);
            Console.WriteLine(stringBuilder.ToString());

            if (isSandClockDescending)
            {
                RecursiveSandClockPrinter(isSandClockDescending, numOfAsterisk - 2, numOfLine + 1, targetNumOfLines);
            }
            else
            {
                RecursiveSandClockPrinter(isSandClockDescending, numOfAsterisk + 2, numOfLine - 1, targetNumOfLines);
            }
        }
    }
}
