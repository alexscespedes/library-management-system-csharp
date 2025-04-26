namespace LibraryManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseInitializer.Initialize();
            var repository = new LibraryRepository();
            var libraryService = new LibraryService(repository);

            while (true)
            {
                Console.WriteLine("Welcome to the Library Management System!");
                Console.WriteLine("1. Add New Book");
                Console.WriteLine("2. View All Books");
                Console.WriteLine("3. Search Book by Title/Author");
                Console.WriteLine("4. Update Book Details");
                Console.WriteLine("5. Delete Book");
                Console.WriteLine("6. Mark as Borrowed/Returned");
                Console.WriteLine("7. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Add a book");
                        AddNewBook(libraryService);
                        break;
                    case "2":
                        Console.WriteLine("View All Books");
                        libraryService.ViewAllBooks();
                        break;
                    case "3":
                        SearchBooks(libraryService);
                        break;
                    case "4":
                        // Update
                        UpdateBookInfo(libraryService);
                        break;
                    case "5":
                        // Delete
                        DeleteBook(libraryService);
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Option not implemented yet.");
                        break;
                }
            }
        }

        static void AddNewBook(LibraryService libraryService) 
        {
            Console.Write("Enter book title: ");
            string title = Console.ReadLine();

            Console.Write("Enter book author: ");
            string author = Console.ReadLine();

            Console.Write("Enter year published: ");
            int year = int.Parse(Console.ReadLine());

            var newBook = new Book {
                Title = title,
                Author = author,
                YearPublished = year
            };   

            libraryService.AddBook(newBook);
        }

        static void SearchBooks(LibraryService libraryService) {
            Console.Write("Enter title or author to seach:");
            string searchTerm = Console.ReadLine();
            libraryService.SearchBooks(searchTerm);
        }

        static void UpdateBookInfo(LibraryService libraryService) {
            Console.Write("Enter the ID of the book to update: ");
            bool validId = int.TryParse(Console.ReadLine(), out int id);
            if (!validId || id <= 0)
            {
                Console.WriteLine("Invalid ID entered.");
                return;
            }

            var existingBook = libraryService.GetBookById(id);
            if (existingBook == null)
            {
                Console.WriteLine($"No book found with ID {id}.");
            }

            Console.WriteLine($"Current Title: {existingBook.Title}");
            Console.Write("Enter new title (leave blank to keep): ");
            string newTitle = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(newTitle))
                newTitle = existingBook.Title;
            
            Console.WriteLine($"Current Author: {existingBook.Author}");
            Console.Write("Enter new author (leave blank to keep): ");
            string newAuthor = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(newAuthor))
            {
                newAuthor = existingBook.Author;
            }

            Console.WriteLine($"Current Year Published: {existingBook.YearPublished}");
            Console.Write("Enter new year published (leave blank to keep): ");
            string yearInput = Console.ReadLine();
            int newYear;
            if (string.IsNullOrWhiteSpace(yearInput))
                newYear = existingBook.YearPublished;
            else if (!int.TryParse(yearInput, out newYear) || newYear <= 0)
            {
                Console.WriteLine("Invalid year entered");
                return;
            }

            /* Old version (important to keep)
            bool validYear = int.TryParse(Console.ReadLine(), out int newYear);
            if (!validYear || newYear <= 0)
            {
                Console.WriteLine("Invalid year entered.");
                return;
            }
            */

            var updatedBook = new Book {
                Id = id,
                Title = newTitle,
                Author = newAuthor,
                YearPublished = newYear
            };

            libraryService.UpdateBook(updatedBook);
        }

        static void DeleteBook(LibraryService libraryService) {
            Console.Write("Enter the ID of the book to delete: ");
            bool valid = int.TryParse(Console.ReadLine(), out int id);
            if (!valid || id <=0)
            {
                Console.WriteLine("Invalid ID entered.");
                return;
            }

            libraryService.DeleteBook(id);
        }
    }
}