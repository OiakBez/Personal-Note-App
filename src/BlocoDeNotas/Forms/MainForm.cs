namespace Personal_Note_App.Forms;

public class Mainform : Form
{
    public Mainform()
    {
        Text = "Personal Note App";

        Width = 1000;
        Height = 650;

        StartPosition = FormStartPosition.CenterScreen;

        MinimumSize = new Size(800, 500);
    }
}