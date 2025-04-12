using Microsoft.Data.Sqlite;

namespace LibraryManagement
{
    public class LibraryRepository {
        private readonly string _connectionString;

        public LibraryRepository(string dbFile) {
            _connectionString = $"Data Source={dbFile};";
        }

        public void AddBook(Book book) {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var query = @"INSERT INTO Books (Title, Author, YearPublished, IsAvailable)
                          VALUES (@Title, @Author, @YearPublished, @IsAvailable)";

            using var command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@Title", book.Title);
            command.Parameters.AddWithValue("@Author", book.Author);
            command.Parameters.AddWithValue("@YearPublished", book.YearPublished);
            command.Parameters.AddWithValue("@IsAvailable", book.IsAvailable);

            command.ExecuteNonQuery();
        }

        public Book? GetBookById(int id) {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var query = "SELECT * FROM Books WHERE Id = @Id";
            using var command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Book 
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Author = reader.GetString(2),
                    YearPublished = reader.GetInt32(3),
                    IsAvailable = reader.GetBoolean(4)
                };
            }
            return null;
        }
    }
}