using System;
using System.Threading.Tasks;
using SQLite;
using WavelengthSailManager.Models;

namespace WavelengthSailManager
{
    public class DatabaseInterface
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<DatabaseInterface> Instance = new AsyncLazy<DatabaseInterface>(async () =>
        {
            var instance = new DatabaseInterface();
            await Database.CreateTableAsync<BoatList>();
            await Database.CreateTableAsync<Category>();
            await Database.CreateTableAsync<Configuration>();
            await Database.CreateTableAsync<PY>();
            await Database.CreateTableAsync<Race>();
            await Database.CreateTableAsync<RaceDetails>();
            await Database.CreateTableAsync<RaceResult>();
            await Database.CreateTableAsync<Sailor>();
            await Database.CreateTableAsync<Sailor>();
            await Database.CreateTableAsync<SpecialValues>();
            return instance;
        });

        public DatabaseInterface()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<int> SaveItemAsync(Race item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }
    }
}
