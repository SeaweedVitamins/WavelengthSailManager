using System;
using SQLite;

namespace WavelengthSailManager.Models
{
    public class BoatSailor
    {
        public int ID {get; set; }
        public int Sail_Number { get; set; }
        public string Class_Name { get; set; }
        public string Sailor_Name { get; set; }

        public override string ToString()
        {
            return Sail_Number + "\n" + Class_Name + " "+ Sailor_Name;
        }
    }
}
