using System;
using System.Linq;

namespace GameLogic
{
    public class Game
    {

        public int numberOfGueses { get; private set; }

        public class GameResult
        {
            public int[] turnResult { get; private set; }
            public bool isGameOver { get; private set; }

            public bool winner { get; private set; }

            public GameResult(int numberOfGueses, int numberOfTurn, int[] turnResult)
            {
                if (IsAllBool(turnResult))
                {
                    this.isGameOver = true;
                    this.winner = true;
                }
                else if (numberOfTurn >= numberOfGueses)
                {
                    this.isGameOver = true;
                    this.winner = false;
                }
                this.turnResult = turnResult;
            }

            private bool IsAllBool(int[] turn)
            {
                for (int i = 0; i < numberOfLetters; i++)
                {
                    if (turn[i] != (int)eResultPossability.Bool)
                    {
                        return false;
                    }
                }
                return true;
            }

        }
        public static int numberOfLetters = 4;


        private char[] randomChoice = new char[numberOfLetters];

        private Random random = new Random();

        public int NumberOfTurn { get; private set; } = 0;
        public string[] UserInputHistory { get; private set; }
        public int[,] ResultsHistory { get; private set; }

        public Game(string numberOfGuesesFromUser)
        {
            try
            {
                int numberOfGueses = Int32.Parse(numberOfGuesesFromUser);
                if (numberOfGueses <= 10 && numberOfGueses >= 4)
                {
                    this.numberOfGueses = numberOfGueses;
                }
                else
                {
                    throw new InvalidOperationException("number of turns needs to be between 4 to 10");
                }
            }
            catch (FormatException)
            {
                throw new InvalidOperationException("number of turns needs to be a number");
            }
            int randomNumber;
            for (int i = 0; i < numberOfLetters; i++)
            {
                randomNumber = random.Next(0, 7);
                this.randomChoice[i] = CastDigitToLetter(randomNumber);
            }

            this.UserInputHistory = new string[this.numberOfGueses];
            this.ResultsHistory = new int[this.numberOfGueses, numberOfLetters];


        }

        public GameResult RunTurn(string turn)
        {
            char[] letters = CastTurnToGameLetters(turn);
            int[] turnResult = DoTurn(letters);
            this.UserInputHistory[this.NumberOfTurn] = turn.Replace(" ", "");
            this.ResultsHistory[this.NumberOfTurn, 0] = turnResult[0];
            this.ResultsHistory[this.NumberOfTurn, 1] = turnResult[1];
            this.ResultsHistory[this.NumberOfTurn, 2] = turnResult[2];
            this.ResultsHistory[this.NumberOfTurn, 3] = turnResult[3];
            this.NumberOfTurn++;

            return new GameResult(this.numberOfGueses, this.NumberOfTurn, turnResult);
        }

        private char CastDigitToLetter(int digit)
        {
            return (char)(65 + digit);
        }

        private char[] CastTurnToGameLetters(string turn)
        {
            char[] charArray = turn.Replace(" ", "").ToCharArray();
            if (charArray.Length != numberOfLetters)
            {
                throw new InvalidOperationException("the input isn't a 4 letters");
            }
            char letter;
            for (int i=0; i < numberOfLetters; i++)
            {
                letter = charArray[i];
                if (!IsCharAGameLetter(letter))
                {
                    throw new InvalidOperationException("the input isn't a game letter");
                }
            }
            return charArray;
        }

        private bool IsCharAGameLetter(char letter)
        {
            int numericRepresntion = (int)(letter);
            return numericRepresntion >= 65 && numericRepresntion <= 72;
        }

        private int[] DoTurn(char[] letters)
        {
            int[] result = new int[numberOfLetters];
            for (int i = 0; i < numberOfLetters; i++)
            {
                if (IsBool(i, letters))
                {
                    result[i] = ((int)eResultPossability.Bool);
                }
                else if (IsHit(letters[i]))
                {
                    result[i] = ((int)eResultPossability.Hit);
                }
                else
                {
                    result[i] = ((int)eResultPossability.Nothing);
                }
            }
            return result;
        }

        private bool IsBool(int index, char[] letters)
        {
            return this.randomChoice[index] == letters[index];
        }

        private bool IsHit(char letter)
        {
            return this.randomChoice.Contains(letter);
        }

    }
}
