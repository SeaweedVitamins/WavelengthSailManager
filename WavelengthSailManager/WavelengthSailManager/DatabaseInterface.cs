using System;
using SQLite;

namespace WavelengthSailManager
{
    public class DatabaseInterface
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<DatabaseInterface> Instance = new AsyncLazy<DatabaseInterface>(async () =>
        {
            var instance = new DatabaseInterface();
            //Create the tables here
            return instance;
        });

        public DatabaseInterface()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }
    }
}
