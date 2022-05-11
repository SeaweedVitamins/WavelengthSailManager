using System;
using SQLite;

namespace WavelengthSailManager.Models
{
    public class Series
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Series_Name { get; set; }
    }
}
