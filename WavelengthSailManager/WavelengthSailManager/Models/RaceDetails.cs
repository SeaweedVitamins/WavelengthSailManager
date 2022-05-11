using System;
using SQLite;

namespace WavelengthSailManager.Models
{
    public class RaceDetails
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int Windspeed { get; set; }
        public string Temperature { get; set; }
        public string Additional_Weather_Details { get; set; }
        public string Personel_List { get; set; }
    }
}
