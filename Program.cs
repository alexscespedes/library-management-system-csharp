namespace LibraryManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=library.db";
            DatabaseInitializer.Initialize(connectionString);
        }
    }
}