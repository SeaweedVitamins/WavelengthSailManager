using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WavelengthSailManager.Models
{
    public class Timing : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public int Sail_Number { get; set; }
        public string Class_Name { get; set; }
        public List<int> Lap_Time_List { get; set; }
        public int Finish_Time { get; set; }
        public string Special_Classification_Assigned { get; set; }
        public List<string> Special_List { get; set; }
        public int Corrected_Time { get; set; }
        public string numberOfLaps;

        public event PropertyChangedEventHandler PropertyChanged;

        public string NumberOfLaps
        {
            set
            {
                if (numberOfLaps != value)
                {
                    numberOfLaps = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("NumberOfLaps"));
                    }
                }
            }
            get
            {
                return numberOfLaps;
            }
        }

        public override string ToString()
        {
            return Class_Name + " - " + Sail_Number;
        }
    }
}
