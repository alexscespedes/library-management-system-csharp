namespace LibraryManagement
{
    public static class DbConfig {
        public static readonly string DatabaseFileName = "library.db";
        public static readonly string FullPath = Path.Combine(AppContext.BaseDirectory, DatabaseFileName);
        public static readonly string ConnectionString = $"Data Source={FullPath}";
    }
}