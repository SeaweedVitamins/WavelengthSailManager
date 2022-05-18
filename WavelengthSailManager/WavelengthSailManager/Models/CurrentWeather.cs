using System;
namespace WavelengthSailManager.Models
{
    public class CurrentWeather
    {
        public NestedWeatherBase weather { get; set; }
        public NestedWeatherMain main { get; set; }
        public NestedWeatherWind wind { get; set; }
    }
}
