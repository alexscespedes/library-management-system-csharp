using Microsoft.Data.Sqlite;

namespace LibraryManagement
{
    public class DatabaseInitializer {

        private static readonly string DatabaseFileName = "library.db";
        private static readonly string FullPath = Path.Combine(AppContext.BaseDirectory, DatabaseFileName);
        private static readonly string ConnectionString = $"Data Source={FullPath}";
        public static void Initialize() {

            // if (!File.Exists(DatabaseFileName))
            // {
            //     Console.WriteLine("Creating new library.db...");
                
            // }
            // Console.WriteLine($"Database path: {Path.Combine(AppContext.BaseDirectory, DatabaseFileName)}");
            
            using var connection = new SqliteConnection(ConnectionString);
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