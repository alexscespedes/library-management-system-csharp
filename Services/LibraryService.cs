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
    }
}