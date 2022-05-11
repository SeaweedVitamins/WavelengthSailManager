using System;
using SQLite;

namespace WavelengthSailManager.Models
{
    public class Race
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int Race_Number { get; set; }
        public string Series_ID { get; set; }
        public string Category_ID { get; set; }
        public DateTime DateTime { get; set; }

        //Im not sure what this is
        public RaceDetails RaceDetails { get; set; }
    }
}
