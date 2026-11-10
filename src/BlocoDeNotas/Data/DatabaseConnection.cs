using Microsoft.Data.Sqlite;

namespace Personal_Note_App.Data;

public static class DatabaseConnection
{
    private const string DatabaseFileName = "personal_notes.db";

    private static string DatabaseDirectory =>
        Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData
            ),
            "PersonalNoteApp"
        );
    
    public static string DatabasePath =>
        Path.Combine(DatabaseDirectory, DatabaseFileName);

    public static SqliteConnection CreateConnection()
    {
        Directory.CreateDirectory(DatabaseDirectory);

        var connectionString = new SqliteConnectionStringBuilder
        {
            DataSource = DatabasePath
        }.ToString();

        return new SqliteConnection(connectionString);
    }
}