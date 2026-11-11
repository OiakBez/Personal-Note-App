using Microsoft.Data.Sqlite;

namespace Personal_Note_App.Data;

public static class DatabaseInitializer
{
    public static void Initialize()
    {
        using var connection = DatabaseConnection.CreateConnection();
            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText = """
                CREATE TABLE IF NOT EXISTS Notes (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT NOT NULL,
                    ContentRtf TEXT NOT NULL,
                    CreatedAt TEXT NOT NULL,
                    UpdatedAt TEXT NOT NULL,
                    Category TEXT NOT NULL,
                    IsFavourite INTEGER NOT NULL DEFAULT 0
                    );
                """;

            command.ExecuteNonQuery();

    }
}