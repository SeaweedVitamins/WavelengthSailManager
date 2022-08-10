using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WavelengthSailManager.Models;
using WavelengthSailManager.ViewModels;
using Xamarin.Forms;

namespace WavelengthSailManager
{
    public partial class RaceResults : ContentPage
    {
        public RaceResults(ObservableCollection<Timing> timingList, Race selectedRace)
        {
            InitializeComponent();

            // Set main binding context
            BindingContext = new RaceResultsViewModel(timingList, selectedRace);
        }

        private void NavigateToHome(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }
    }
}
