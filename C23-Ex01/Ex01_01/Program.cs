using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex01_01
{
    // This is a PE because it is a .NET assembly that can be executed.
    // The file is an assembly because it can be analysed by ILDASM.

    public class Program // Added public access modifier
    {
        public const int k_th_NumberOfIntegers = 3;

        /// <summary>
        /// Main entry point of the program.
        /// </summary>

        public static void Main()
        {
            ExecuteProgram();
            Console.Read();
        }


        /// <summary>
        /// Executes the main program logic.
        /// </summary>

        private static void ExecuteProgram()
        {
            (string[] o_inputBinaryArray, float[] o_inputDecimalArray) = recieveUserInput();
            printOutput(o_inputBinaryArray, o_inputDecimalArray);
        }


        /// <summary>
        /// Prints the results and information based on user input.
        /// </summary>
        /// <param name="i_outputBinaryArray">The array of binary numbers.</param>
        /// <param name="i_outputDecimalArray">The corresponding decimal representation of binary numbers.</param>
        private static void printOutput(string[] i_outputBinaryArray, float[] i_outputDecimalArray)
        {
            Console.WriteLine(string.Format(@"The decimal numbers are: {0}, {1}, {2}.",
                i_outputDecimalArray[0],
                i_outputDecimalArray[1],
                i_outputDecimalArray[2]));

            Console.WriteLine(string.Format(@"The average number of zeros is: {0}.",
                averageOccurences(i_outputBinaryArray, '0')));

            Console.WriteLine(string.Format(@"The average number of ones is: {0}.",
                averageOccurences(i_outputBinaryArray, '1')));

            Console.WriteLine(string.Format(@"There are {0} numbers that are a power of two.",
                countPowerOfTwo(i_outputBinaryArray)));

            Console.WriteLine(string.Format(@"There are {0} numbers that are a strictly montone.",
                countStriclyMonotone(i_outputDecimalArray)));

            Console.WriteLine(string.Format(@"There are {0} numbers that are a palindrome.",
                countPalindrome(i_outputDecimalArray)));

            Console.WriteLine(string.Format(@"The greatest number is: {0}.",
                getGreatestNum(i_outputDecimalArray)));

            Console.WriteLine(string.Format(@"The smallest number is: {0}.",
                getSmallestNum(i_outputDecimalArray)));
        }


        /// <summary>
        /// Receives binary numbers from the user and returns their binary and decimal representations.
        /// </summary>
        /// <returns>A tuple containing an array of binary numbers and their decimal equivalents.</returns>

        private static (string[], float[]) recieveUserInput()
        {
            string[] o_inputBinaryArray = new string[k_th_NumberOfIntegers];
            float[] o_inputDecimalArray = new float[k_th_NumberOfIntegers];
            int currentIndex = 0;

            while (currentIndex < k_th_NumberOfIntegers)
            {
                Console.WriteLine("Please enter a valid 7 bit binary number after entering the binary number please press enter)");
                string currentInput = Console.ReadLine();
                bool isInputValid = isValidInput(currentInput);

                if (isInputValid)
                {
                    o_inputBinaryArray[currentIndex] = currentInput;
                    o_inputDecimalArray[currentIndex] = toDecimal(currentInput);
                    currentIndex++;
                }
                else
                {
                    Console.WriteLine("Invalid input!");
                }
            }

            return (o_inputBinaryArray, o_inputDecimalArray); // Added blank line before return
        }


        /// <summary>
        /// Checks if the provided string represents a binary number.
        /// </summary>
        /// <param name="i_BinaryUserInput">The string to check.</param>
        /// <returns>True if the string is a binary number, false otherwise.</returns>

        private static bool isBinary(string i_BinaryUserInput)
        {
            bool isBinary = true;
            foreach (char character in i_BinaryUserInput)
            {
                if (character != '1' && character != '0')
                {
                    isBinary = false;
                }
            }

            return isBinary; // Added blank line before return
        }


        /// <summary>
        /// Validates if the given user input is a 7-bit positive binary number.
        /// </summary>
        /// <param name="i_userInput">The string to validate.</param>
        /// <returns>True if valid, false otherwise.</returns>

        private static bool isValidInput(string i_userInput)
        {
            int LengthOfInput = i_userInput.Length; // Added blank line after local variable

            if (LengthOfInput > 0 && LengthOfInput == 7 && isBinary(i_userInput))
            {
                
                return true;
            }

            return false; // Added blank line before return
        }


        /// <summary>
        /// Counts the occurrences of a specified character in a string.
        /// </summary>
        /// <param name="i_userInput">The string to check.</param>
        /// <param name="i_requiredNum">The character to count.</param>
        /// <returns>The number of occurrences of the character.</returns>

        private static int CountOccurences(string i_userInput, char i_requiredNum)
        {
            int countOfNumber = 0;
            for (int i = 0; i < i_userInput.Length; i++)
            {
                if (i_userInput[i].Equals(i_requiredNum))
                {
                    countOfNumber++;
                }
            }

            return countOfNumber;
        }


        /// <summary>
        /// Computes the average occurrences of a specified character in an array of strings.
        /// </summary>
        /// <param name="i_outputBinaryArray">Array of binary numbers.</param>
        /// <param name="i_digit">The digit (character) to average.</param>
        /// <returns>The average number of occurrences of the digit.</returns>

        private static float averageOccurences(string[] i_outputBinaryArray, char i_digit)
        {
            float numOfOccurences = 0;
            for (int i = 0; i < k_th_NumberOfIntegers; i++)
            {
                numOfOccurences += CountOccurences(i_outputBinaryArray[i], i_digit);
            }

            return (numOfOccurences / k_th_NumberOfIntegers);
        }

        /// <summary>
        /// Determines if a binary number is a power of two.
        /// </summary>
        /// <param name="i_userInput">The binary number as a string.</param>
        /// <returns>True if the number is a power of two, false otherwise.</returns>

        private static bool isPowerOfTwo(string i_userInput)
        {
            float userDecimal = toDecimal(i_userInput);

            if (userDecimal > 1)
            {
                while (userDecimal % 2 == 0)
                {
                    userDecimal = (userDecimal / 2);
                }
            }
            return (userDecimal == 1);
        }

        /// <summary>
        /// Counts the number of binary numbers in the array that are powers of two.
        /// </summary>
        /// <param name="i_outputBinaryArray">Array of binary numbers.</param>
        /// <returns>The count of numbers that are powers of two.</returns>

        private static int countPowerOfTwo(string[] i_outputBinaryArray)
        {
            int countPowerTwo = 0;
            for (int i = 0; i < i_outputBinaryArray.Length; i++)
            {
                if (isPowerOfTwo(i_outputBinaryArray[i]))
                {
                    countPowerTwo++;
                }
            }

            return countPowerTwo;
        }

        /// <summary>
        /// Converts a binary number to its decimal equivalent.
        /// </summary>
        /// <param name="i_userInput">The binary number as a string.</param>
        /// <returns>The decimal representation of the binary number.</returns>

        private static float toDecimal(string i_userInput)
        {
            float currentOutput = 0;
            for (int i = i_userInput.Length - 1; i >= 0; i--)
            {
                if (i_userInput[i].Equals('1'))
                {
                    float BinaryToDecimal = (float)Math.Pow(2, i_userInput.Length - i - 1);
                    currentOutput += BinaryToDecimal;
                }
            }

            return currentOutput;
        }

        /// <summary>
        /// Determines if a number is a palindrome.
        /// </summary>
        /// <param name="i_userInputDec">The number to check.</param>
        /// <returns>True if the number is a palindrome, false otherwise.</returns>

        private static bool isPalindrome(float i_userInputDec)
        {
            string userInputDecimalString = i_userInputDec.ToString();
            char leftChar = userInputDecimalString[0];
            char rightChar = userInputDecimalString[userInputDecimalString.Length - 1];
            bool checkPalindrome = true;

            for (int i = 0; i < (userInputDecimalString.Length / 2); i++)
            {
                if (leftChar != rightChar)
                {
                    checkPalindrome = false;
                }
                leftChar = userInputDecimalString[i++];
                rightChar = userInputDecimalString[userInputDecimalString.Length - i - 1];
            }

            return checkPalindrome;
        }

        /// <summary>
        /// Counts the number of numbers in the array that are palindromes.
        /// </summary>
        /// <param name="i_outputDecimalArray">Array of decimal numbers.</param>
        /// <returns>The count of numbers that are palindromes.</returns>

        private static int countPalindrome(float[] i_outputDecimalArray)
        {
            int numOfPalindrome = 0;
            for (int i = 0; i < i_outputDecimalArray.Length; i++)
            {
                if (isPalindrome(i_outputDecimalArray[i]))
                {
                    numOfPalindrome++;
                }
            }

            return numOfPalindrome; // Added blank line before return
        }

        /// <summary>
        /// Determines if a number is strictly monotone.
        /// </summary>
        /// <param name="i_userInputDec">The number to check.</param>
        /// <returns>True if the number is strictly monotone, false otherwise.</returns>

        private static bool isStrictlyMonotone(float i_userInputDec)
        {
            bool isStrictlyMonotone = true;
            float rightDigit = -1;
            int userInputInt = (int)i_userInputDec;

            while (userInputInt > 0)
            {
                if (userInputInt % 10 <= rightDigit)
                {
                    isStrictlyMonotone = false;
                }
                rightDigit = userInputInt % 10;
                userInputInt = (userInputInt / 10);
            }

            return isStrictlyMonotone;
        }

        /// <summary>
        /// Counts the number of numbers in the array that are strictly monotone.
        /// </summary>
        /// <param name="i_outputDecimalArray">Array of decimal numbers.</param>
        /// <returns>The count of numbers that are strictly monotone.</returns>

        private static int countStriclyMonotone(float[] i_outputDecimalArray)
        {
            int numOfStrictlyMonotone = 0;
            for (int i = 0; i < i_outputDecimalArray.Length; i++)
            {
                if (isStrictlyMonotone(i_outputDecimalArray[i]))
                {
                    numOfStrictlyMonotone++;
                }
            }

            return numOfStrictlyMonotone; // Added blank line before return
        }

        /// <summary>
        /// Retrieves the greatest number from the array.
        /// </summary>
        /// <param name="i_outputDecimalArray">Array of decimal numbers.</param>
        /// <returns>The greatest number in the array.</returns>

        private static float getGreatestNum(float[] i_outputDecimalArray)
        {
            float greatestNum = i_outputDecimalArray[0];
            for (int i = 1; i < i_outputDecimalArray.Length; i++)
            {
                if (i_outputDecimalArray[i] > greatestNum)
                {
                    greatestNum = i_outputDecimalArray[i];
                }
            }

            return greatestNum; // Added blank line before return
        }

        /// <summary>
        /// Retrieves the smallest number from the array.
        /// </summary>
        /// <param name="i_outputDecimalArray">Array of decimal numbers.</param>
        /// <returns>The smallest number in the array.</returns>

        private static float getSmallestNum(float[] i_outputDecimalArray)
        {
            float smallestNum = i_outputDecimalArray[0];
            for (int i = 1; i < i_outputDecimalArray.Length; i++)
            {
                if (i_outputDecimalArray[i] < smallestNum)
                {
                    smallestNum = i_outputDecimalArray[i];
                }
            }

            return smallestNum; 
        }
    }
}
