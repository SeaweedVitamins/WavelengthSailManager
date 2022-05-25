using System;
using SQLite;

namespace WavelengthSailManager.Models
{
    public class SpecialValues
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
