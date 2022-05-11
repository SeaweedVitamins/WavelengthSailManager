using System;
using SQLite;

namespace WavelengthSailManager.Models
{
    public class Sailor
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
