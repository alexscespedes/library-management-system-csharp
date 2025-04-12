namespace LibraryManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            // string connectionString = "Data Source=library.db";
            // DatabaseInitializer.Initialize(connectionString);

            while (true)
            {
                Console.WriteLine("==== Library Management System ====");
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
                        Console.WriteLine("Hello case 1");
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Option not implemented yet.");
                        break;
                }
            }
        }

        static void AddNewBook() {
            
        }
    }
}