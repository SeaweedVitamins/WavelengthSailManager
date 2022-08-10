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
            // Get selected race from button context
            Race selectedRace = ((Button)sender).BindingContext as Race;

            // Switch page to race details view
            App.Current.MainPage = new RaceDetailsView(selectedRace);
        }
    }
}
