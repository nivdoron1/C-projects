using System;
using System.Windows.Forms;
using System.Drawing;

public class InitialSettingForm : Form
{
    private Button startButton;
    private Button guessButton;
    private int currentGuesses;

    public InitialSettingForm()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        startButton = new Button();
        startButton.Text = "Start";
        startButton.Location = new Point(120, 50);
        startButton.Click += StartButton_Click;
        this.Controls.Add(startButton);

        guessButton = new Button();
        currentGuesses = 4;
        guessButton.Text = "Number of Guesses: " + currentGuesses.ToString();
        guessButton.Location = new Point(10, 10);
        guessButton.Size = new Size(200, 30);
        guessButton.Click += GuessButton_Click;
        this.Controls.Add(guessButton);

        this.Text = "Bool Pgia";
        this.StartPosition = FormStartPosition.CenterScreen;
    }

    private void GuessButton_Click(object sender, EventArgs e)
    {
        if (currentGuesses >= 10)
        {
            currentGuesses = 4;
        }
        else
        {
            currentGuesses++;
        }
        guessButton.Text = "Number of Guesses: " + currentGuesses.ToString();
    }

    private void StartButton_Click(object sender, EventArgs e)
    {
        UI.GameUI gameUI = new UI.GameUI(currentGuesses);
        gameUI.Show();
    }
}
