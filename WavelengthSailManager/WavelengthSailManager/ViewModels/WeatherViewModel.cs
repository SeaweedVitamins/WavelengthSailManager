using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using WavelengthSailManager.Models;
using Xamarin.Forms;

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
                RestService restService = new RestService();
                this.Weather = await restService.RefreshDataAsync();
            });
        }

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
            Weather = new CurrentWeather();

            Uri uri = new Uri(string.Format("https://api.openweathermap.org/data/2.5/weather?lat=54.218&lon=-5.8898&appid=cdf654e6fe0368fd8956aafec8f15707&units=imperial", string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Weather;
        }
    }
}
