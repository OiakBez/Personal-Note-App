using Personal_Note_App.Forms;
using Personal_Note_App.Data;

namespace Personal_Note_App;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        DatabaseInitializer.Initialize();
        Application.Run(new Mainform());
    }
}