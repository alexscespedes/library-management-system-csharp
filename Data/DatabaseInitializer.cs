using Microsoft.Data.Sqlite;

namespace LibraryManagement
{
    public class DatabaseInitializer {
        public static void Initialize(string connectionString) {
            using var connection = new SqliteConnection(connectionString);
                connection.Open();

                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Book (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT NOT NULL,
                    Author TEXT NOT NULL,
                    YearPublished INTEGER NOT NULL,
                    IsAvailable BOOLEAN NOT NULL
                    );
                ";
                tableCmd.ExecuteNonQuery();
                
                Console.WriteLine("Database and Book table created");

        }
    }
}