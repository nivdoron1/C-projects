using System;

namespace Ex01_04
{
    public class Program
    {
        public static void Main()
        {
            ExecuteProgram();
            Console.Read();
        }

        /// <summary>
        /// Acts as the primary access point to execute the program's functionality.
        /// </summary>
        private static void ExecuteProgram()
        {
            string userInputString = GetValidUserInput();
            PrintUserInput(userInputString);
        }

        /// <summary>
        /// Retrieves a valid user input string consisting of 6 English letters or digits.
        /// </summary>
        /// <returns>Validated user input string.</returns>
        private static string GetValidUserInput()
        {
            bool isValidInput = false;
            string userInput = "";

            Console.WriteLine("Insert a input of 6 letters or digits. The letters should be only in English A-Z");

            while (!isValidInput)
            {
                userInput = Console.ReadLine();

                if (IsValidInput(userInput))
                {
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input!");
                    Console.WriteLine("Insert a input of 6 letters or digits. The letters should be only in English A-Z");
                }
            }

            return userInput;
        }

        /// <summary>
        /// Prints analysis of the given user input string.
        /// </summary>
        /// <param name="userInput">User input string to be analyzed.</param>
        private static void PrintUserInput(string userInput)
        {
            if (RecursiveIsPalindrome(userInput))
            {
                Console.WriteLine("The string is a palindrome");
            }
            else
            {
                Console.WriteLine("The string isn't a palindrome");
            }

            if (OnlyDigits(userInput))
            {
                if (IsThreeMultipication(userInput))
                {
                    Console.WriteLine("The number is a multiplication of three");
                }
                else
                {
                    Console.WriteLine("The number is not a multiplication of three");
                }
            }

            if (OnlyEnglishLetters(userInput))
            {
                Console.WriteLine(string.Format(@"The number of lowercase letters in the string is {0}", CountLowerCaseLetters(userInput)));
            }
        }

        /// <summary>
        /// Validates if the provided string consists of either 6 digits or English letters.
        /// </summary>
        /// <param name="userInput">String to be validated.</param>
        /// <returns>Boolean value indicating validity.</returns>
        private static bool IsValidInput(string userInput)
        {
            return (OnlyDigits(userInput) || OnlyEnglishLetters(userInput)) && userInput.Length == 6;
        }

        /// <summary>
        /// Checks if the provided string contains only English letters.
        /// </summary>
        /// <param name="userInput">String to be checked.</param>
        /// <returns>Boolean value indicating if the string contains only English letters.</returns>
        private static bool OnlyEnglishLetters(string userInput)
        {
            foreach (char letter in userInput)
            {
                if (!((letter >= 'A' && letter <= 'Z') || (letter >= 'a' && letter <= 'z')))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if the provided string contains only digits.
        /// </summary>
        /// <param name="userInput">String to be checked.</param>
        /// <returns>Boolean value indicating if the string contains only digits.</returns>
        private static bool OnlyDigits(string userInput)
        {
            foreach (char letter in userInput)
            {
                if (!char.IsDigit(letter))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if the provided string forms a palindrome.
        /// </summary>
        /// <param name="userInput">String to be checked.</param>
        /// <returns>Boolean value indicating if the string is a palindrome.</returns>
        private static bool RecursiveIsPalindrome(string userInput)
        {
            if (userInput.Length <= 1)
            {
                return true;
            }
            if(userInput[0] != userInput[userInput.Length - 1])
            {
                return false;
            }
            string subsetUserInput = userInput.Substring(1, userInput.Length - 2);
            return RecursiveIsPalindrome(subsetUserInput);
        }

        /// <summary>
        /// Checks if the provided string (interpreted as a number) is a multiple of three.
        /// </summary>
        /// <param name="userInput">String to be checked.</param>
        /// <returns>Boolean value indicating if the number is a multiple of three.</returns>
        private static bool IsThreeMultipication(string userInput)
        {
            int.TryParse(userInput, out int userInputInt);
            return userInputInt % 3 == 0;
        }

        /// <summary>
        /// Counts the number of lowercase letters in a given string.
        /// </summary>
        /// <param name="userInput">String whose lowercase letters will be counted.</param>
        /// <returns>Count of lowercase letters in the string.</returns>
        private static int CountLowerCaseLetters(string userInput)
        {
            int countLowerCase = 0;

            foreach (char letter in userInput)
            {
                if (char.IsLower(letter))
                {
                    countLowerCase++;
                }
            }

            return countLowerCase;
        }
    }
}
