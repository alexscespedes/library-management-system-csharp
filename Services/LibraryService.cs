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
    }
}