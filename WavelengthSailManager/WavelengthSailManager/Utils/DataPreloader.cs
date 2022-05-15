using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
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
            DatabaseInterface @interface = await DatabaseInterface.Instance;
            var assembly = Assembly.GetExecutingAssembly();
            var resource = "WavelengthSailManager.Resources.PYList.csv";

            using (Stream stream = assembly.GetManifestResourceStream(resource))
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var py = new PY();
                    py.Class_Name = values[0];
                    py.Value = values[1];
                    
                    await @interface.SavePYAsync(py);
                }
            }
        }

        public async void LoadRaceExample()
        {
            DatabaseInterface @interface = await DatabaseInterface.Instance;
            var assembly = Assembly.GetExecutingAssembly();
            var resource = "WavelengthSailManager.Resources.Race.csv";

            using (Stream stream = assembly.GetManifestResourceStream(resource))
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var race = new Race();
                    race.Race_Number = Convert.ToInt16(values[0]);
                    race.Series_ID = Convert.ToInt16(values[1]);
                    race.Category_ID = Convert.ToInt16(values[2]);
                    race.DateTime = DateTime.Now;

                    await @interface.SaveRaceAsync(race);
                }
            }
        }

        public async void LoadSailorsExample()
        {
            DatabaseInterface @interface = await DatabaseInterface.Instance;
            var assembly = Assembly.GetExecutingAssembly();
            var resource = "WavelengthSailManager.Resources.SailorNames.csv";

            using (Stream stream = assembly.GetManifestResourceStream(resource))
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var sailor = new Sailor();
                    sailor.Name = values[0];

                    await @interface.SaveSailorAsync(sailor);
                }
            }
        }
    }
}
