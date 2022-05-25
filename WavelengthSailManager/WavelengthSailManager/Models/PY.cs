using System;
using SQLite;

namespace WavelengthSailManager.Models
{
    public class PY
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Class_Name { get; set; }
        public int Value { get; set; }
    }
}
