using Microsoft.Data.Sqlite;

namespace LibraryManagement
{
    public class LibraryRepository {
        public void AddBook(Book book) {
            using var connection = new SqliteConnection(DbConfig.ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Books (Title, Author, YearPublished, IsAvailable)
                VALUES ($title, $author, $year, $available);
            ";

            command.Parameters.AddWithValue("$title", book.Title);
            command.Parameters.AddWithValue("$author", book.Author);
            command.Parameters.AddWithValue("$year", book.YearPublished);
            command.Parameters.AddWithValue("$available", book.IsAvailable);

            command.ExecuteNonQuery();
        }

        public Book? GetBookById(int id) {
            using var connection = new SqliteConnection(DbConfig.ConnectionString);
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