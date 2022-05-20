using System;
using System.Collections.Generic;
using SQLite;

namespace WavelengthSailManager.Models
{
    public class RaceDetails
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string GeneralWeather { get; set; }
        public double Windspeed { get; set; }
        public double WindGusts { get; set; }
        public int WindDirection { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }
        public double Temperature { get; set; }
        public string Additional_Weather_Details { get; set; }
        public string Race_Officer { get; set; }
        public string Safety_Helm { get; set; }
        public string Safety_Crew { get; set; }
        //public List<string> Personel_List { get; set; }
    }
}
