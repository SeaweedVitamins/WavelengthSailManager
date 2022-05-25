using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using WavelengthSailManager.Models;
using WavelengthSailManager.Utils;
using System.Linq;

namespace WavelengthSailManager
{
    public class DatabaseInterface
    {
        static SQLiteAsyncConnection Database;

        public DatabaseInterface()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public static readonly AsyncLazy<DatabaseInterface> Instance = new AsyncLazy<DatabaseInterface>(async () =>
        {
            var instance = new DatabaseInterface();
            await Database.CreateTableAsync<Boat>();
            await Database.CreateTableAsync<Category>();
            await Database.CreateTableAsync<Configuration>();
            await Database.CreateTableAsync<PY>();
            await Database.CreateTableAsync<RaceDetails>();
            await Database.CreateTableAsync<Race>();
            await Database.CreateTableAsync<RaceResult>();
            await Database.CreateTableAsync<Sailor>();
            await Database.CreateTableAsync<SpecialValues>();
            DataPreloader preloader = new DataPreloader();
            //preloader.LoadPYHandicaps();
            //preloader.LoadRaceExample();
            //preloader.LoadSailorsExample();
            //preloader.LoadBoatsExample();
            //preloader.LoadSpecialValues();
            return instance;
        });

        public Task<int> SavePYAsync(PY item)
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

        public Task<int> SaveRaceAsync(Race item)
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

        public Task<int> SaveSailorAsync(Sailor item)
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

        public Task<int> SaveBoatAsync(Boat item)
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

        public Task<int> SaveSpecialAsync(SpecialValues item)
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

        public Task<List<Race>> GetTodaysRacesAsync()
        {
            return Database.Table<Race>()
                            .ToListAsync();
        }

        public Task<List<Sailor>> GetSailorsAsync()
        {
            return Database.Table<Sailor>()
                            .ToListAsync();
        }

        public Task<List<PY>> GetPYsAsync()
        {
            return Database.Table<PY>()
                            .ToListAsync();
        }

        public Task<List<SpecialValues>> GetSpecialAsync()
        {
            return Database.Table<SpecialValues>()
                            .ToListAsync();
        }

        public Task<List<Boat>> GetBoatsAsync()
        {
            return Database.Table<Boat>()
                            .ToListAsync();
        }

        public Task<List<BoatSailor>> GetBoatListAsync()
        {
            return Database.QueryAsync<BoatSailor>("SELECT Boat.ID, Boat.Sail_Number, " +
                "Boat.Class_Name, Sailor.Sailor_Name FROM [Boat] INNER JOIN [Sailor] " +
                "ON Boat.Sailor_ID = Sailor.ID");
        }

        public Task<List<Boat>> GetTimingListAsync(List<int> competingBoatList)
        {
            return Database.Table<Boat>()
                    .Where(t => competingBoatList.Contains(t.ID))
                    .ToListAsync();
        }
    }
}
