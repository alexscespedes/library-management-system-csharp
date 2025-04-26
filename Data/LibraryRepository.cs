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

        public List<Book> GetAllBooks() 
        {
            var books = new List<Book>();

            using var connection = new SqliteConnection(DbConfig.ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Books";

            using var reader = command.ExecuteReader();
            while (reader.Read()) 
            {
                var book = new Book 
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Author = reader.GetString(2),
                    YearPublished = reader.GetInt32(3),
                    IsAvailable = reader.GetInt32(4) == 1
                };

                books.Add(book);
            }
            return books;
        }

        public Book? GetBookById(int id) {
            using var connection = new SqliteConnection(DbConfig.ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Books WHERE Id = $id";
            command.Parameters.AddWithValue("$id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Book {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Author = reader.GetString(2),
                    YearPublished = reader.GetInt32(3),
                    IsAvailable = reader.GetInt32(4) == 1
                };
            }
            return null;
        }

        public List<Book> SearchBooks (string searchTerm) {
            var books = new List<Book>();

            using var connection = new SqliteConnection(DbConfig.ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
            SELECT * FROM Books
            WHERE Title LIKE $searchTerm OR Author LIKE $searchTerm
            ";

            command.Parameters.AddWithValue("$searchTerm", $"%{searchTerm}%");

            using var reader = command.ExecuteReader();
            while (reader.Read()) {
                var book = new Book {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Author = reader.GetString(2),
                    YearPublished = reader.GetInt32(3),
                    IsAvailable = reader.GetInt32(4) == 1
                };

                books.Add(book);
            }

            return books;
        }

        public bool UpdateBook(Book book) {
            using var connection = new SqliteConnection(DbConfig.ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE Books
                SET Title = $title,
                    Author = $author,
                    YearPublished = $yearPublished
                WHERE Id = $id;
            ";

            command.Parameters.AddWithValue("$title", book.Title);
            command.Parameters.AddWithValue("$author", book.Author);
            command.Parameters.AddWithValue("$yearPublished", book.YearPublished);
            command.Parameters.AddWithValue("$id", book.Id);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;

        }

        public bool DeleteBookById(int id) {
            using var connection = new SqliteConnection(DbConfig.ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Books WHERE Id = $id";
            command.Parameters.AddWithValue("$id", id);
            
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool CheckoutBook(int id) {
            using var connection = new SqliteConnection(DbConfig.ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
            UPDATE Books
            SET IsAvailable = 0
            WHERE Id = $id AND IsAvailable = 1;
            ";
            command.Parameters.AddWithValue("$id", id);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool ReturnBook(int id) {
            using var connection = new SqliteConnection(DbConfig.ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
            UPDATE Books
            SET IsAvailable = 1
            WHERE Id = $id AND IsAvailable = 0;
            ";
            command.Parameters.AddWithValue("$id", id);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
}