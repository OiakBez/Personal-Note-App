using Personal_Note_App.Forms;

namespace Personal_Note_App;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new Mainform());
    }
}