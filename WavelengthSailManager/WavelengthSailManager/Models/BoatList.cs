using System;
using SQLite;

namespace WavelengthSailManager.Models
{
    public class BoatList
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int Sail_Number { get; set; }
        public string Class_Name { get; set; }
        public string Sailor_ID { get; set; }
    }
}
