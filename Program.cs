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
    }
}