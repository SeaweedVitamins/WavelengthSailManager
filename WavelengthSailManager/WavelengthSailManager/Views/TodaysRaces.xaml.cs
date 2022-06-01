using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WavelengthSailManager.Models;
using Xamarin.Forms;

namespace WavelengthSailManager
{
    public partial class TodaysRaces : ContentPage
    {
        public TodaysRaces()
        {
            InitializeComponent();
        }

        private void NavigateToRaceDetails(object sender, EventArgs e)
        {
            Race selectedRace = ((Button)sender).BindingContext as Race;
            App.Current.MainPage = new RaceDetailsView(selectedRace);
        }
    }
}
