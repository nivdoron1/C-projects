using System.Drawing;
using System.Windows.Forms;

public class ColorChooser : Form
{
    public Color SelectedColor { get; private set; }
    private Button[] m_ColorButtons;

    public ColorChooser()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        Color[] i_Colors = { Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Purple, Color.Brown, Color.Pink };
        m_ColorButtons = new Button[8];
        int i_ButtonSize = 40;
        int i_XOffset = 10;
        int i_YOffset = 10;
        this.Text = "Pick A Color:";
        this.StartPosition = FormStartPosition.CenterScreen;

        for (int i_Index = 0; i_Index < 8; i_Index++)
        {
            Button i_ColorButton = new Button
            {
                Size = new Size(i_ButtonSize, i_ButtonSize),
                BackColor = i_Colors[i_Index]
            };

            int i_Row = i_Index / 4;
            int i_Col = i_Index % 4;
            int i_X = i_XOffset + i_Col * (i_ButtonSize + 5);
            int i_Y = i_YOffset + i_Row * (i_ButtonSize + 5);

            i_ColorButton.Location = new Point(i_X, i_Y);
            i_ColorButton.Click += (sender, e) =>
            {
                SelectedColor = i_ColorButton.BackColor;
                this.DialogResult = DialogResult.OK;
                this.Close();
            };

            this.Controls.Add(i_ColorButton);
            m_ColorButtons[i_Index] = i_ColorButton;
        }

        this.Size = new Size(i_XOffset * 2 + 4 * (i_ButtonSize + 5), i_YOffset * 2 + 2 * (i_ButtonSize + 5));
    }
}
