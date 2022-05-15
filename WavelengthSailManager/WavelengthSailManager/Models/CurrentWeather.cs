using System;
namespace WavelengthSailManager.Models
{
    public class CurrentWeather
    {
        public string Main { get; set; }
        public string MainIconCode { get; set; }
        public string Temperature { get; set; }
        public string Pressure { get; set; }
        public string Humidity { get; set; }
        public string WindSpeed { get; set; }
        public string WindGusts { get; set; }
        public string Direction { get; set; }
    }
}
