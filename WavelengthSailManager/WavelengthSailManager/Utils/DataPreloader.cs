using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using WavelengthSailManager.Models;

namespace WavelengthSailManager.Utils
{
    public class DataPreloader
    {
        public DataPreloader()
        {
        }

        public async void LoadPYHandicaps()
        {
            // Instantiate DB
            DatabaseInterface @interface = await DatabaseInterface.Instance;

            // Get resource from assembly
            var assembly = Assembly.GetExecutingAssembly();
            var resource = "WavelengthSailManager.Resources.PYList.csv";

            // Read file as csv
            using (Stream stream = assembly.GetManifestResourceStream(resource))
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    // Create a new object for each line
                    var py = new PY();

                    // Set the values to the object
                    py.Class_Name = values[0];
                    py.Value = Convert.ToInt16(values[1]);

                    // Write to the DB
                    await @interface.SavePYAsync(py);
                }
            }
        }

        public async void LoadRaces()
        {
            // Get database interface
            DatabaseInterface @interface = await DatabaseInterface.Instance;

            // Retrieve Resource
            var assembly = Assembly.GetExecutingAssembly();
            var resource = "WavelengthSailManager.Resources.Race.csv";

            // Read csv
            using (Stream stream = assembly.GetManifestResourceStream(resource))
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    // Convert values and add to model
                    var race = new Race();
                    race.Race_Number = Convert.ToInt16(values[0]);
                    race.Series_ID = Convert.ToInt16(values[1]);
                    race.Category_ID = Convert.ToInt16(values[2]);
                    race.DateTime = Convert.ToDateTime(values[3]);

                    // Upsert to db
                    await @interface.SaveRaceAsync(race);
                }
            }
        }

        public async void LoadConfiguration()
        {
            string password = "Dundrum2012";
            // Get database interface
            DatabaseInterface @interface = await DatabaseInterface.Instance;

            // Create salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);

            // Create Hash
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            // Get has to save
            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            // Save config to db
            var configuration = new Configuration();
            configuration.Name = "Admin_Password";
            configuration.Value = savedPasswordHash;

            await @interface.SaveConfiguration(configuration);
        }

        /*
         * These functions import a csv list from assembley and parse it
         * into models. These models are then added to the database.
         * The first function has been commented as an example.
         */

        public async void LoadSailorsExample()
        {
            // Instantiate database interface
            DatabaseInterface @interface = await DatabaseInterface.Instance;
            var assembly = Assembly.GetExecutingAssembly();

            // Find resource
            var resource = "WavelengthSailManager.Resources.SailorNames.csv";

            using (Stream stream = assembly.GetManifestResourceStream(resource))
            using (StreamReader reader = new StreamReader(stream))
            {
                // Untill the end of file
                while (!reader.EndOfStream)
                {
                    // Parse line and add to model
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var sailor = new Sailor();
                    sailor.Sailor_Name = values[0];

                    // Save model to database
                    await @interface.SaveSailorAsync(sailor);
                }
            }
        }

        public async void LoadBoatsExample()
        {
            DatabaseInterface @interface = await DatabaseInterface.Instance;
            var assembly = Assembly.GetExecutingAssembly();
            var resource = "WavelengthSailManager.Resources.BoatList.csv";

            using (Stream stream = assembly.GetManifestResourceStream(resource))
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var boat = new Boat();
                    boat.Sail_Number = Convert.ToInt32(values[0]);
                    boat.Class_Name = values[1];
                    boat.Sailor_ID = Convert.ToInt16(values[2]);

                    await @interface.SaveBoatAsync(boat);
                }
            }
        }

        public async void LoadSpecialValues()
        {
            DatabaseInterface @interface = await DatabaseInterface.Instance;
            var assembly = Assembly.GetExecutingAssembly();
            var resource = "WavelengthSailManager.Resources.Special.csv";

            using (Stream stream = assembly.GetManifestResourceStream(resource))
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var special = new SpecialValues();
                    special.Name = values[0];
                    special.Value = Convert.ToInt16(values[1]);

                    await @interface.SaveSpecialAsync(special);
                }
            }
        }

        public async void LoadCategoryValues()
        {
            DatabaseInterface @interface = await DatabaseInterface.Instance;
            var assembly = Assembly.GetExecutingAssembly();
            var resource = "WavelengthSailManager.Resources.CategoryList.csv";

            using (Stream stream = assembly.GetManifestResourceStream(resource))
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var category = new Category();
                    category.Category_Name = values[0];

                    await @interface.SaveCategoryAsync(category);
                }
            }
        }

        public async void LoadSeriesValues()
        {
            DatabaseInterface @interface = await DatabaseInterface.Instance;
            var assembly = Assembly.GetExecutingAssembly();
            var resource = "WavelengthSailManager.Resources.SeriesList.csv";

            using (Stream stream = assembly.GetManifestResourceStream(resource))
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var series = new Series();
                    series.Series_Name = values[0];

                    await @interface.SaveSeriesAsync(series);
                }
            }
        }
    }
}
