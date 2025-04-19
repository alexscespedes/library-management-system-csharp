using Microsoft.Data.Sqlite;

namespace LibraryManagement
{
    public class DatabaseInitializer {
        public static void Initialize() 
        {
            if (!File.Exists(DbConfig.FullPath))
            {
                Console.WriteLine("Creating new library.db...");
            }

            using var connection = new SqliteConnection(DbConfig.ConnectionString);
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Books (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT NOT NULL,
                    Author TEXT NOT NULL,
                    YearPublished INTEGER NOT NULL,
                    IsAvailable BOOLEAN NOT NULL DEFAULT 1 
                    );
                ";
            tableCmd.ExecuteNonQuery();
        }
    }
}