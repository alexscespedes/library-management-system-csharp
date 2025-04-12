namespace LibraryManagement
{
    public class LibraryRepository {
        private List<Book> listBooks = new List<Book>();

        public bool AddBook(string title, string author, int yearPublished) 
        {
            var book = new Book {
                Title = title,
                Author = author,
                YearPublished = yearPublished
            };

            return true;
        }

    }
}