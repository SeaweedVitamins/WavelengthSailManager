using System;
using SQLite;

namespace WavelengthSailManager.Models
{
    public class RaceResult
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int Race_ID { get; set; }
        public string Boat_ID { get; set; }
        public double Points { get; set; }
    }
}
