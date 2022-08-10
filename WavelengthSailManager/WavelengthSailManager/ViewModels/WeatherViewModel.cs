using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using WavelengthSailManager.Models;
using Newtonsoft.Json;

namespace WavelengthSailManager.ViewModels
{
    class WeatherViewModel : INotifyPropertyChanged
    {
        CurrentWeather weather { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public WeatherViewModel()
        {
            Task.Run(async () =>
            {
                // Create rest service
                RestService restService = new RestService();
                this.Weather = await restService.RefreshDataAsync();
            });
        }

        // Getter and setter for weather
        public CurrentWeather Weather
        {
            set
            {
                if (weather != value)
                {
                    weather = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Weather"));
                    }
                }
            }
            get
            {
                return weather;
            }
        }
    }

    public class RestService
    {
        HttpClient client;

        public CurrentWeather Weather { get; private set; }

        public RestService()
        {
            client = new HttpClient();
        }

        public async Task<CurrentWeather> RefreshDataAsync()
        {
            // Setting Uri
            Uri uri = new Uri(string.Format(
                "https://api.openweathermap.org/data/2.5/weather?lat=54.218&lon=-5.8898&appid=cdf654e6fe0368fd8956aafec8f15707&units=imperial",
                string.Empty));
            try
            {
                // Call api async
                HttpResponseMessage response = await client.GetAsync(uri);

                // If api response is valid
                if (response.IsSuccessStatusCode)
                {
                    //Initialise the json serialiser
                    var jsonSerializerSettings = new JsonSerializerSettings();

                    // This is needed as the api response contains more elements than are in the model
                    jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;

                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    // Deserialise content into weather
                    CurrentWeather deserialized = JsonConvert.DeserializeObject<CurrentWeather>(jsonResponse, jsonSerializerSettings);

                    // Sets to the display model
                    this.Weather = deserialized;
                }
            }
            catch (Exception ex)
            {
                // Write error to log
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Weather;
        }
    }
}
