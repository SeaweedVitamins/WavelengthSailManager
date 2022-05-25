using System;
using System.Collections.Generic;

namespace WavelengthSailManager.Models
{
    public class Timing
    {
        public int ID { get; set; }
        public int Sail_Number { get; set; }
        public string Class_Name { get; set; }
        public int Latest_Lap_Time { get; set; }
        public int Finish_Time { get; set; }
        public string Special_Classification_Assigned { get; set; }
        public List<string> Special_List { get; set; }

        public override string ToString()
        {
            return Class_Name + " - " + Sail_Number;
        }
    }
}
