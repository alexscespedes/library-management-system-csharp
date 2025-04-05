namespace LibraryManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            var book = new Book(1, "12 Rules for life", "Jordan Peterson", 2017, true);
            Console.WriteLine(book.Author);
        }
    }
}