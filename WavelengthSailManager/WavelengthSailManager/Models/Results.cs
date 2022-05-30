using System;
using SQLite;

namespace WavelengthSailManager.Models
{
    public class Results
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int Race_ID { get; set; }
        public string Place { get; set; }
        public int Boat_ID { get; set; }
        public int Sail_Number { get; set; }
        public string Sailor_Name { get; set; }
        public string Class_Name { get; set; }
        public int Number_Of_Laps { get; set; }
        public string Finish_Time { get; set; }
        public string Corrected_Time { get; set; }
    }
}
