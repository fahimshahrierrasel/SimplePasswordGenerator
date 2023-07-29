using SQLite;

namespace PasswordGenerator
{
    public static class Constants
    {
        public const string DatabaseFilename = "PasswordHistory.db3";

        public const SQLiteOpenFlags DatabaseFlags =
            // open the database in read/write mode
            SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLiteOpenFlags.SharedCache;

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
    }
}
