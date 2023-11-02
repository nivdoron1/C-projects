using System;

namespace Ex01_05
{
    public class Program
    {
        public const int k_th_NumberOfDigits = 6;
        public static void Main()
        {
            string StringuserInput = GetUserInput();
            PrintUserInput(StringuserInput);
            Console.Read();
        }

        /// <summary>
        /// Prompt the user to provide a 6-digit input and validate it.
        /// </summary>
        /// <returns>The validated 6-digit input from the user.</returns>
        private static string GetUserInput()
        {
            bool receivedValidInput = false;
            string userInput = "";

            Console.WriteLine("Insert an input of 6 digits!!!");

            while (!receivedValidInput)
            {
                userInput = Console.ReadLine();

                if (IsValidInput(userInput))
                {
                    receivedValidInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input!");
                    Console.WriteLine("Insert an input of 6 digits!!!");
                }
            }

            return userInput;
        }

        /// <summary>
        /// Validates the input to ensure it's a 6-digit number.
        /// </summary>
        private static bool IsValidInput(string userInput)
        {
            return OnlyDigits(userInput) && (userInput.Length == k_th_NumberOfDigits);
        }

        /// <summary>
        /// Determines if a given string consists of digits only.
        /// </summary>
        private static bool OnlyDigits(string userInput)
        {
            foreach (char digit in userInput)
            {
                if (!Char.IsDigit(digit))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Outputs results based on the user's input.
        /// </summary>
        private static void PrintUserInput(string userInput)
        {
            Console.WriteLine(String.Format(@"There are {0} numbers that are smaller than the unit's digit ({1}).", CountSmallerThanUnits(userInput), GetUnitsDigit(userInput)));
            Console.WriteLine(String.Format(@"The smallest digit is: {0}.", GetTheSmallestDigit(userInput)));
            Console.WriteLine(String.Format(@"There are {0} numbers that are divisible by 3.", CountDigitsDividedByThree(userInput)));
            Console.WriteLine(String.Format(@"The average of the digits is: {0}.", ComputeAverageOfDigits(userInput)));
        }

        /// <summary>
        /// Identifies the smallest digit within the user's input.
        /// </summary>
        private static int GetTheSmallestDigit(string userInput)
        {
            int smallestDigitInTheInput = 10000;

            foreach (char digit in userInput)
            {
                int currentComparingDigit = int.Parse(digit.ToString());
                smallestDigitInTheInput = Math.Min(smallestDigitInTheInput, currentComparingDigit);
            }

            return smallestDigitInTheInput;
        }

        /// <summary>
        /// Calculates the average of all digits in the user's input.
        /// </summary>
        private static float ComputeAverageOfDigits(string userInput)
        {
            float sumOfDigits = 0;

            foreach (char digit in userInput)
            {
                sumOfDigits += int.Parse(digit.ToString());
            }

            return sumOfDigits / k_th_NumberOfDigits;
        }

        /// <summary>
        /// Determines if a digit is divisible by three.
        /// </summary>
        private static bool IsDigitDividedByThree(char digit)
        {
            return int.Parse(digit.ToString()) % 3 == 0;
        }

        /// <summary>
        /// Counts how many digits within the user's input are divisible by three.
        /// </summary>
        private static int CountDigitsDividedByThree(string userInput)
        {
            int countDigitsDividedByThree = 0;

            foreach (char digit in userInput)
            {
                if (IsDigitDividedByThree(digit))
                {
                    countDigitsDividedByThree++;
                }
            }

            return countDigitsDividedByThree;
        }

        /// <summary>
        /// Extracts the unit's digit from the user's input.
        /// </summary>
        private static int GetUnitsDigit(string userInput) => int.Parse(userInput[userInput.Length - 1].ToString());

        /// <summary>
        /// Counts the number of digits in the user's input that are smaller than the unit's digit.
        /// </summary>
        private static int CountSmallerThanUnits(string userInput)
        {
            int unitDigit = GetUnitsDigit(userInput);
            int countSmallerThanUnitDigit = 0;

            foreach (char digit in userInput)
            {
                int currentDigit = int.Parse(digit.ToString());

                if (unitDigit > currentDigit)
                {
                    countSmallerThanUnitDigit++;
                }
            }

            return countSmallerThanUnitDigit;
        }
    }
}
