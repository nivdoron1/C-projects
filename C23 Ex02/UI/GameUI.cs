using System;
using System.Text;
using Ex02.ConsoleUtils;
using GameLogic;
namespace UI
{
    class GameUI
    {
        private Game m_Game;
        private bool isGameOver = false;
        private string currentUserInput;
        private bool quit = false;

        public GameUI()
        {
            while (!quit)
            {
                try
                {
                    Console.WriteLine("please enter number of guesess: ");
                    this.currentUserInput = Console.ReadLine();
                    m_Game = new Game(currentUserInput);
                    this.isGameOver = false;
                    RunGame();
                    quit = !StartANewGame();
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        private bool StartANewGame()
        {
            while (true) {
                Console.WriteLine("Whould you like to start a new game? (Y/N)");
                string isNewGame = Console.ReadLine();
                if (isNewGame == "N")
                {
                    return false;
                }
                if (isNewGame == "Y")
                {
                    Screen.Clear();
                    return true;
                }
                Console.WriteLine("You entered invalid input.");
            }
        }

        private void RunGame()
        {
            PrintBoard();
            while (!this.isGameOver)
            {
                Console.WriteLine("please type your next guess <A B C D E F G H> or 'Q' to quit");
                try
                {
                    this.currentUserInput = Console.ReadLine();
                    if (this.currentUserInput == "Q")
                    {
                        System.Environment.Exit(0);
                    }
                    GameLogic.Game.GameResult gameResult = m_Game.RunTurn(currentUserInput);
                    this.isGameOver = gameResult.isGameOver;

                    Screen.Clear();
                    PrintBoard();
                    if (this.isGameOver)
                    {
                        if (gameResult.winner)
                        {
                            Console.WriteLine(String.Format("you guessed after {0} steps!", m_Game.NumberOfTurn));
                        }
                        else
                        {
                            Console.WriteLine("You've reached the maximum number of guesses.");
                        }
                    }
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            

        }


        private void PrintBoard()
        {
            Console.WriteLine("Current board status: ");
            Console.WriteLine("|Pins:     |Result:  |");
            Console.WriteLine("|==========|=========|");
            Console.WriteLine("| # # # #  |         |");
            for (int i = 0; i < m_Game.NumberOfTurn; i++)
            {
                char[] historyInChar = m_Game.UserInputHistory[i].ToCharArray();
                Console.WriteLine("|==========|=========|"); 
                Console.WriteLine(String.Format("| {0} {1} {2} {3}  | {4} {5} {6} {7} |",
                historyInChar[0], historyInChar[1], historyInChar[2], historyInChar[3],
                CastToResult(m_Game.ResultsHistory[i, 0]), CastToResult(m_Game.ResultsHistory[i, 1]), CastToResult(m_Game.ResultsHistory[i, 2]), CastToResult(m_Game.ResultsHistory[i, 3])));
            }
            for (int i = 0; i < m_Game.numberOfGueses - m_Game.NumberOfTurn; i++)
            {
                Console.WriteLine("|==========|=========|");
                Console.WriteLine("|          |         |");
            }
            Console.WriteLine("|==========|=========|");
        }

        private char CastToResult(int result)
        {
            if (result == (int)eResultPossability.Bool)
            {
                return 'V';
            }
            if (result == (int)eResultPossability.Hit)
            {
                return 'X';
            }
            if (result == (int)eResultPossability.Nothing)
            {
                return ' ';
            }
            return ' ';
        }
    }
}
