using GameLogic;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace UI
{
    public class GameUI : Form
    {
        private Game m_Game;
        private readonly int maxGuesses = 10;
        private Label correctSequenceLabel;
        private readonly Button[] correctSequenceSquares = new Button[4];
        private int currentRowIndex = 0;


        private readonly List<Button> arrowButtons = new List<Button>();

        public GameUI(int i_maxGuesses)
        {
            this.maxGuesses = i_maxGuesses;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            InitializeGame();
            InitializeCorrectSequenceLabel();
            InitializeRows();
            InitializeCorrectSequenceSquares();
            EnableDisableRows();
        }

        private void InitializeGame()
        {
            m_Game = new Game(maxGuesses.ToString());
        }

        private void InitializeCorrectSequenceLabel()
        {
            correctSequenceLabel = new Label
            {
                Text = "Bool Pgia",
                Location = new Point(250, 30),
                Visible = false
            };
            this.Controls.Add(correctSequenceLabel);
        }

        private void InitializeRows()
        {
            for (int i = 0; i < maxGuesses; i++)
            {
                CreateRow(i);
            }
        }

        private void InitializeCorrectSequenceSquares()
        {
            for (int i = 0; i < 4; i++)
            {
                Button correctSequenceSquare = new Button
                {
                    Size = new Size(40, 40),
                    Location = new Point(40 + i * 45, 10),
                    BackColor = Color.Black,
                    Enabled = false
                };
                this.Controls.Add(correctSequenceSquare);
                correctSequenceSquares[i] = correctSequenceSquare;
            }
        }

        private void EnableDisableRows()
        {
            for (int i = 0; i < arrowButtons.Count; i++)
            {
                Button arrowButton = arrowButtons[i];
                object buttonTag = arrowButton.Tag;
                Button[] guessButtons = buttonTag.GetType().GetProperty("GuessButtons").GetValue(buttonTag, null) as Button[];

                if (i == currentRowIndex)
                {
                    foreach (Button guessButton in guessButtons)
                    {
                        guessButton.Enabled = true;
                    }
                }
                else
                {
                    foreach (Button guessButton in guessButtons)
                    {
                        guessButton.Enabled = false;
                    }
                }
            }
        }

        private void CreateRow(int i_rowIndex)
        {
            Button arrowButton = CreateArrowButton(i_rowIndex);
            Button[] guessButtons = CreateGuessButtons(i_rowIndex, arrowButton);
            Button[] resultButtons = CreateResultButtons(i_rowIndex);

            arrowButton.Tag = new { GuessButtons = guessButtons, ResultButtons = resultButtons };
            arrowButton.Click += new EventHandler(this.ArrowButton_Click);
            this.Controls.Add(arrowButton);
            arrowButtons.Add(arrowButton);
        }

        private Button CreateArrowButton(int i_rowIndex)
        {
            Button o_arrowButton = new Button
            {
                Text = "-->>",
                Size = new Size(40, 20),
                Enabled = false,
                Location = new Point(220, 60 + 55 * i_rowIndex + 20)
            };
            this.Controls.Add(o_arrowButton);
            return o_arrowButton;
        }

        private Button[] CreateGuessButtons(int i_rowIndex, Button i_arrowButton)
        {
            Button[] o_guessButtons = new Button[4];
            for (int i = 0; i < 4; i++)
            {
                Button guessButton = new Button
                {
                    Size = new Size(40, 40),
                    Location = new Point(40 + i * 45, 50 + 55 * i_rowIndex + 20),
                    BackColor = Color.Empty
                };
                guessButton.Click += (sender, e) => { OpenColorDialog(guessButton, i_arrowButton, o_guessButtons); };
                o_guessButtons[i] = guessButton;
                this.Controls.Add(guessButton);
            }
            return o_guessButtons;
        }

        private Button[] CreateResultButtons(int i_rowIndex)
        {
            Button[] o_resultButtons = new Button[4];
            for (int i = 0; i < 4; i++)
            {
                Button resultButton = new Button();
                resultButton.Size = new Size(15, 15);
                int rowOffset = i < 2 ? 0 : 1;
                int colOffset = i % 2;
                int x = 270 + colOffset * (15 + 5);
                int y = 50 + rowOffset * (15 + 5) + 55 * i_rowIndex + 20;
                resultButton.Location = new Point(x, y);
                resultButton.Enabled = false;
                o_resultButtons[i] = resultButton;
                this.Controls.Add(resultButton);
            }
            return o_resultButtons;
        }

        private void OpenColorDialog(Button i_GuessButton, Button i_ArrowButton, Button[] i_GuessButtons)
        {
            ColorChooser colorDialog = new ColorChooser();
            colorDialog.StartPosition = FormStartPosition.CenterScreen;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                i_GuessButton.BackColor = colorDialog.SelectedColor;
                CheckEnableArrowButton(i_ArrowButton, i_GuessButtons);
            }
        }

        private void CheckEnableArrowButton(Button i_ArrowButton, Button[] i_GuessButtons)
        {
            int distinctColorsCount = i_GuessButtons.Select(btn => btn.BackColor).Distinct().Count();

            int filledColorsCount = i_GuessButtons.Count(btn => btn.BackColor != SystemColors.Control);

            if (filledColorsCount == 4 && distinctColorsCount == 4)
            {
                correctSequenceLabel.Text = "Correct Sequence: " + string.Join(", ", distinctColorsCount);
                i_ArrowButton.Enabled = true;
            }
            else
            {
                i_ArrowButton.Enabled = false;
            }
        }
        private void ArrowButton_Click(object i_Sender, EventArgs i_E)
        {
            Button arrowButton = i_Sender as Button;

            Button[] guessButtons = GetGuessButtonsFromTag(arrowButton.Tag);
            Button[] resultButtons = GetResultButtonsFromTag(arrowButton.Tag);

            Color[] colors = GetColorsFromGuessButtons(guessButtons);

            Game.GameResult gameResult = m_Game.RunTurn(colors);

            UpdateResultButtons(resultButtons, gameResult);

            arrowButton.Enabled = false;

            HandleGameStatus(gameResult);
        }

        private Button[] GetGuessButtonsFromTag(object i_buttonTag)
        {
            return i_buttonTag.GetType().GetProperty("GuessButtons").GetValue(i_buttonTag, null) as Button[];
        }

        private Button[] GetResultButtonsFromTag(object i_buttonTag)
        {
            return i_buttonTag.GetType().GetProperty("ResultButtons").GetValue(i_buttonTag, null) as Button[];
        }

        private Color[] GetColorsFromGuessButtons(Button[] i_guessButtons)
        {
            return i_guessButtons.Select(button => button.BackColor).ToArray();
        }

        private void UpdateResultButtons(Button[] i_resultButtons, Game.GameResult i_gameResult)
        {
            for (int i = 0; i < i_resultButtons.Length; i++)
            {
                switch (i_gameResult.TurnResult[i])
                {
                    case (int)eResultPossibility.Bool:
                        i_resultButtons[i].BackColor = Color.Yellow;
                        break;
                    case (int)eResultPossibility.Hit:
                        i_resultButtons[i].BackColor = Color.Black;
                        break;
                    default:
                        i_resultButtons[i].BackColor = Color.Gray;
                        break;
                }
            }
        }

        private void HandleGameStatus(Game.GameResult i_gameResult)
        {
            if (i_gameResult.IsGameOver)
            {
                EndGame();
            }
            else
            {
                currentRowIndex++;
                EnableDisableRows();
            }
        }

        private void EndGame()
        {
            foreach (Control control in this.Controls)
            {
                if (control is Button button && button.Text != "Restart")
                {
                    button.Enabled = false;
                }
            }
            string correctSequence = m_Game.GetCorrectSequence();
            for (int i = 0; i < correctSequence.Length; i++)
            {
                Color color = Game.ColorToGameLetterMap.FirstOrDefault(x => x.Value == correctSequence[i]).Key;
                correctSequenceSquares[i].BackColor = color;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GameUI
            // 
            this.ClientSize = new System.Drawing.Size(946, 644);
            this.Name = "GameUI";
            this.ResumeLayout(false);

        }
    }
}