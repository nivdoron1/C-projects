using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GameLogic
{
    public class Game
    {
        public const int NumberOfLetters = 4;

        public int NumberOfGuesses { get; private set; }
        public int NumberOfTurns { get; private set; } = 0;
        public string[] UserInputHistory { get; private set; }
        public int[,] ResultsHistory { get; private set; }

        private readonly char[] m_RandomChoice = new char[NumberOfLetters];
        private readonly Random m_Random = new Random();

        public class GameResult
        {
            public int[] TurnResult { get; private set; }
            public bool IsGameOver { get; private set; }
            public bool Winner { get; private set; }

            public GameResult(int i_NumberOfGuesses, int i_NumberOfTurns, int[] i_TurnResult)
            {
                TurnResult = i_TurnResult;

                if (IsAllBool(i_TurnResult))
                {
                    IsGameOver = true;
                    Winner = true;
                }
                else if (i_NumberOfTurns >= i_NumberOfGuesses)
                {
                    IsGameOver = true;
                    Winner = false;
                }
            }

            private bool IsAllBool(int[] i_Turn)
            {
                return i_Turn.All(result => result == (int)eResultPossibility.Bool);
            }
        }

        public static readonly Dictionary<Color, char> ColorToGameLetterMap = new Dictionary<Color, char>
        {
            { Color.Red, 'A' },
            { Color.Blue, 'B' },
            { Color.Green, 'C' },
            { Color.Yellow, 'D' },
            { Color.Orange, 'E' },
            { Color.Purple, 'F' },
            { Color.Brown, 'G' },
            { Color.Pink, 'H' }
        };

        public Game(string i_NumberOfGuessesFromUser)
        {
            try
            {
                NumberOfGuesses = Int32.Parse(i_NumberOfGuessesFromUser);
                if (NumberOfGuesses < 4 || NumberOfGuesses > 10)
                {
                    throw new InvalidOperationException("Number of turns needs to be between 4 to 10.");
                }
            }
            catch (FormatException)
            {
                throw new InvalidOperationException("Number of turns needs to be a number.");
            }

            GenerateRandomChoices();
            InitializeHistories();
        }

        public GameResult RunTurn(Color[] i_Colors)
        {
            char[] i_Letters = i_Colors.Select(c => ColorToGameLetterMap[c]).ToArray();
            return RunTurn(new string(i_Letters));
        }

        public GameResult RunTurn(string i_Turn)
        {
            char[] i_Letters = CastTurnToGameLetters(i_Turn);
            ValidateTurn(i_Letters);
            int[] i_TurnResult = DoTurn(i_Letters);

            UserInputHistory[NumberOfTurns] = new string(i_Letters).Replace(" ", "");
            for (int i = 0; i < NumberOfLetters; i++)
            {
                ResultsHistory[NumberOfTurns, i] = i_TurnResult[i];
            }

            NumberOfTurns++;

            return new GameResult(NumberOfGuesses, NumberOfTurns, i_TurnResult);
        }

        private void GenerateRandomChoices()
        {
            for (int i = 0; i < NumberOfLetters; i++)
            {
                int i_RandomNumber = m_Random.Next(0, 7);
                while (m_RandomChoice.Contains((char)(65 + i_RandomNumber)))
                {
                    i_RandomNumber = m_Random.Next(0, 7);
                }
                m_RandomChoice[i] = (char)(65 + i_RandomNumber);
            }
        }
        private void InitializeHistories()
        {
            UserInputHistory = new string[NumberOfGuesses];
            ResultsHistory = new int[NumberOfGuesses, NumberOfLetters];
        }

        private char[] CastTurnToGameLetters(string i_Turn)
        {
            char[] i_CharArray = i_Turn.Replace(" ", "").ToCharArray();
            return i_CharArray;
        }

        private void ValidateTurn(char[] i_CharArray)
        {
            if (i_CharArray.Length != NumberOfLetters)
            {
                throw new InvalidOperationException("The input isn't 4 letters.");
            }

            foreach (char i_Letter in i_CharArray)
            {
                if (!IsCharAGameLetter(i_Letter))
                {
                    throw new InvalidOperationException("The input isn't a game letter.");
                }
            }
        }

        private bool IsCharAGameLetter(char i_Letter)
        {
            int i_NumericRepresentation = (int)i_Letter;
            return i_NumericRepresentation >= 65 && i_NumericRepresentation <= 72;
        }

        private int[] DoTurn(char[] i_Letters)
        {
            int[] i_Result = new int[NumberOfLetters];
            for (int i = 0; i < NumberOfLetters; i++)
            {
                if (m_RandomChoice[i] == i_Letters[i])
                {
                    i_Result[i] = (int)eResultPossibility.Bool;
                }
                else
                {
                    i_Result[i] = m_RandomChoice.Contains(i_Letters[i]) ? (int)eResultPossibility.Hit : (int)eResultPossibility.Nothing;
                }
            }

            return i_Result;
        }

        public string GetCorrectSequence()
        {
            return new string(m_RandomChoice);
        }
    }
}
