using System;
using System.Collections.Generic;
using WavelengthSailManager.Models;
using Xamarin.Forms;

namespace WavelengthSailManager
{
    public partial class RaceDetailsView : ContentPage
    {
        RaceDetails selectedRaceDetails = new RaceDetails();
        Race selectedRace;
        public RaceDetailsView(Race selectedRace)
        {
            this.selectedRace = selectedRace;
            InitializeComponent();
        }

        private void NavigateToBoatSelection(object sender, EventArgs e)
        {
            /*selectedRaceDetails.GeneralWeather = GeneralWeatherInfo.Text;
            selectedRaceDetails.Temperature = Convert.ToDouble(TemperatureInfo.Text);
            selectedRaceDetails.Windspeed = Convert.ToDouble(WindSpeedInfo.Text);
            selectedRaceDetails.WindGusts = Convert.ToDouble(WindGustsInfo.Text);
            selectedRaceDetails.WindDirection = Convert.ToInt16(WindDirectionInfo.Text);
            selectedRaceDetails.Humidity = Convert.ToInt16(HumidityInfo.Text);
            selectedRaceDetails.Pressure = Convert.ToInt16(PressureInfo.Text);
            selectedRaceDetails.Race_Officer = pickerRaceOfficer.Items[pickerRaceOfficer.SelectedIndex];
            selectedRaceDetails.Safety_Helm = pickerSafetyHelm.Items[pickerSafetyHelm.SelectedIndex];
            selectedRaceDetails.Safety_Crew = pickerSafetyCrew.Items[pickerSafetyCrew.SelectedIndex];
            selectedRaceDetails.Personel_List = CollectPersonel();*/

            App.Current.MainPage = new TodaysRaces();
        }

        private List<string> CollectPersonel()
        {
            List<string> collectionList = new List<string>();
            collectionList.Add(pickerAdditional0.Items[pickerAdditional0.SelectedIndex]);
            collectionList.Add(pickerAdditional1.Items[pickerAdditional1.SelectedIndex]);
            collectionList.Add(pickerAdditional2.Items[pickerAdditional2.SelectedIndex]);
            collectionList.Add(pickerAdditional3.Items[pickerAdditional3.SelectedIndex]);
            collectionList.Add(pickerAdditional4.Items[pickerAdditional4.SelectedIndex]);
            collectionList.Add(pickerAdditional5.Items[pickerAdditional5.SelectedIndex]);
            return collectionList;
        }
    }
}
