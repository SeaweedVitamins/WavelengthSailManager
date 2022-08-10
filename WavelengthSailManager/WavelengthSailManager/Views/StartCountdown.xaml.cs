using System;
using System.Collections.Generic;
using WavelengthSailManager.Models;
using WavelengthSailManager.ViewModels;
using Xamarin.Forms;

namespace WavelengthSailManager
{
    public partial class StartCountdown : ContentPage
    {
        List<int> competingBoatList = new List<int>();
        Race selectedRace;

        public StartCountdown(List<int> competingBoatList, Race selectedRace)
        {
            // Start the countdown view model
            this.selectedRace = selectedRace;
            this.competingBoatList = competingBoatList;
            InitializeComponent();
            BindingContext = new CountdownViewModel(competingBoatList, selectedRace);
        }

        private void RestartSequence(object sender, EventArgs e)
        {
            App.Current.MainPage = new RaceStart(competingBoatList, selectedRace);
        }

        private void NextPage(object sender, EventArgs e)
        {
            App.Current.MainPage = new RaceView(competingBoatList, selectedRace);
        }

        private void NavigateToHome(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainPage();
        }
    }
}
