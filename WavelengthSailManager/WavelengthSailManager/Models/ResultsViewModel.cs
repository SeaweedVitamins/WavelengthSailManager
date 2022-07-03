using System;
using SQLite;

namespace WavelengthSailManager.Models
{
    public class ResultsViewModel
    {
        public int Boat_ID { get; set; }
        public string Position { get; set; }
        public string Sail_Number { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public string Points { get; set; }
    }
}
