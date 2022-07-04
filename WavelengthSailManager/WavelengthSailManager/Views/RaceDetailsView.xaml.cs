using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            var valid = true;
            if (pickerRaceOfficer.SelectedIndex == -1) { valid = false; DisplayAlert("Error", "Please set the Race Officer", "OK"); return; }
            if (pickerSafetyHelm.SelectedIndex == -1) { valid = false; DisplayAlert("Error", "Please set the Safety Helm", "OK"); return; }
            if (pickerSafetyCrew.SelectedIndex == -1) { valid = false; DisplayAlert("Error", "Please set the Safety Crew", "OK"); return; }
            if (!(conditionsSame.IsChecked || conditionsDifferent.IsChecked)) { valid = false; DisplayAlert("Error", "Select a weather option", "OK"); return; }

            selectedRaceDetails.GeneralWeather = GeneralWeatherInfo.Text;
            selectedRaceDetails.Temperature = Convert.ToDouble(TemperatureInfo.Text);
            selectedRaceDetails.Windspeed = Convert.ToDouble(WindSpeedInfo.Text);
            selectedRaceDetails.WindGusts = Convert.ToDouble(WindGustsInfo.Text);
            selectedRaceDetails.WindDirection = Convert.ToInt16(WindDirectionInfo.Text);
            selectedRaceDetails.Humidity = Convert.ToInt16(HumidityInfo.Text);
            selectedRaceDetails.Pressure = Convert.ToInt16(PressureInfo.Text);
            selectedRaceDetails.Race_Officer = pickerRaceOfficer.Items[pickerRaceOfficer.SelectedIndex];
            selectedRaceDetails.Safety_Helm = pickerSafetyHelm.Items[pickerSafetyHelm.SelectedIndex];
            selectedRaceDetails.Safety_Crew = pickerSafetyCrew.Items[pickerSafetyCrew.SelectedIndex];
            selectedRaceDetails.Personel_List = CollectPersonel();

            Task.Run(async () =>
            {
                DatabaseInterface @interface = await DatabaseInterface.Instance;
                await @interface.SaveRaceDetailsAsync(selectedRaceDetails);
                selectedRace.Race_Details_ID = selectedRaceDetails.ID;
                await @interface.SaveRaceAsync(selectedRace);
            });

            App.Current.MainPage = new BoatList(selectedRace);
        }

        private string CollectPersonel()
        {
            string personelCSV = "";
            List<string> collectionList = new List<string>();
            collectionList = getAndAppendPickerValue(collectionList, pickerAdditional0);
            collectionList = getAndAppendPickerValue(collectionList, pickerAdditional1);
            collectionList = getAndAppendPickerValue(collectionList, pickerAdditional2);
            collectionList = getAndAppendPickerValue(collectionList, pickerAdditional3);
            collectionList = getAndAppendPickerValue(collectionList, pickerAdditional4);
            collectionList = getAndAppendPickerValue(collectionList, pickerAdditional5);

            foreach(string x in collectionList)
            {
                personelCSV += (x+",");
            }

            return personelCSV;
        }

        private List<string> getAndAppendPickerValue(List<string> collectionList,Picker picker)
        {
            if(picker.SelectedIndex == -1)
            {
                return collectionList;
            }
            else
            {
                collectionList.Add(picker.Items[picker.SelectedIndex]);
                return collectionList;
            }
        }
    }
}
