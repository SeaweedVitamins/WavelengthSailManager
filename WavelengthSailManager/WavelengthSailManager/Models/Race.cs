using System;
using SQLite;

namespace WavelengthSailManager.Models
{
    public class Race
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int Race_Number { get; set; }
        public int Series_ID { get; set; }
        public int Category_ID { get; set; }
        public DateTime DateTime { get; set; }
    }
}
