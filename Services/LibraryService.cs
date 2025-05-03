namespace LibraryManagement
{
    public class LibraryService {
        private readonly LibraryRepository _repository;

        // Constructor to initialize the repository
        public LibraryService(LibraryRepository repository) {
            _repository = repository;
        }

        // Method to add a new book with validation logic
        public void AddBook(Book book) 
        {
            // Validation logic (e.g., check if year is valid)
            if (string.IsNullOrEmpty(book.Title) || string.IsNullOrEmpty(book.Author))
            {
                Console.WriteLine("Error: Title and Author are required.");
                return;
            }

            if (book.YearPublished <= 0)
            {
                Console.WriteLine("Error: Invalid Year Published.");
                return;
            }

            // Add book to the database through the repository
            _repository.AddBook(book);

            Console.WriteLine($"Book '{book.Title}' by {book.Author} added successfully.");
        }

        public void ViewAllBooks() 
        {
            var books = _repository.GetAllBooks();

            if (books.Count == 0)
            {
                Console.WriteLine("No books found.");
                return;
            }

            Console.WriteLine("\n--- Library Catalog ---");
            foreach (var book in books)
            {
                string status = book.IsAvailable ? "Available" : "Checked Out";
                Console.WriteLine($"ID: {book.Id} | Title: {book.Title} | Author: {book.Author} | Year: {book.YearPublished} | Status: {status}");
            }
            Console.WriteLine();
        }

        public void SearchBooks(string searchTerm) {
            var books = _repository.SearchBooks(searchTerm);

            if (books.Count == 0)
            {
                Console.WriteLine($"No books found matching '{searchTerm}");
                return;
            }

            Console.WriteLine($"\n-- Search Results for '{searchTerm}' ---");
            foreach (var book in books)
            {
                string status = book.IsAvailable ? "Available" : "Checked Out";
                Console.WriteLine($"ID: {book.Id} | Title: {book.Title} | Author: {book.Author} | Year: {book.YearPublished} | Status: {status}");
            }
            Console.WriteLine();
        }

        public void UpdateBook(Book updatedBook) {
            if (string.IsNullOrWhiteSpace(updatedBook.Title) || string.IsNullOrWhiteSpace(updatedBook.Author))
            {
                Console.WriteLine("Error: Title and Author cannot be empty.");
                return;
            }

            if (updatedBook.YearPublished <= 0)
            {
                Console.WriteLine("Error: Invalid Year Published.");
                return;
            }

            bool success =_repository.UpdateBook(updatedBook);

            if (success)
            {
                Console.WriteLine($"Book with ID {updatedBook.Id} updated successfully.");
            }
            else {
                Console.WriteLine($"No book found with ID {updatedBook.Id}.");
            }
        }

        public void DeleteBook(int id) {
            bool success = _repository.DeleteBookById(id);

            if (success)
            {
                Console.WriteLine($"Book with ID {id} deleted successfully.");
            }
            else {
                Console.WriteLine($"No book found with ID {id}.");
            }
        }

        public void CheckoutBook(int id) {
            bool success = _repository.CheckoutBook(id);

            if (success)
            {
                Console.WriteLine($"Book with ID {id} checkout out successfully.");
            }
            else 
            {
                Console.WriteLine($"Book with ID {id} is not available for checkout or does not exist.");
            }
        }

        public void ReturnBook(int id) {
            bool success = _repository.ReturnBook(id);

            if (success)
            {
                Console.WriteLine($"Book with ID {id} returned successfully.");
            }
            else 
            {
                Console.WriteLine($"Book with ID {id} is not available for checked out or does not exist.");
            }
        }

        // Helper Method

        public Book? GetBookById(int id) {
            return _repository.GetBookById(id);
        }
    }
}